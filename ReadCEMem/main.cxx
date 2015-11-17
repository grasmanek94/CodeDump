#include <Windows.h>
//#include <string>
#include <iostream>
/*
class _myVector
{
public:
	float x;
	float y;
	float z;

	_myVector()
		: x(0.0), y(0.0), z(0.0)
	{

	}
	_myVector(const _myVector& other)
		: x(other.x), y(other.y), z(other.z)
	{

	}
	_myVector(float x, float y, float z)
		: x(x), y(y), z(z)
	{

	}
	_myVector& operator=(const _myVector& other)
	{
		this->x = other.x;
		this->y = other.y;
		this->z = other.z;
		return *this;
	}
	const _myVector operator-(const _myVector& right)
	{
		_myVector tmp;
		tmp.x = this->x - right.x;
		tmp.y = this->y - right.y;
		tmp.z = this->z - right.z;
		return tmp;
	}
	const _myVector operator+=(const _myVector& right)
	{
		this->x += right.x;
		this->y += right.y;
		this->z += right.z;
		return *this;
	}
	const _myVector operator+(const _myVector& right)
	{
		_myVector tmp;
		tmp.x = this->x + right.x;
		tmp.y = this->y + right.y;
		tmp.z = this->z + right.z;
		return tmp;
	}
	const _myVector operator/=(float div)
	{
		this->x /= div;
		this->y /= div;
		this->z /= div;
		return *this;
	}
	const _myVector operator/(float div)
	{
		_myVector tmp;
		tmp.x = this->x / div;
		tmp.y = this->y / div;
		tmp.z = this->z / div;
		return tmp;
	}
	const _myVector operator*(float mul)
	{
		_myVector tmp;
		tmp.x = this->x * mul;
		tmp.y = this->y * mul;
		tmp.z = this->z * mul;
		return tmp;
	}
};

const _myVector MoveAverF(const _myVector& x, unsigned int weight)
{
	static bool firstInsert = false;
	static bool firstMeasurements = false;
	static int insertedElems = 0;
	static _myVector movingAverage[50];
	static _myVector previousAverage;

	if (weight > 50) {
		weight = 50;
	}

	if (!firstInsert)
	{
		previousAverage = x;
		firstInsert = true;
	}

	movingAverage[insertedElems] = x;

	if (++insertedElems == weight)
	{
		insertedElems = 0;
		firstMeasurements = true;
	}

	if (firstMeasurements)
	{
		previousAverage = _myVector(0.0, 0.0, 0.0);
		for (int j = 0; j < weight; ++j)
		{
			previousAverage += movingAverage[j];
		}
	}

	previousAverage /= (float)weight;

	return previousAverage;
}
*/
/*struct handle_data {
	unsigned long process_id;
	HWND best_handle;
};

BOOL is_main_window(HWND handle)
{
	return GetWindow(handle, GW_OWNER) == (HWND)0 && IsWindowVisible(handle);
}

BOOL CALLBACK enum_windows_callback(HWND handle, LPARAM lParam)
{
	handle_data& data = *(handle_data*)lParam;
	unsigned long process_id = 0;
	GetWindowThreadProcessId(handle, &process_id);
	if (data.process_id != process_id || !is_main_window(handle)) {
		return TRUE;
	}
	data.best_handle = handle;
	return FALSE;
}

HWND find_main_window(unsigned long process_id)
{
	handle_data data;
	data.process_id = process_id;
	data.best_handle = 0;
	EnumWindows(enum_windows_callback, (LPARAM)&data);
	return data.best_handle;
}*/

unsigned long long a = 0x1122334455667788ULL;
unsigned long long * b = &a;
unsigned long long ** c = &b;
unsigned long long *** d = &c;
unsigned long long **** e = &d;
unsigned long long ***** f = &e;

struct something
{
	unsigned long long * g;
	unsigned long long ** h;
	unsigned long long *** i;
	unsigned long long **** j;
	unsigned long long ***** k;
};

something * h = new something();
something i;

int main()
{
	h->g = &a;
	h->h = &b;
	h->i = &c;
	h->j = &d;
	h->k = &e;
	i.h = &h->g;
	i.i = &h->h;
	i.j = &h->i;
	i.k = &h->j;
	i.k = &e;

	while (true)
	{
		std::cout << a << std::endl;
		Sleep(1000);
	}
	//std::cout << std::hex << "0x" << (unsigned long long)find_main_window(0x00001F34);
	//printf("Player: %016I64X PlayerPed: %016I64X", 0x1234567890123456ULL, 0x6543210987654321ULL);
	//char data[12];
	//data[3] = ??
	//data[4] = ??
	//data[5] = ??
	//data[6] = ??

	//float a = 1.0;
	//memcpy(data + 3, &a, sizeof(a));
	//
	////lezen:
	//float b;
	//memcpy(&b, data + 3, sizeof(a));
	//
	//std::cout << b << std::endl;
	//_myVector reading(0.0, 0.0, 0.0);
	//HANDLE proc = OpenProcess(PROCESS_ALL_ACCESS | PROCESS_VM_READ, TRUE, 4444);
	//int address = 0x06239078;
	//std::string buffer;
	//buffer.reserve(16 * 1024 * 1024);//16 MB
	//
	//char readchar[2] = { 0, 0 };
	//for (;; ++address)
	//{
	//	if (!ReadProcessMemory(proc, (void*)address, readchar, 1, NULL))
	//	{
	//		int error = GetLastError();
	//		std::cout << "ERROR " << error;
	//		return 0;
	//	}
	//
	//	if (readchar[0])
	//	{
	//		buffer.append(readchar);
	//	}
	//	else
	//	{
	//		break;
	//	}
	//}
	//
	//std::cout << buffer;
	int i;
	std::cin >> i;
	return 0;
}