// Copyright (c) Softanics

#include "StdAfx.h"
#include "MyVirtualFile.h"

// Forward declarations

// My virtual file
class CMyFile;

// IStream implementation that reads / writes data from CMyFile
// Each instance of CMyStream has its own file current position
class CMyStream;

/// Simple lock
class CSimpleLock
{
private: 
    CRITICAL_SECTION m_cs;

public: 

    /// Class to automaticalle acquire and release lock
    class Owner // NOLINT(build/class)
    {
    private: 
        CSimpleLock& m_lock;

    public: 
        explicit Owner(CSimpleLock& lock);
        ~Owner();
    };

    friend class Owner;

private: 
    void Lock();
    void Unlock();

public: 
    CSimpleLock();
    ~CSimpleLock();
};

/// My virtual file
class CMyFile
{
private: 

    // Reference count
    LONG m_nRefCount;

    // Memory buffer that contains data of the file
    PVOID m_pBuffer;

    // Size of the buffer
    DWORD m_dwSize;

    // Lock
    CSimpleLock m_lock;

public: 

    // Creates instance of CMyFile
    static CMyFile* Create();

    // Creates IStream that is associated with CMyFile
    LPSTREAM CreateNewStream();

private: 
    CMyFile();
    ~CMyFile();

public: 
    void AddRef();
    void Release();

    DWORD GetSize() const;

    void ReAlloc(ULARGE_INTEGER NewSize);

    DWORD Read(DWORD nCurrentPosition, void* pv, ULONG cb);
    DWORD Write(DWORD nCurrentPosition, const void* pv, ULONG cb);
};

/// Represents file stream
class CMyStream : public IStream
{
private: 

    // Reference count
    LONG m_nRefCount;

    // Reference to CMyFile
    CMyFile* m_pMyFile;

    // Current position
    DWORD m_nCurrentPosition;

    // Lock
    CSimpleLock m_lock;

public: 
    explicit CMyStream(CMyFile* pMyFile);
    ~CMyStream();

    // IUnknown
    virtual ULONG STDMETHODCALLTYPE AddRef();
    virtual ULONG STDMETHODCALLTYPE Release();
    virtual HRESULT STDMETHODCALLTYPE QueryInterface(REFIID riid, void** ppvObject);

    // ISequentialStream
    virtual HRESULT STDMETHODCALLTYPE Read(void* pv, ULONG cb, ULONG* pcbRead);
    virtual HRESULT STDMETHODCALLTYPE Write(const void* pv, ULONG cb, ULONG* pcbWritten);

    // IStream
    virtual HRESULT STDMETHODCALLTYPE Seek(LARGE_INTEGER dlibMove, DWORD dwOrigin, ULARGE_INTEGER* plibNewPosition);
    virtual HRESULT STDMETHODCALLTYPE SetSize(ULARGE_INTEGER libNewSize);
    virtual HRESULT STDMETHODCALLTYPE CopyTo(IStream* pstm, ULARGE_INTEGER cb, ULARGE_INTEGER* pcbRead, ULARGE_INTEGER* pcbWritten);
    virtual HRESULT STDMETHODCALLTYPE Commit(DWORD grfCommitFlags);
    virtual HRESULT STDMETHODCALLTYPE Revert();
    virtual HRESULT STDMETHODCALLTYPE LockRegion(ULARGE_INTEGER libOffset, ULARGE_INTEGER cb, DWORD dwLockType);
    virtual HRESULT STDMETHODCALLTYPE UnlockRegion(ULARGE_INTEGER libOffset, ULARGE_INTEGER cb, DWORD dwLockType);
    virtual HRESULT STDMETHODCALLTYPE Stat(STATSTG* pstatstg, DWORD grfStatFlag);
    virtual HRESULT STDMETHODCALLTYPE Clone(IStream** ppstm);
};

// class CMyFile

LPSTREAM CMyFile::CreateNewStream()
{
    return new CMyStream(this);
}

// Creates instance of CMyFile
CMyFile* CMyFile::Create()
{
    return new CMyFile();
}

CMyFile::CMyFile() : 
    m_pBuffer(NULL), 
    m_dwSize(0), 
    m_nRefCount(1)
{
}

