#include <dxgi.h>
#include "detours.h"
#include <cstdio>

//////////////////////////////////////////////////////////////// Target Class.
//
class CMember
{
public:
	void Target(void);
};

void CMember::Target(void)
{
	printf("  CMember::Target!   (this:%p)\n", this);
}

//////////////////////////////////////////////////////////////// Detour Class.
//
class CDetour /* add ": public CMember" to enable access to member variables... */
{
public:
	void Mine_Target(void);
	static void (CDetour::* Real_Target)(void);

	// Class shouldn't have any member variables or virtual functions.
};

void CDetour::Mine_Target(void)
{
	printf("  CDetour::Mine_Target! (this:%p)\n", this);
	(this->*Real_Target)();
}

void (CDetour::* CDetour::Real_Target)(void) = (void (CDetour::*)(void))&CMember::Target;

//////////////////////////////////////////////////////////////////////////////
//
int main()
{
	printf("\n");

	DetourTransactionBegin();
	DetourUpdateThread(GetCurrentThread());

	auto x = &CDetour::Mine_Target;
	DetourAttach(&(PVOID&)CDetour::Real_Target,
		*(PVOID*)(&x));

	LONG l = DetourTransactionCommit();
	printf("DetourTransactionCommit = %d\n", l);
	printf("\n");

	printf("\n");

	//////////////////////////////////////////////////////////////////////////
	//
	CMember target;

	printf("Calling CMember (w/o Detour):\n");
	(((CDetour*)&target)->*CDetour::Real_Target)();

	printf("Calling CMember (will be detoured):\n");
	target.Target();

	while (true);
	return 0;
}