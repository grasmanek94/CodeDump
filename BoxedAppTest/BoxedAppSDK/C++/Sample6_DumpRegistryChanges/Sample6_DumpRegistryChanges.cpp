// Sample6_DumpRegistryChanges.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"

#include "..\\..\\..\\BoxedAppSDK\\include\\BoxedAppSDK.h"
#pragma comment(lib, "..\\..\\..\\BoxedAppSDK\\lib\\bxsdk32.lib")

BOOL WINAPI VirtualRegKeysEnum(HKEY hRootKey, LPCTSTR szSubKey, LPARAM lParam);

int _tmain(int argc, _TCHAR* argv[])
{
	BoxedAppSDK_Init();

	// All changes are virtual within the following block
	BoxedAppSDK_EnableOption(DEF_BOXEDAPPSDK_OPTION__ALL_CHANGES_ARE_VIRTUAL, TRUE);
	{
		// for example: 
		// BoxedAppSDK_RegisterCOMLibraryInVirtualRegistry(_T("Flash.ocx"));

		for (int i = 1; i < argc; i++)
		{
			BoxedAppSDK_RegisterCOMLibraryInVirtualRegistry(argv[i]);
		}
	}
	BoxedAppSDK_EnableOption(DEF_BOXEDAPPSDK_OPTION__ALL_CHANGES_ARE_VIRTUAL, FALSE);

	BoxedAppSDK_EnumVirtualRegKeys(&VirtualRegKeysEnum, 0);

	return 0;
}

BOOL WINAPI VirtualRegKeysEnum(HKEY hRootKey, LPCTSTR szSubKey, LPARAM lParam)
{
	LPCWSTR szRootKey = L"";

	if (HKEY_CURRENT_USER == hRootKey)
		szRootKey = L"HKEY_CURRENT_USER";
	else if (HKEY_CLASSES_ROOT == hRootKey)
		szRootKey = L"HKEY_CLASSES_ROOT";
	else if (HKEY_USERS == hRootKey)
		szRootKey = L"HKEY_USERS";
	else if (HKEY_CURRENT_CONFIG == hRootKey)
		szRootKey = L"HKEY_CURRENT_CONFIG";
	else if (HKEY_LOCAL_MACHINE == hRootKey)
		szRootKey = L"HKEY_LOCAL_MACHINE";

	wprintf(L"%s\\%s\n", szRootKey, szSubKey);

	HKEY hKey;

	if (ERROR_SUCCESS == RegOpenKey(hRootKey, szSubKey, &hKey))
	{
		BOOL bFinished = FALSE;

		for (DWORD dwIndex = 0; ; dwIndex++)
		{
			DWORD dwValueNameLength = 1;
			DWORD dwDataLength = 1;

			while (true)
			{
				WCHAR* pValueName = new WCHAR[dwValueNameLength];
				PBYTE pData = new BYTE[dwDataLength];
				LONG lResult = RegEnumValueW(hKey, dwIndex, pValueName, &dwValueNameLength, NULL, NULL, pData, &dwDataLength);

				if (ERROR_MORE_DATA == lResult)
				{
					dwValueNameLength += 256;
					dwDataLength += 256;
				}
				else if (ERROR_SUCCESS == lResult)
				{
					wprintf(L"\tvalue: \"%s\"\n", pValueName);

					break;
				}
				else
				{
					bFinished = TRUE;

					break;
				}

				delete[] pValueName;
				delete[] pData;
			}

			if (bFinished)
				break;
		}

		RegCloseKey(hKey);
	}

	return TRUE;
}
