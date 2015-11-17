#include <iostream>
#include <fstream>
#include <string>
#include <sstream>
#include <algorithm>

int main()
{
	std::ifstream ibxsdk[2] =
	{
		std::ifstream("./bxsdk32.dll", std::ifstream::in | std::ifstream::binary),
		std::ifstream("./bxsdk64.dll", std::ifstream::in | std::ifstream::binary)
	};

	std::ofstream obxsdk[2] =
	{
		std::ofstream("./bxsdk32.h", std::ifstream::out | std::ifstream::trunc),
		std::ofstream("./bxsdk64.h", std::ifstream::out | std::ifstream::trunc)
	};

	std::string header;

	for (int f = 0; f < 2; ++f)
	{
		header = "char bxsdk32dll[] = \n{\n\t";
		obxsdk[f] << header;

		unsigned char c;
		int i = 0;
		do
		{
			c = ibxsdk[f].get();

			std::stringstream stream;
			stream << std::hex << (int)c;
			std::string result(stream.str());
			std::transform(result.begin(), result.end(), result.begin(), ::toupper);
			if (result.length() == 1)
			{
				result = "0x0" + result + ", ";
			}
			else
			{
				result = "0x" + result + ", ";
			}
			if (++i % 128 == 0)
			{
				result += "\n\t";
			}
			obxsdk[f] << result;
		} 
		while (ibxsdk[f].good());

		header = "\n};\n";
		obxsdk[f] << header;

		obxsdk[f].close();
		ibxsdk[f].close();
	}
	return 0;
}