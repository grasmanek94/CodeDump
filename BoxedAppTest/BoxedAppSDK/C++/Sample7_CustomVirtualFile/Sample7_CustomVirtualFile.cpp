// Copyright (c) Softanics

// Sample7_CustomVirtualFile.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "MyVirtualFile.h"

int _tmain(int argc, _TCHAR* argv[])
{
    // Init BoxedApp SDK
    BoxedAppSDK_Init();

    // Allow injection BoxedApp engine into child processes
    BoxedAppSDK_EnableOption(DEF_BOXEDAPPSDK_OPTION__EMBED_BOXEDAPP_IN_CHILD_PROCESSES, TRUE);

    // Create our implementation of IStream
    LPSTREAM pMyStream;
    CreateMyStream(&pMyStream);

    // Create a virtual file that contains data in our IStream
    HANDLE hFile = 
        BoxedAppSDK_CreateVirtualFileBasedOnIStream(
            _T("1.txt"), 
            GENERIC_WRITE, 
            FILE_SHARE_READ, 
            NULL, 
            CREATE_NEW, 
            0, 
            NULL, 
            pMyStream);

    // Let's write something to our virtual file
    // Data will be saved to pMyStream
    const char* szText = "This is a virtual file. Cool! You have just loaded the virtual file into notepad.exe!\r\nDon't forget to obtain a license ;)\r\nhttp://boxedapp.com/order.html";
    DWORD temp;
    WriteFile(hFile, szText, lstrlenA(szText), &temp, NULL);
    CloseHandle(hFile);

    // Now notepad loads the virtual file
    WinExec("notepad.exe 1.txt", SW_SHOW);

    printf("Press Ctrl + C to finish");

    Sleep(-1);

    return 0;
}
