#include <iostream>
#include <fstream>
#include <sstream>
#include <string>
#include <vector>
#include <concurrent_vector.h>
#include <thread>

std::vector<char> data;

struct needle
{
	std::vector<char>::iterator begin;
	std::vector<char>::iterator end;
	needle(const std::vector<char>::iterator& begin, const std::vector<char>::iterator& end)
		: begin(begin), end(end)
	{}
};

struct result
{
	bool reverse;
	size_t occurences;
	size_t size;
	needle search;
	result(const bool& reverse, const size_t& size, const size_t& occurences, const needle& search)
		: reverse(reverse), size(size), search(search), occurences(occurences)
	{}
};

Concurrency::concurrent_vector<result> results;

void Worker(bool reverse, size_t size)
{
	std::vector<char> needle;
}

int main(int argc, char** args)
{
	std::ifstream input_data("data.in", std::ios::in | std::ios::binary);

	input_data.seekg(0, std::ios::end);
	std::streampos length(input_data.tellg());

	if (length < 1) 
	{
		std::cout << "Error: read " << length << " bytes." << std::endl;
		return 1;
	}

	input_data.seekg(0, std::ios::beg);
	data.resize(static_cast<std::size_t>(length));
	input_data.read(&data.front(), static_cast<std::size_t>(length));

	std::cout << "Successfully read " << length << " bytes." << std::endl;


	return 0;
}