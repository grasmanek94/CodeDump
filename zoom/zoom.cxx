#include <iostream>

void clampPos(const double current_x, const double current_y, const double center_x, const double center_y, const double maxdist, double &new_x, double &new_y)
{
	double dist = pow(pow(current_x - center_x, 2.0) + pow(current_y - center_y, 2.0), 0.5);

	if (dist < maxdist)
	{
		new_x = current_x;
		new_y = current_y;
		return;
	}

	double angle = atan2(center_y - current_y, center_x - current_x);

	new_x = center_x - cos(angle) * maxdist;
	new_y = center_y + sin(-angle) * maxdist;
}

void PrintTestClamp(const double current_x, const double current_y, const double center_x, const double center_y, const double maxdist)
{
	double 
		new_x, 
		new_y;
	clampPos(current_x, current_y, center_x, center_y, maxdist, new_x, new_y);
	std::cout << "clampPos(" << current_x << ", " << current_y << ", " << center_x << ", " << center_y << ", " << maxdist << ", " << new_x << ", " << new_y << ")" << std::endl;
}

int main()
{
	PrintTestClamp(	2.0,	0.0,	0.0,	0.0,	1.0);
	PrintTestClamp(	0.0,	2.0,	0.0,	0.0,	1.0);
	PrintTestClamp(	-2.0,	0.0,	0.0,	0.0,	1.0);
	PrintTestClamp(	0.0,	-2.0,	0.0,	0.0,	1.0);

	std::cout << std::endl;

	PrintTestClamp(	2.0,	2.0,	0.0,	0.0,	1.0);
	PrintTestClamp(	-2.0,	-2.0,	0.0,	0.0,	1.0);
	PrintTestClamp(	-2.0,	2.0,	0.0,	0.0,	1.0);
	PrintTestClamp(	2.0, -	2.0,	0.0,	0.0,	1.0);

	std::cout << std::endl;

	PrintTestClamp(	2.0,	0.0,	1.0,	2.0,	1.0);
	PrintTestClamp(	0.0,	2.0,	1.0,	2.0,	1.0);
	PrintTestClamp(	-2.0,	0.0,	1.0,	2.0,	1.0);
	PrintTestClamp(	0.0,	-2.0,	1.0,	2.0,	1.0);

	std::cout << std::endl;

	PrintTestClamp(	2.0,	2.0,	1.0,	2.0,	1.0);
	PrintTestClamp(	-2.0,	-2.0,	1.0,	2.0,	1.0);
	PrintTestClamp(	-2.0,	2.0,	1.0,	2.0,	1.0);
	PrintTestClamp(	2.0,	-2.0,	1.0,	2.0,	1.0);

	std::cout << std::endl;

	getchar();
	return 0;
}