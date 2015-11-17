// Sample1_DLLEmbedding.cpp : Defines the class behaviors for the application.
//

#include "stdafx.h"
#include "Sample1_DLLEmbedding.h"
#include "Sample1_DLLEmbeddingDlg.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CSample1_DLLEmbeddingApp

BEGIN_MESSAGE_MAP(CSample1_DLLEmbeddingApp, CWinApp)
	//{{AFX_MSG_MAP(CSample1_DLLEmbeddingApp)
		// NOTE - the ClassWizard will add and remove mapping macros here.
		//    DO NOT EDIT what you see in these blocks of generated code!
	//}}AFX_MSG
	ON_COMMAND(ID_HELP, CWinApp::OnHelp)
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CSample1_DLLEmbeddingApp construction

CSample1_DLLEmbeddingApp::CSample1_DLLEmbeddingApp()
{
	// TODO: add construction code here,
	// Place all significant initialization in InitInstance
}

/////////////////////////////////////////////////////////////////////////////
// The one and only CSample1_DLLEmbeddingApp object

CSample1_DLLEmbeddingApp theApp;

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

/////////////////////////////////////////////////////////////////////////////
// CSample1_DLLEmbeddingApp initialization

BOOL CSample1_DLLEmbeddingApp::InitInstance()
{
	AfxEnableControlContainer();

	//
	BoxedAppSDK_Init();

	HANDLE hFile__DLL1 = 
		BoxedAppSDK_CreateVirtualFile(
			_T("DLL1.dll"), 
			GENERIC_WRITE, 
			FILE_SHARE_READ, 
			NULL, 
			CREATE_NEW, 
			0, 
			NULL
		);

	LPVOID pBuffer;
	DWORD dwSize;
	LoadResourceHelper(_T("IDR_BIN_DLL"), _T("DLL"), pBuffer, dwSize);

	DWORD dwTemp;
	WriteFile(hFile__DLL1, pBuffer, dwSize, &dwTemp, NULL);

	CloseHandle(hFile__DLL1);

	// Standard initialization
	// If you are not using these features and wish to reduce the size
	//  of your final executable, you should remove from the following
	//  the specific initialization routines you do not need.

#ifdef _AFXDLL
	Enable3dControls();			// Call this when using MFC in a shared DLL
#else
	Enable3dControlsStatic();	// Call this when linking to MFC statically
#endif

	CSample1_DLLEmbeddingDlg dlg;
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
