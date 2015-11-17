#include "proxydll.h"

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