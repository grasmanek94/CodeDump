// DLL1.cpp : Defines the entry point for the DLL application.
//

#include "stdafx.h"
#include "DLL1.h"
#include <tchar.h>

BOOL APIENTRY DllMain( HANDLE hModule, 
                       DWORD  ul_reason_for_call, 
                       LPVOID lpReserved
					 )
{
    switch (ul_reason_for_call)
	{
		case DLL_PROCESS_ATTACH:
		case DLL_THREAD_ATTACH:
		case DLL_THREAD_DETACH:
		case DLL_PROCESS_DETACH:
			break;
    }
    return TRUE;
}

DLL1_API void Function()
{
	MessageBox(NULL, _T("This is a function \"Function\" from DLL1.dll"), _T("Message"), MB_OK);
}
