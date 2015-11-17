#include <algorithm>
#include <iostream>
#include <vector>

class SomeClass
{
private:
	char x;
public:
	SomeClass()
		: x(rand() % 0x100)
	{ }

	char GetX()
	{
		return x;
	}
};

class VectorOptimizationTest
{
	std::vector<SomeClass*> someVector;
private:

public:
	VectorOptimizationTest(size_t number)
	{
		for (size_t i = 0; i < number; ++i)
		{
			someVector.push_back(new SomeClass());
		}
	}

	~VectorOptimizationTest()
	{
		while(someVector.size())
		{
			delete someVector[0];
			someVector.erase(someVector.begin());
		}
	}

	size_t GetIdxAt()
	{
		for (size_t i = 0; i < someVector.size(); ++i)
		{
			if (someVector.at(i)->GetX() == 0x15)
			{
				return i;
			}
		}
		return (size_t)-1;
	}

	size_t GetIdxRaw()
	{
		for (size_t i = 0; i < someVector.size(); ++i)
		{
			if (someVector[i]->GetX() == 0x15)
			{
				return i;
			}
		}
		return (size_t)-1;
	}
};

VectorOptimizationTest * test = nullptr;

int main()
{
	test = new VectorOptimizationTest(0x100);
	{
		std::cout << "<<< AT  : " << test->GetIdxAt()  << " >>>" << std::endl;
		std::cout << "<<< RAW : " << test->GetIdxRaw() << " >>>" << std::endl;
	}
	delete test;
	while (true);
	return 0;
}