// EmbeddableAppView.cpp : implementation of the CEmbeddableAppView class
//

#include "stdafx.h"
#include "EmbeddableApp.h"

#include "EmbeddableAppDoc.h"
#include "EmbeddableAppView.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif

// CEmbeddableAppView

IMPLEMENT_DYNCREATE(CEmbeddableAppView, CEditView)

BEGIN_MESSAGE_MAP(CEmbeddableAppView, CEditView)
	// Standard printing commands
	ON_COMMAND(ID_FILE_PRINT, &CEditView::OnFilePrint)
	ON_COMMAND(ID_FILE_PRINT_DIRECT, &CEditView::OnFilePrint)
	ON_COMMAND(ID_FILE_PRINT_PREVIEW, &CEditView::OnFilePrintPreview)
END_MESSAGE_MAP()

// CEmbeddableAppView construction/destruction

CEmbeddableAppView::CEmbeddableAppView()
{
	// TODO: add construction code here

}

CEmbeddableAppView::~CEmbeddableAppView()
{
}

BOOL CEmbeddableAppView::PreCreateWindow(CREATESTRUCT& cs)
{
	// TODO: Modify the Window class or styles here by modifying
	//  the CREATESTRUCT cs

	BOOL bPreCreated = CEditView::PreCreateWindow(cs);
	cs.style &= ~(ES_AUTOHSCROLL|WS_HSCROLL);	// Enable word-wrapping

	return bPreCreated;
}


// CEmbeddableAppView printing

BOOL CEmbeddableAppView::OnPreparePrinting(CPrintInfo* pInfo)
{
	// default CEditView preparation
	return CEditView::OnPreparePrinting(pInfo);
}

void CEmbeddableAppView::OnBeginPrinting(CDC* pDC, CPrintInfo* pInfo)
{
	// Default CEditView begin printing
	CEditView::OnBeginPrinting(pDC, pInfo);
}

void CEmbeddableAppView::OnEndPrinting(CDC* pDC, CPrintInfo* pInfo)
{
	// Default CEditView end printing
	CEditView::OnEndPrinting(pDC, pInfo);
}


// CEmbeddableAppView diagnostics

#ifdef _DEBUG
void CEmbeddableAppView::AssertValid() const
{
	CEditView::AssertValid();
}

void CEmbeddableAppView::Dump(CDumpContext& dc) const
{
	CEditView::Dump(dc);
}

CEmbeddableAppDoc* CEmbeddableAppView::GetDocument() const // non-debug version is inline
{
	ASSERT(m_pDocument->IsKindOf(RUNTIME_CLASS(CEmbeddableAppDoc)));
	return (CEmbeddableAppDoc*)m_pDocument;
}
#endif //_DEBUG
