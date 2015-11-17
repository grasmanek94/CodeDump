// EmbeddableApp.h : main header file for the EmbeddableApp application
//
#pragma once

#ifndef __AFXWIN_H__
	#error "include 'stdafx.h' before including this file for PCH"
#endif

#include "resource.h"       // main symbols


// CEmbeddableAppApp:
// See EmbeddableApp.cpp for the implementation of this class
//

class CEmbeddableAppApp : public CWinApp
{
public:
	CEmbeddableAppApp();


// Overrides
public:
	virtual BOOL InitInstance();

// Implementation
	afx_msg void OnAppAbout();
	DECLARE_MESSAGE_MAP()
};

extern CEmbeddableAppApp theApp;