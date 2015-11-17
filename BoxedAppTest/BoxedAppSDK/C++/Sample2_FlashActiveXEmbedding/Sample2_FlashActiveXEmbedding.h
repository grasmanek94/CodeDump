// Sample2_FlashActiveXEmbedding.h : main header file for the SAMPLE2_FLASHACTIVEXEMBEDDING application
//

#if !defined(AFX_SAMPLE2_FLASHACTIVEXEMBEDDING_H__FBEF2FF2_E430_4F81_8155_4CFA71BB6016__INCLUDED_)
#define AFX_SAMPLE2_FLASHACTIVEXEMBEDDING_H__FBEF2FF2_E430_4F81_8155_4CFA71BB6016__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#ifndef __AFXWIN_H__
	#error include 'stdafx.h' before including this file for PCH
#endif

#include "resource.h"		// main symbols

/////////////////////////////////////////////////////////////////////////////
// CSample2_FlashActiveXEmbeddingApp:
// See Sample2_FlashActiveXEmbedding.cpp for the implementation of this class
//

class CSample2_FlashActiveXEmbeddingApp : public CWinApp
{
public:
	CSample2_FlashActiveXEmbeddingApp();

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CSample2_FlashActiveXEmbeddingApp)
	public:
	virtual BOOL InitInstance();
	//}}AFX_VIRTUAL

// Implementation

	//{{AFX_MSG(CSample2_FlashActiveXEmbeddingApp)
		// NOTE - the ClassWizard will add and remove member functions here.
		//    DO NOT EDIT what you see in these blocks of generated code !
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};


/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_SAMPLE2_FLASHACTIVEXEMBEDDING_H__FBEF2FF2_E430_4F81_8155_4CFA71BB6016__INCLUDED_)
