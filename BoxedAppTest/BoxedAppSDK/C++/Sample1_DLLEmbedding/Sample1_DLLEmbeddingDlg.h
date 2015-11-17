// Sample1_DLLEmbeddingDlg.h : header file
//

#if !defined(AFX_SAMPLE1_DLLEMBEDDINGDLG_H__A62CCD79_FFF4_49DD_B838_A4C28B5CA090__INCLUDED_)
#define AFX_SAMPLE1_DLLEMBEDDINGDLG_H__A62CCD79_FFF4_49DD_B838_A4C28B5CA090__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

/////////////////////////////////////////////////////////////////////////////
// CSample1_DLLEmbeddingDlg dialog

class CSample1_DLLEmbeddingDlg : public CDialog
{
// Construction
public:
	CSample1_DLLEmbeddingDlg(CWnd* pParent = NULL);	// standard constructor

// Dialog Data
	//{{AFX_DATA(CSample1_DLLEmbeddingDlg)
	enum { IDD = IDD_SAMPLE1_DLLEMBEDDING_DIALOG };
		// NOTE: the ClassWizard will add data members here
	//}}AFX_DATA

	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CSample1_DLLEmbeddingDlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support
	//}}AFX_VIRTUAL

// Implementation
protected:
	HICON m_hIcon;

	// Generated message map functions
	//{{AFX_MSG(CSample1_DLLEmbeddingDlg)
	virtual BOOL OnInitDialog();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	afx_msg void OnButtonCallFunction();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_SAMPLE1_DLLEMBEDDINGDLG_H__A62CCD79_FFF4_49DD_B838_A4C28B5CA090__INCLUDED_)
