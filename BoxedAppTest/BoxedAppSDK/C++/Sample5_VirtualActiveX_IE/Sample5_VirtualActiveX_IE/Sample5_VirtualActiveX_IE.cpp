// Sample5_VirtualActiveX_IE.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"

#include "..\\..\\..\\..\\BoxedAppSDK\\include\\BoxedAppSDK.h"
#pragma comment(lib, "..\\..\\..\\..\\BoxedAppSDK\\lib\\bxsdk32.lib")

#define DEF_DLL_NAME    _T("\\Inject.dll")

int _tmain(int argc, _TCHAR* argv[])
{
    BoxedAppSDK_Init();

    TCHAR szCurDir[MAX_PATH + 1] = { 0 };
    GetCurrentDirectory(MAX_PATH, szCurDir);

    TCHAR szCmdLine[MAX_PATH + 1] = { 0 };
    ExpandEnvironmentStrings(_T("\"%ProgramFiles%\\Internet Explorer\\iexplore.exe\""), szCmdLine, MAX_PATH);

    lstrcat(szCmdLine, _T(" "));
    lstrcat(szCmdLine, szCurDir);
    lstrcat(szCmdLine, _T("\\test.html"));

    PROCESS_INFORMATION pi = { 0 };

    STARTUPINFO startup_info = { 0 };
    startup_info.cb = sizeof(startup_info);
    startup_info.dwFlags = STARTF_USESHOWWINDOW;
    startup_info.wShowWindow = SW_SHOW;

    // Create process specifying CREATE_SUSPENDED flag
    BOOL bRes = CreateProcess(NULL, szCmdLine, NULL, NULL, FALSE, CREATE_SUSPENDED, NULL, NULL, &startup_info, &pi);

    if (bRes)
    {
        // Inject our library that will register ActiveX
        int i = 0;
        _TCHAR szDir[MAX_PATH + 1] = { 0 };

        GetModuleFileName(NULL, szDir, MAX_PATH + 1); 

        for (i = lstrlen(szDir) - 1; i >= 0; i--)
        {
            if (_T('\\') == szDir[i])
            {
                szDir[i] = 0;
                break;
            }	
        }

        TCHAR* szDllFullPath = (TCHAR*)malloc(sizeof(TCHAR) * (i + lstrlen(DEF_DLL_NAME) + 1));

        _stprintf(szDllFullPath, _T("%s%s"), szDir, DEF_DLL_NAME); 

        BoxedAppSDK_RemoteProcess_LoadLibrary(pi.dwProcessId, szDllFullPath);

        ResumeThread(pi.hThread);

        WaitForSingleObject(pi.hProcess, INFINITE);

        free(szDllFullPath);
        CloseHandle(pi.hProcess);
        CloseHandle(pi.hThread);
	}

	return 0;
}
