#include <iostream>
#include <thread>
#include <chrono>


#include <windows.h>
#include <stdio.h>
#include <tchar.h>

void fork()
{
	typedef   void(*FunctionFunc)  (void);
	static HMODULE cygwinDll = nullptr;
	FunctionFunc _fork = nullptr;
	FunctionFunc cygwin_dll_init = nullptr;
	if (!cygwinDll)
	{
		cygwinDll = LoadLibrary(L"cygwin1.dll");
		cygwin_dll_init = (FunctionFunc)GetProcAddress(cygwinDll, "cygwin_dll_init");
		_fork = (FunctionFunc)GetProcAddress(cygwinDll, "fork");

		try
		{
			cygwin_dll_init();
			_fork();
		}
		catch (const std::exception& ex)
		{
			std::cout << "Exception: " << ex.what() << std::endl;
		}
	}
	/*STARTUPINFO si;
	PROCESS_INFORMATION pi;

	ZeroMemory(&si, sizeof(si));
	si.cb = sizeof(si);

	GetStartupInfo(&si);

	ZeroMemory(&pi, sizeof(pi));

	PROCESS_INFORMATION_CLASS pic;
	ZeroMemory(&pic, sizeof(pic));

	GetProcessInformation(GetCurrentProcess(), pic, &pi, sizeof(pi));
	
	wchar_t filename[MAX_PATH];
	GetModuleFileName(NULL, filename, MAX_PATH);
	// Start the child process. 
	if (!CreateProcess(filename,   // No module name (use command line)
		NULL,        // Command line
		NULL,           // Process handle not inheritable
		NULL,           // Thread handle not inheritable
		TRUE,          // Set handle inheritance to FALSE
		NULL | CREATE_NEW_CONSOLE ,              // No creation flags
		NULL,           // Use parent's environment block
		NULL,           // Use parent's starting directory 
		&si,            // Pointer to STARTUPINFO structure
		&pi)           // Pointer to PROCESS_INFORMATION structure
		)
	{
		printf("CreateProcess failed (%d).\n", GetLastError());
		return;
	}*/
}

int main()
{
	size_t i = 0;
	while (true)
	{
		++i;
		std::cout << i << std::endl;
		std::this_thread::sleep_for(std::chrono::milliseconds(1000));
		if (i == 3)
		{
			fork();
		}
	}
	return 0;
}