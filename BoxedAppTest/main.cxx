#include <iostream>
#include <fstream>
#include <string>
#include <BoxedAppSDK.h>


#pragma comment(lib, "bxsdk32.lib")

int main()
{
	BoxedAppSDK_Init();
	BoxedAppSDK_EnableOption(DEF_BOXEDAPPSDK_OPTION__EMBED_BOXEDAPP_IN_CHILD_PROCESSES, TRUE);
	BoxedAppSDK_EnableOption(DEF_BOXEDAPPSDK_OPTION__ENABLE_ALL_HOOKS, TRUE);
	BoxedAppSDK_SetFileIsolationModeA(BX_ISOLATION_MODE::BxIsolationMode_WriteCopy, "C:\\Users\\Rafal\\Desktop\\", 
		"D:\\System\\Temp\\BoxedApp\\");

	WinExec("C:\\Users\\Rafal\\Desktop\\RedirectTestX64.exe", SW_SHOW);

	printf("Press Ctrl + C to finish\r\n");

	Sleep(-1);
	return 0;
}