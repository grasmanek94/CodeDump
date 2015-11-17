// EmbeddableAppView.h : interface of the CEmbeddableAppView class
//


#pragma once


class CEmbeddableAppView : public CEditView
{
protected: // create from serialization only
	CEmbeddableAppView();
	DECLARE_DYNCREATE(CEmbeddableAppView)

// Attributes
public:
	CEmbeddableAppDoc* GetDocument() const;

// Operations
public:

// Overrides
public:
	virtual BOOL PreCreateWindow(CREATESTRUCT& cs);
protected:
	virtual BOOL OnPreparePrinting(CPrintInfo* pInfo);
	virtual void OnBeginPrinting(CDC* pDC, CPrintInfo* pInfo);
	virtual void OnEndPrinting(CDC* pDC, CPrintInfo* pInfo);

// Implementation
public:
	virtual ~CEmbeddableAppView();
#ifdef _DEBUG
	virtual void AssertValid() const;
	virtual void Dump(CDumpContext& dc) const;
#endif

protected:

// Generated message map functions
protected:
	DECLARE_MESSAGE_MAP()
};

#ifndef _DEBUG  // debug version in EmbeddableAppView.cpp
inline CEmbeddableAppDoc* CEmbeddableAppView::GetDocument() const
   { return reinterpret_cast<CEmbeddableAppDoc*>(m_pDocument); }
#endif

