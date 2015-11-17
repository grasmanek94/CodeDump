// Inject.cpp : Defines the entry point for the DLL application.
//

#include "stdafx.h"

#include "..\\..\\..\\..\\BoxedAppSDK\\include\\BoxedAppSDK.h"
#pragma comment(lib, "..\\..\\..\\..\\BoxedAppSDK\\lib\\bxsdk32.lib")

#ifdef _MANAGED
#pragma managed(push, off)
#endif

BOOL APIENTRY DllMain( HMODULE hModule,
                       DWORD  ul_reason_for_call,
                       LPVOID lpReserved
					 )
{
	if (DLL_PROCESS_ATTACH == ul_reason_for_call)
	{
		// Let's register ActiveX in virtual registry
		BoxedAppSDK_RegisterCOMLibraryInVirtualRegistry(_T("flash.ocx"));
	}

    return TRUE;
}

#ifdef _MANAGED
#pragma managed(pop)
#endif
