// Dll1.cpp : Defines the entry point for the DLL application.
//

#include "stdafx.h"
#include "Dll1.h"


#ifdef _MANAGED
#pragma managed(push, off)
#endif

BOOL APIENTRY DllMain( HMODULE hModule,
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

#ifdef _MANAGED
#pragma managed(pop)
#endif

// This is an example of an exported variable
DLL1_API int nDll1=0;

// This is an example of an exported function.
DLL1_API int fnDll1(void)
{
	MessageBox(NULL, _T("Hello from embedded DLL!"), _T("Hello from embedded DLL!"), MB_OK | MB_SYSTEMMODAL);

	return 1;
}

// This is the constructor of a class that has been exported.
// see Dll1.h for the class definition
CDll1::CDll1()
{
	return;
}