CMyFile::~CMyFile()
{
    if (NULL != m_pBuffer)
    {
        HeapFree(GetProcessHeap(), 0, m_pBuffer);
    }
}

void CMyFile::AddRef()
{
    InterlockedIncrement(&m_nRefCount);
}

void CMyFile::Release()
{
    if (0 == InterlockedDecrement(&m_nRefCount))
    {
        delete this;
    }
}

DWORD CMyFile::GetSize() const
{
    return m_dwSize;
}

void CMyFile::ReAlloc(ULARGE_INTEGER NewSize)
{
    CSimpleLock::Owner lock(m_lock);

    m_dwSize = NewSize.LowPart;

    if (NULL != m_pBuffer)
        m_pBuffer = HeapReAlloc(GetProcessHeap(), 0, m_pBuffer, m_dwSize);
    else
        m_pBuffer = HeapAlloc(GetProcessHeap(), 0, m_dwSize);
}

DWORD CMyFile::Read(DWORD nCurrentPosition, void* pv, ULONG cb)
{
    CSimpleLock::Owner lock(m_lock);

    DWORD nBytesToRead;

    if (nCurrentPosition + cb <= m_dwSize)
        // nCurrentPosition ... nCurrentPosition + cb ... m_dwSize
        nBytesToRead = cb;
    else
        // nCurrentPosition ... m_dwSize ... nCurrentPosition + cb
        nBytesToRead = m_dwSize - nCurrentPosition;

    if (nBytesToRead > 0)
        CopyMemory(pv, (PBYTE)m_pBuffer + nCurrentPosition, nBytesToRead);

    return nBytesToRead;
}

DWORD CMyFile::Write(DWORD nCurrentPosition, const void* pv, ULONG cb)
{
    CSimpleLock::Owner lock(m_lock);

    if (nCurrentPosition + cb > m_dwSize)
        // Resize the buffer
    {
        // Recalculate size
        m_dwSize = nCurrentPosition + cb;

        if (NULL != m_pBuffer)
            m_pBuffer = HeapReAlloc(GetProcessHeap(), 0, m_pBuffer, m_dwSize);
        else
            m_pBuffer = HeapAlloc(GetProcessHeap(), 0, m_dwSize);
    }

    CopyMemory((PBYTE)m_pBuffer + nCurrentPosition, pv, cb);

    return cb;
}

// class CMyStream

CMyStream::CMyStream(CMyFile* pMyFile) : 
    m_nCurrentPosition(0), 
    m_nRefCount(1)
{
    m_pMyFile = pMyFile;
    m_pMyFile->AddRef();
}

CMyStream::~CMyStream()
{
    m_pMyFile->Release();
}

ULONG STDMETHODCALLTYPE CMyStream::AddRef()
{
    return InterlockedIncrement(&m_nRefCount);
}

ULONG STDMETHODCALLTYPE CMyStream::Release()
{
    ULONG nRefCount = InterlockedDecrement(&m_nRefCount);

    if (0 == nRefCount)
    {
        delete this;
    }

    return nRefCount;
}

HRESULT CMyStream::Seek(LARGE_INTEGER Distance, DWORD dwMoveMethod, ULARGE_INTEGER* NewPos)
{
    // Note: new position can be more than m_dwSize

    CSimpleLock::Owner lock(m_lock);

    LARGE_INTEGER new_pos = { 0 };

    if (STREAM_SEEK_SET == dwMoveMethod)
        new_pos.QuadPart = Distance.QuadPart;
    else if (STREAM_SEEK_CUR == dwMoveMethod)
        new_pos.QuadPart = Distance.QuadPart + m_nCurrentPosition;
    else if (STREAM_SEEK_END == dwMoveMethod)
        new_pos.QuadPart = m_pMyFile->GetSize() + Distance.QuadPart;

    m_nCurrentPosition = new_pos.LowPart;

    if (NewPos)
        NewPos->QuadPart = new_pos.QuadPart;

    return S_OK;
}

