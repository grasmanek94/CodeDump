#include <iostream>
#include <fstream>
#include <string>

int main()
{
	std::ofstream redirect_out_test("C:\\Users\\Rafal\\Desktop\\RedirectTest.txt", std::ofstream::out);
	redirect_out_test << "TEST123456";
	redirect_out_test.close();
	std::ifstream redirect_in_test("C:\\Users\\Rafal\\Desktop\\RedirectTest2.txt", std::ofstream::in);
	std::string str;
	redirect_in_test >> str;
	redirect_in_test.close();

	std::cout << str << std::endl;

	int dummy;
	std::cin >> dummy;
	return 0;
}