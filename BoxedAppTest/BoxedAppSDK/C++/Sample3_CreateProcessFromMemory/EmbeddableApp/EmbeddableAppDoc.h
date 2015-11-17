// EmbeddableAppDoc.h : interface of the CEmbeddableAppDoc class
//


#pragma once


class CEmbeddableAppDoc : public CDocument
{
protected: // create from serialization only
	CEmbeddableAppDoc();
	DECLARE_DYNCREATE(CEmbeddableAppDoc)

// Attributes
public:

// Operations
public:

// Overrides
public:
	virtual BOOL OnNewDocument();
	virtual void Serialize(CArchive& ar);

// Implementation
public:
	virtual ~CEmbeddableAppDoc();
#ifdef _DEBUG
	virtual void AssertValid() const;
	virtual void Dump(CDumpContext& dc) const;
#endif

protected:

// Generated message map functions
protected:
	DECLARE_MESSAGE_MAP()
};


