// EmbeddableAppDoc.cpp : implementation of the CEmbeddableAppDoc class
//

#include "stdafx.h"
#include "EmbeddableApp.h"

#include "EmbeddableAppDoc.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// CEmbeddableAppDoc

IMPLEMENT_DYNCREATE(CEmbeddableAppDoc, CDocument)

BEGIN_MESSAGE_MAP(CEmbeddableAppDoc, CDocument)
END_MESSAGE_MAP()


// CEmbeddableAppDoc construction/destruction

CEmbeddableAppDoc::CEmbeddableAppDoc()
{
	// TODO: add one-time construction code here

}

CEmbeddableAppDoc::~CEmbeddableAppDoc()
{
}

BOOL CEmbeddableAppDoc::OnNewDocument()
{
	if (!CDocument::OnNewDocument())
		return FALSE;

	// TODO: add reinitialization code here
	// (SDI documents will reuse this document)

	return TRUE;
}




// CEmbeddableAppDoc serialization

void CEmbeddableAppDoc::Serialize(CArchive& ar)
{
	// CEditView contains an edit control which handles all serialization
	reinterpret_cast<CEditView*>(m_viewList.GetHead())->SerializeRaw(ar);
}


// CEmbeddableAppDoc diagnostics

#ifdef _DEBUG
void CEmbeddableAppDoc::AssertValid() const
{
	CDocument::AssertValid();
}

void CEmbeddableAppDoc::Dump(CDumpContext& dc) const
{
	CDocument::Dump(dc);
}
#endif //_DEBUG


// CEmbeddableAppDoc commands
