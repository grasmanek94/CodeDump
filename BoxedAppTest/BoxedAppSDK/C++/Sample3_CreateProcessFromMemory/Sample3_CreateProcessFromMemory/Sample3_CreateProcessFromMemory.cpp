// Sample3_CreateProcessFromMemory.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"

#include <shellapi.h>

#include "..\\..\\..\\..\\BoxedAppSDK\\include\\BoxedAppSDK.h"
#pragma comment(lib, "..\\..\\..\\..\\BoxedAppSDK\\lib\\bxsdk32.lib")

int _tmain(int argc, _TCHAR* argv[])
{
	BoxedAppSDK_Init();

// Create virtual app1.exe
	HMODULE hModule = GetModuleHandle(NULL);
    HRSRC hResInfo = FindResource(hModule, _T("BIN1"), _T("BIN"));
    HGLOBAL hResData = LoadResource(hModule, hResInfo);
    LPVOID lpData = LockResource(hResData);
    DWORD dwSize = SizeofResource(hModule, hResInfo);

	HANDLE hFile = BoxedAppSDK_CreateVirtualFile(_T("app1.exe"), GENERIC_WRITE, FILE_SHARE_READ, NULL, CREATE_NEW, 0, NULL);
	DWORD temp;
	WriteFile(hFile, lpData, dwSize, &temp, NULL);
	CloseHandle(hFile);

// Create virtual 1.txt
	hFile = BoxedAppSDK_CreateVirtualFile(_T("1.txt"), GENERIC_WRITE, FILE_SHARE_READ, NULL, CREATE_NEW, 0, NULL);
	CloseHandle(hFile);

// Create virtual dll1.dll
    hResInfo = FindResource(hModule, _T("BIN2"), _T("BIN"));
    hResData = LoadResource(hModule, hResInfo);
    lpData = LockResource(hResData);
    dwSize = SizeofResource(hModule, hResInfo);

	hFile = BoxedAppSDK_CreateVirtualFile(_T("dll1.dll"), GENERIC_WRITE, FILE_SHARE_READ, NULL, CREATE_NEW, 0, NULL);
	WriteFile(hFile, lpData, dwSize, &temp, NULL);
	CloseHandle(hFile);

// Launch it!
	WinExec("app1.exe 1.txt", SW_SHOW);

	printf("Press Ctrl + C to finish");

	Sleep(-1);

	return 0;
}
