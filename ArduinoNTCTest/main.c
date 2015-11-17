#include <stdio.h>
#include <stdbool.h>

int temps[] =
	{ 737, 684, 628, 570, 512, 456, 402, 353 };

double GetTemp(int readout, bool *error, bool *interpolated)
{
	int maxElem = sizeof(temps) / sizeof(int) - 1;
	if (readout > temps[0] || readout < temps[maxElem])
	{
		*error = true;
		*interpolated = false;
		return 0.0;
	}

	int i = 0;
	int tempsi;
	for (; i <= maxElem; ++i)
	{
		tempsi = temps[i];
		if (readout >= tempsi)
		{
			break;
		}
	}

	if (readout == tempsi)
	{
		*interpolated = false;
		return ((double)i + 1.0)*5.0;
	}
	*interpolated = true;

	if (i == maxElem)
	{
		*error = true;
		return 0.0;
	}

	int tempsipo = temps[i + 1];
	double basetemp = ((double)i + 1) * 5.0;
	double percentage2next = (double)(tempsi - readout) / (double)(tempsi - tempsipo);
	double toAddFromPercentage = 5.0 * percentage2next;
	double toRet = basetemp + toAddFromPercentage;

	return toRet;
}

int main()
{
	//some usable range
	for (int i = 750; i > 350; --i)
	{
		bool error = false;
		bool interpolated = false;
		int readout = i;
		double temp = GetTemp(readout, &error, &interpolated);

		if (error)
		{
			//printf("ERROR\n");
		}
		else
		{
			printf("Readval: %d Temperature: %02.1f %c\n", readout, temp, interpolated ? '*' : ' ');
		}
	}

	return getchar();
}
