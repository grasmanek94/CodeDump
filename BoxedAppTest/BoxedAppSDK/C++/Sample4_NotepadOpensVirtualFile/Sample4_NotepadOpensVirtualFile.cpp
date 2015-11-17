// Sample4_NotepadOpensVirtualFile.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"

#include "..\\..\\..\\BoxedAppSDK\\include\\BoxedAppSDK.h"
#pragma comment(lib, "..\\..\\..\\BoxedAppSDK\\lib\\bxsdk32.lib")

int _tmain(int argc, _TCHAR* argv[])
{
	BoxedAppSDK_Init();

	HANDLE hFile = BoxedAppSDK_CreateVirtualFile(_T("1.txt"), GENERIC_WRITE, FILE_SHARE_READ, NULL, CREATE_NEW, 0, NULL);
	const char* szText = "This is a virtual file. Cool! You have just loaded the virtual file into notepad.exe!\r\nDon't forget to obtain a license ;)\r\nhttp://boxedapp.com/order.html";
	DWORD temp;
	WriteFile(hFile, szText, lstrlenA(szText), &temp, NULL);
	CloseHandle(hFile);

	// Inject BoxedApp engine to child processes
	BoxedAppSDK_EnableOption(DEF_BOXEDAPPSDK_OPTION__EMBED_BOXEDAPP_IN_CHILD_PROCESSES, TRUE);
	// Now notepad loads the virtual file
	WinExec("notepad.exe 1.txt", SW_SHOW);

	printf("Press Ctrl + C to finish");

	Sleep(-1);

	return 0;
}

