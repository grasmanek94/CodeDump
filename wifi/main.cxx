#include <Windows.h>
#include <Wlanapi.h>
#include <iostream>

#pragma comment(lib, "Wlanapi.lib")

int main()
{
	HANDLE wlan = 0;
	DWORD negotiatedversion = 1;
	DWORD ret_WlanOpenHandle = WlanOpenHandle(1, NULL, &negotiatedversion, &wlan);
	DWORD err_WlanOpenHandle = GetLastError();
	if (ret_WlanOpenHandle == ERROR_SUCCESS)
	{
		WLAN_HOSTED_NETWORK_STATUS var_status;
		SecureZeroMemory(&var_status, sizeof(var_status));
		PWLAN_HOSTED_NETWORK_STATUS status = NULL;
		DWORD ret_WlanHostedNetworkQueryStatus = WlanHostedNetworkQueryStatus(wlan, &status, NULL);
		DWORD err_WlanHostedNetworkQueryStatus = GetLastError();

		std::cout <<	 "Return Value                   : " << ret_WlanHostedNetworkQueryStatus << std::endl;
		std::cout <<	 "Error  Value                   : " << err_WlanHostedNetworkQueryStatus << std::endl;
		if (status)
		{
			std::cout << "status->dot11PhyType           : " << (int)status->dot11PhyType << std::endl;
			std::cout << "status->dwNumberOfPeers        : " << (int)status->dwNumberOfPeers << std::endl;
			std::cout << "status->HostedNetworkState     : " << (int)status->HostedNetworkState << std::endl;
			std::cout << "status->IPDeviceID             : "
				<< std::hex
				<< (unsigned long)status->IPDeviceID.Data1 << "-"
				<< (unsigned long)status->IPDeviceID.Data2 << "-"
				<< (unsigned long)status->IPDeviceID.Data3 << "-"
				<< (unsigned long)status->IPDeviceID.Data4[0] << "-"
				<< (unsigned long)status->IPDeviceID.Data4[1] << "-"
				<< (unsigned long)status->IPDeviceID.Data4[2] << "-"
				<< (unsigned long)status->IPDeviceID.Data4[3] << "-"
				<< (unsigned long)status->IPDeviceID.Data4[4] << "-"
				<< (unsigned long)status->IPDeviceID.Data4[5] << "-"
				<< (unsigned long)status->IPDeviceID.Data4[6] << "-"
				<< (unsigned long)status->IPDeviceID.Data4[7]
				<< std::dec
				<< std::endl;
			std::cout << "status->ulChannelFrequency     : " << (int)status->ulChannelFrequency << std::endl;
			std::cout << "status->wlanHostedNetworkBSSID : " 
				<< std::hex
				<< (unsigned long)status->wlanHostedNetworkBSSID[0] << "-"
				<< (unsigned long)status->wlanHostedNetworkBSSID[1] << "-"
				<< (unsigned long)status->wlanHostedNetworkBSSID[2] << "-"
				<< (unsigned long)status->wlanHostedNetworkBSSID[3] << "-"
				<< (unsigned long)status->wlanHostedNetworkBSSID[4] << "-"
				<< (unsigned long)status->wlanHostedNetworkBSSID[5]
				<< std::dec
				<< std::endl;
		}
		WlanCloseHandle(wlan, NULL);
	}
	return 0;
}

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
	for (; i <= maxElem; ++i)
	{
		if (readout <= temps[i])
		{
			break;
		}
	}

	*interpolated = readout == temps[i];
	if (!*interpolated)
	{
		return ((double)i + 1.0)*5.0;
	}

	if (i == maxElem)
	{
		*error = true;
		return 0.0;
	}

	return
		((double)i + 1) * 5 + //basis temperatuur plus
		(5.0 * //standaard verschil maal
		(double)(temps[i] - readout) / (double)(temps[i] - temps[i + 1]));//percentage op naar volgende temperatuur
}

void setup()
{

}

void loop()
{
	bool error;
	bool interpolated;
	int readout = analogRead(A5);
	double temp = GetTemp(readout, &error, &interpolated);

	if (error)
	{
		Serial.println("ERROR");
	}
	else
	{
		Serial.print("Readval: ");
		Serial.print(readout);
		Serial.print(" Temperature: ");
		Serial.print((int)temp);
		if (interpolated)
		{
			Serial.print(" *");
		}
		Serial.println("");
	}
	delay(1000);
}