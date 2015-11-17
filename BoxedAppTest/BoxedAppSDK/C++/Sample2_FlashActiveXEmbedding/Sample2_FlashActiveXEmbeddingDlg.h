// Sample2_FlashActiveXEmbeddingDlg.h : header file
//

#if !defined(AFX_SAMPLE2_FLASHACTIVEXEMBEDDINGDLG_H__7C6A7C43_3259_4519_9F20_17EC1A8BF0BF__INCLUDED_)
#define AFX_SAMPLE2_FLASHACTIVEXEMBEDDINGDLG_H__7C6A7C43_3259_4519_9F20_17EC1A8BF0BF__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

/////////////////////////////////////////////////////////////////////////////
// CSample2_FlashActiveXEmbeddingDlg dialog

class CSample2_FlashActiveXEmbeddingDlg : public CDialog
{
// Construction
public:
	CSample2_FlashActiveXEmbeddingDlg(CWnd* pParent = NULL);	// standard constructor

// Dialog Data
	//{{AFX_DATA(CSample2_FlashActiveXEmbeddingDlg)
	enum { IDD = IDD_SAMPLE2_FLASHACTIVEXEMBEDDING_DIALOG };
		// NOTE: the ClassWizard will add data members here
	//}}AFX_DATA

	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CSample2_FlashActiveXEmbeddingDlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support
	//}}AFX_VIRTUAL

// Implementation
protected:
	HICON m_hIcon;

	// Generated message map functions
	//{{AFX_MSG(CSample2_FlashActiveXEmbeddingDlg)
	virtual BOOL OnInitDialog();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_SAMPLE2_FLASHACTIVEXEMBEDDINGDLG_H__7C6A7C43_3259_4519_9F20_17EC1A8BF0BF__INCLUDED_)
