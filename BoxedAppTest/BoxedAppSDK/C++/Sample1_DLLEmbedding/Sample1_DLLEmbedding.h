// Sample1_DLLEmbedding.h : main header file for the SAMPLE1_DLLEMBEDDING application
//

#if !defined(AFX_SAMPLE1_DLLEMBEDDING_H__9CE65D96_9FC8_4A43_8720_07FE474AC0A1__INCLUDED_)
#define AFX_SAMPLE1_DLLEMBEDDING_H__9CE65D96_9FC8_4A43_8720_07FE474AC0A1__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#ifndef __AFXWIN_H__
	#error include 'stdafx.h' before including this file for PCH
#endif

#include "resource.h"		// main symbols

/////////////////////////////////////////////////////////////////////////////
// CSample1_DLLEmbeddingApp:
// See Sample1_DLLEmbedding.cpp for the implementation of this class
//

class CSample1_DLLEmbeddingApp : public CWinApp
{
public:
	CSample1_DLLEmbeddingApp();

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CSample1_DLLEmbeddingApp)
	public:
	virtual BOOL InitInstance();
	//}}AFX_VIRTUAL

// Implementation

	//{{AFX_MSG(CSample1_DLLEmbeddingApp)
		// NOTE - the ClassWizard will add and remove member functions here.
		//    DO NOT EDIT what you see in these blocks of generated code !
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};


/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_SAMPLE1_DLLEMBEDDING_H__9CE65D96_9FC8_4A43_8720_07FE474AC0A1__INCLUDED_)
