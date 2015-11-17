#include <iostream>
#include <vector>
#include <string>
#include <boost/filesystem.hpp>

std::vector<std::vector<char>> replaceables;

enum program_mode
{
	pm_encrypt,
	pm_decrypt,
	pm_table
};

int cpp_main(const std::vector<std::string>& args)
{
	if (args.size() != 7)
	{
		std::cerr << "This program needs 6 arguments:\r\nprogram.exe e[ncode]/d[ecode] ratio table input output seed[=0 for random or decode]\r\ne.g.: program.exe e 15 table.secret input.txt output.dat 0" << std::endl;
		std::cerr << "Or if you want to generate a table:\r\nprogram.exe t[able] ratio output seed[=0 for random]\r\ne.g.: program.exe t 15 table.secret 0" << std::endl;
		return 1;
	}

	program_mode mode;
	if (args[1][0] == 'e' || args[1][0] == 'E')
	{
		mode = pm_encrypt;
	}
	else if (args[1][0] == 'd' || args[1][0] == 'D')
	{
		mode = pm_decrypt;
	}
	else if (args[1][0] == 't' || args[1][0] == 'T')
	{
		mode = pm_table;
	}
	else
	{
		std::cerr << "To encode or not to encode, or to make a table, what will it be?" << std::endl;
		return 2;
	}

	int ratio = std::stoi(args[2]);

	if (ratio < 2 || ratio > 1000)
	{
		std::cerr << "What kind of weird ratio do you want?" << std::endl;
		return 3;
	}

	replaceables.resize(0x100);
	for (auto& i : replaceables)
	{
		i.resize(ratio);
	}
	while (true);
	return 0;
}

int main(int argc, char** args)
{
	std::vector<std::string> args_vec;
	for (int i = 0; i < argc; ++i)
	{
		args_vec.push_back(std::string(args[i]));
	}
	return cpp_main(args_vec);
}