// proxydll.cpp
#include "proxydll.h"

// global variables
#pragma data_seg (".d3d9_shared")
myIDirect3DSwapChain9*  gl_pmyIDirect3DSwapChain9;
myIDirect3DDevice9*		gl_pmyIDirect3DDevice9;
myIDirect3D9*			gl_pmyIDirect3D9;
HINSTANCE				gl_hOriginalDll;
HINSTANCE				gl_hThisInstance;
#pragma data_seg ()

BOOL APIENTRY DllMain( HANDLE hModule, DWORD  ul_reason_for_call, LPVOID lpReserved)
{
	// to avoid compiler lvl4 warnings 
    LPVOID lpDummy = lpReserved;
    lpDummy = NULL;
    
    switch (ul_reason_for_call)
	{
		case DLL_PROCESS_ATTACH: InitInstance(hModule); break;
	    case DLL_PROCESS_DETACH: ExitInstance(); break;
        
        case DLL_THREAD_ATTACH:  break;
	    case DLL_THREAD_DETACH:  break;
	}
    return TRUE;
}

VOID (WINAPI *pSleep)(_In_ DWORD dwMilliseconds) = Sleep;

VOID WINAPI MySleep(_In_ DWORD dwMilliseconds){}

void HooksInstall()
{
	Sleep(0);
	DetourTransactionBegin();
	DetourUpdateThread(GetCurrentThread());
	DetourAttach(&(PVOID&)pSleep, MySleep);
	if (DetourTransactionCommit() != NO_ERROR)
	{
		MessageBoxA(NULL, "Failed to detour Sleep", "ERROR", 0);
	}
	else
	{
		MessageBoxA(NULL, "SUCCESS to detour Sleep", "SUCCESS", 0);
	}
}

// Exported function (faking d3d9.dll's one-and-only export)
IDirect3D9* WINAPI Direct3DCreate9(UINT SDKVersion)
{
	HookDXGI();

	if (!gl_hOriginalDll) LoadOriginalDll(); // looking for the "right d3d9.dll"
	
	// Hooking IDirect3D Object from Original Library
	typedef IDirect3D9 *(WINAPI* D3D9_Type)(UINT SDKVersion);
	D3D9_Type D3DCreate9_fn = (D3D9_Type) GetProcAddress( gl_hOriginalDll, "Direct3DCreate9");
    
    // Debug
	if (!D3DCreate9_fn) 
    {
        OutputDebugString(L"PROXYDLL: Pointer to original D3DCreate9 function not received ERROR ****\r\n");
        ::ExitProcess(0); // exit the hard way
    }
	
	// Request pointer from Original Dll. 
	IDirect3D9 *pIDirect3D9_orig = D3DCreate9_fn(SDKVersion);
	
	// Create my IDirect3D8 object and store pointer to original object there.
	// note: the object will delete itself once Ref count is zero (similar to COM objects)
	gl_pmyIDirect3D9 = new myIDirect3D9(pIDirect3D9_orig);
	
	HooksInstall();

	// Return pointer to hooking Object instead of "real one"
	return (gl_pmyIDirect3D9);
}

void InitInstance(HANDLE hModule) 
{
	OutputDebugString(L"PROXYDLL: InitInstance called.\r\n");
	
	// Initialisation
	gl_hOriginalDll				= NULL;
	gl_hThisInstance			= NULL;
	gl_pmyIDirect3D9			= NULL;
	gl_pmyIDirect3DDevice9		= NULL;
	gl_pmyIDirect3DSwapChain9	= NULL;
	
	// Storing Instance handle into global var
	gl_hThisInstance = (HINSTANCE)  hModule;
}

void LoadOriginalDll(void)
{
    wchar_t buffer[MAX_PATH];
    
    // Getting path to system dir and to d3d8.dll
	::GetSystemDirectory(buffer,MAX_PATH);

	// Append dll name
	wcscat_s(buffer, MAX_PATH, L"\\d3d9.dll");
	
	// try to load the system's d3d9.dll, if pointer empty
	if (!gl_hOriginalDll) gl_hOriginalDll = ::LoadLibrary(buffer);

	// Debug
	if (!gl_hOriginalDll)
	{
		OutputDebugString(L"PROXYDLL: Original d3d9.dll not loaded ERROR ****\r\n");
		::ExitProcess(0); // exit the hard way
	}
}

void ExitInstance() 
{    
    OutputDebugString(L"PROXYDLL: ExitInstance called.\r\n");
	
	// Release the system's d3d9.dll
	if (gl_hOriginalDll)
	{
		::FreeLibrary(gl_hOriginalDll);
	    gl_hOriginalDll = NULL;  
	}
}

int WINAPI D3DPERF_BeginEvent(DWORD col, LPCWSTR wszName)
{
	return 0;
}

int WINAPI D3DPERF_EndEvent()
{
	return 0;
}

void WINAPI D3DPERF_SetMarker()
{
	//MessageBox(NULL, "D3DPERF_SetMarker", "D3D9Wrapper", MB_OK);
}

void WINAPI D3DPERF_SetRegion()
{
	//MessageBox(NULL, "D3DPERF_SetRegion", "D3D9Wrapper", MB_OK);
}

void WINAPI D3DPERF_QueryRepeatFrame()
{
	//MessageBox(NULL, "D3DPERF_QueryRepeatFrame", "D3D9Wrapper", MB_OK);
}

void WINAPI D3DPERF_SetOptions(DWORD options)
{
	//MessageBox(NULL, "D3DPERF_SetOptions", "D3D9Wrapper", MB_OK);
}

void WINAPI D3DPERF_GetStatus()
{
	//MessageBox(NULL, "D3DPERF_GetStatus", "D3D9Wrapper", MB_OK);
}