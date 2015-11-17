#include <iostream>
#include <Windows.h>

int main()
{
	const int GBS = 16;
	std::cout << "Allocating " << GBS << "GB of memory..." << std::flush;

	char* mem[GBS];
	for (int i = 0; i < GBS; ++i)
	{
		mem[i] = new char[1024 * 1024 * 1024];
		memset(mem[i], 0, 1024 * 1024 * 1024);
	}

	std::cout << " OK" << std::endl;

	while (true) { Sleep(1000000000); };
	return 0;
};