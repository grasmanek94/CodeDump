#include <iostream>
#include <Windows.h>

LRESULT CALLBACK WndProc(HWND hWnd, UINT Msg, WPARAM wParam, LPARAM lParam)
{
	std::cout << "Message " << Msg << std::endl;
	return DefWindowProc(hWnd, Msg, wParam, lParam);
}

int main()
{
	HWND hWnd = GetConsoleWindow();
	SetWindowLong(hWnd, GWL_WNDPROC, (LONG)WndProc);
	MSG Msg;
	while (GetMessage(&Msg, NULL, 0, 0))
	{
		TranslateMessage(&Msg);
		DispatchMessage(&Msg);
	}
	return 0;
}