HRESULT CMyStream::SetSize(ULARGE_INTEGER newSize)
{
    CSimpleLock::Owner lock(m_lock);

    // SetSize (the current pointer should remain unchanged)
    m_pMyFile->ReAlloc(newSize);

    return S_OK;
}

HRESULT CMyStream::Stat(STATSTG* pstatstg, DWORD grfStatFlag)
{
    if (NULL == pstatstg)
        return E_INVALIDARG;

    ZeroMemory(pstatstg, sizeof(*pstatstg));
    pstatstg->type = STGTY_STREAM;
    pstatstg->cbSize.QuadPart = m_pMyFile->GetSize();

    return S_OK;
}

HRESULT STDMETHODCALLTYPE CMyStream::QueryInterface(REFIID iid, void** ppObject)
{
    *ppObject = NULL;

    if (IsEqualIID(iid, IID_IUnknown))
        *ppObject = this;
    else if (IsEqualIID(iid, IID_IStream))
        *ppObject = (IStream*)this;

    if (NULL != *ppObject)
    {
        AddRef();
        return S_OK;
    }
    else
    {
        return E_NOINTERFACE;
    }
}

HRESULT STDMETHODCALLTYPE CMyStream::Read(void* pv, ULONG cb, ULONG* pcbRead)
{
    CSimpleLock::Owner lock(m_lock);

    DWORD nReadBytes = m_pMyFile->Read(m_nCurrentPosition, pv, cb);

    if (NULL != pcbRead)
        *pcbRead = nReadBytes;

    m_nCurrentPosition += nReadBytes;

    return S_OK;
}

HRESULT STDMETHODCALLTYPE CMyStream::Write(const void* pv, ULONG cb, ULONG* pcbWritten)
{
    CSimpleLock::Owner lock(m_lock);

    DWORD nWrittenBytes = m_pMyFile->Write(m_nCurrentPosition, pv, cb);

    if (NULL != pcbWritten)
        *pcbWritten = nWrittenBytes;

    m_nCurrentPosition += nWrittenBytes;

    return S_OK;
}

HRESULT STDMETHODCALLTYPE CMyStream::Clone(IStream** ppstm)
{
    CMyStream* pMyStream = new CMyStream(m_pMyFile);

    HRESULT hr = pMyStream->QueryInterface(IID_IStream, (void**)ppstm);

    pMyStream->Release();

    return hr;
}

HRESULT STDMETHODCALLTYPE CMyStream::CopyTo(IStream* pstm, ULARGE_INTEGER cb, ULARGE_INTEGER* pcbRead, ULARGE_INTEGER* pcbWritten)
{
    return E_NOTIMPL;
}

HRESULT STDMETHODCALLTYPE CMyStream::Commit(DWORD grfCommitFlags)
{
    return E_NOTIMPL;
}

HRESULT STDMETHODCALLTYPE CMyStream::Revert()
{
    return E_NOTIMPL;
}

HRESULT STDMETHODCALLTYPE CMyStream::LockRegion(ULARGE_INTEGER libOffset, ULARGE_INTEGER cb, DWORD dwLockType)
{
    return E_NOTIMPL;
}

HRESULT STDMETHODCALLTYPE CMyStream::UnlockRegion(ULARGE_INTEGER libOffset, ULARGE_INTEGER cb, DWORD dwLockType)
{
    return E_NOTIMPL;
}

// class CSimpleLock::Owner

CSimpleLock::Owner::Owner(CSimpleLock& lock) : 
    m_lock(lock)
{
    m_lock.Lock();
}

CSimpleLock::Owner::~Owner()
{
    m_lock.Unlock();
}

void CSimpleLock::Lock()
{
    EnterCriticalSection(&m_cs);
}

void CSimpleLock::Unlock()
{
    LeaveCriticalSection(&m_cs);
}

CSimpleLock::CSimpleLock()
{
    InitializeCriticalSection(&m_cs);
}

CSimpleLock::~CSimpleLock()
{
    DeleteCriticalSection(&m_cs);
}

HRESULT CreateMyStream(IStream** ppStream)
{
    CMyFile* pMyFile = CMyFile::Create();

    *ppStream = pMyFile->CreateNewStream();

    pMyFile->Release();

    return S_OK;
}
