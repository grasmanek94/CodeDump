// Sample2_FlashActiveXEmbedding.cpp : Defines the class behaviors for the application.
//

#include "stdafx.h"
#include "Sample2_FlashActiveXEmbedding.h"
#include "Sample2_FlashActiveXEmbeddingDlg.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CSample2_FlashActiveXEmbeddingApp

BEGIN_MESSAGE_MAP(CSample2_FlashActiveXEmbeddingApp, CWinApp)
	//{{AFX_MSG_MAP(CSample2_FlashActiveXEmbeddingApp)
		// NOTE - the ClassWizard will add and remove mapping macros here.
		//    DO NOT EDIT what you see in these blocks of generated code!
	//}}AFX_MSG
	ON_COMMAND(ID_HELP, CWinApp::OnHelp)
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CSample2_FlashActiveXEmbeddingApp construction

CSample2_FlashActiveXEmbeddingApp::CSample2_FlashActiveXEmbeddingApp()
{
	// TODO: add construction code here,
	// Place all significant initialization in InitInstance
}

/////////////////////////////////////////////////////////////////////////////
// The one and only CSample2_FlashActiveXEmbeddingApp object

CSample2_FlashActiveXEmbeddingApp theApp;

// Load resource helper
void LoadResourceHelper( /* in */ LPCTSTR lpszName, 
                         /* in */ LPCTSTR lpszType, 
                         /* out */ LPVOID& lpData, 
                         /* out */ DWORD& dwSize)
{
    HMODULE hModule = GetModuleHandle(NULL);
    HRSRC hResInfo = FindResource(hModule, lpszName, lpszType);
    HGLOBAL hResData = LoadResource(hModule, hResInfo);
    lpData = LockResource(hResData);
    dwSize = SizeofResource(hModule, hResInfo);
}

void CreateVirtualFlashOCX()
{
	LPVOID pBuffer;
	DWORD dwSize;
	LoadResourceHelper(MAKEINTRESOURCE(IDR_BIN_FLASH_OCX), _T("BIN"), pBuffer, dwSize);

	HANDLE hVirtualFile1 = 
		BoxedAppSDK_CreateVirtualFile(
			_T("C:\\Flash9e.ocx"), 
			GENERIC_ALL, 
			FILE_SHARE_READ, 
			NULL, 
			CREATE_NEW, 
			0, 
			NULL);

	DWORD dwTemp;
	WriteFile(hVirtualFile1, pBuffer, dwSize, &dwTemp, NULL);

	CloseHandle(hVirtualFile1);

	BoxedAppSDK_RegisterCOMLibraryInVirtualRegistry(_T("C:\\Flash9e.ocx"));
}

/////////////////////////////////////////////////////////////////////////////
// CSample2_FlashActiveXEmbeddingApp initialization

BOOL CSample2_FlashActiveXEmbeddingApp::InitInstance()
{
	AfxEnableControlContainer();

	BoxedAppSDK_Init();

	CreateVirtualFlashOCX();

	{
		LPVOID pBuffer;
		DWORD dwSize;
		LoadResourceHelper(MAKEINTRESOURCE(IDR_BIN_MOVIE_SWF), _T("BIN"), pBuffer, dwSize);

		HANDLE hVirtualFile = 
			BoxedAppSDK_CreateVirtualFile(
				_T("C:\\1.swf"), 
				GENERIC_ALL, 
				FILE_SHARE_READ, 
				NULL, 
				CREATE_NEW, 
				0, 
				NULL);

		DWORD dwTemp;
		WriteFile(hVirtualFile, pBuffer, dwSize, &dwTemp, NULL);

		CloseHandle(hVirtualFile);
	}

	// Standard initialization
	// If you are not using these features and wish to reduce the size
	//  of your final executable, you should remove from the following
	//  the specific initialization routines you do not need.

#ifdef _AFXDLL
	Enable3dControls();			// Call this when using MFC in a shared DLL
#else
	Enable3dControlsStatic();	// Call this when linking to MFC statically
#endif

	CSample2_FlashActiveXEmbeddingDlg dlg;
	m_pMainWnd = &dlg;
	int nResponse = dlg.DoModal();
	if (nResponse == IDOK)
	{
		// TODO: Place code here to handle when the dialog is
		//  dismissed with OK
	}
	else if (nResponse == IDCANCEL)
	{
		// TODO: Place code here to handle when the dialog is
		//  dismissed with Cancel
	}

	// Since the dialog has been closed, return FALSE so that we exit the
	//  application, rather than start the application's message pump.
	return FALSE;
}
