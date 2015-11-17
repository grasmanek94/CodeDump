#include <string>
#include <ctime>
#include <iostream>
#include <string>
#include <array>
#include <fstream>

#include "SharedMP.hxx"

#include "script.hxx"
#include "CustomConsole.hxx"
#include "KeyboardManager.hxx"

#include "GTAV.hxx"

#include <Windows.h>

#include <memory>

//std::unique_ptr<CMultiplayer> Multiplayer = std::make_unique<CMultiplayer>();

//std::unique_ptr<GTA::V> game = nullptr;
std::unique_ptr<GTA::CustomHelpText> cht = nullptr;
std::unique_ptr<GTA::CustomKeyboardText> ckt = nullptr;

bool activated = false;
std::string lastresult("----------");

void OnKeyStateChange(bool pressed, unsigned char vkey)
{
	if (pressed)
	{
		/*const std::vector<unsigned char> join_pattern	({VK_CONTROL, VK_CONTROL, 'K', 'J', VK_RETURN});
		const std::vector<unsigned char> server_pattern ({ VK_CONTROL, VK_CONTROL, 'K', 'K', VK_RETURN });
		const std::vector<unsigned char> server_pattern2({ VK_CONTROL, VK_CONTROL, 'K', 'L', VK_RETURN });

		if (activated && vkey == VK_RETURN)
		{
			activated = false;
			cht->End();
			kbmgr.ClearBuffer();
			kbmgr.BackspaceTextEditing = false;
		}
		else if (!activated)
		{
			if(kbmgr.BufferContainsArray(join_pattern))
			{
				activated = true;

				cht->Begin();
				kbmgr.ClearBuffer();
				kbmgr.BackspaceTextEditing = true;
			}
			else if(kbmgr.BufferContainsArray(server_pattern))
			{
				activated = true;
				GAMEPLAY::DISPLAY_ONSCREEN_KEYBOARD(0, "CELL_EMAIL_BOD", "", "", "", "", "", 4096);
				cht->ShowTimedText("1 Activated", 1000);
			}
			else if (kbmgr.BufferContainsArray(server_pattern2))
			{
				activated = true;
				GAMEPLAY::DISPLAY_ONSCREEN_KEYBOARD(0, "CELL_EMAIL_BODE", "", "", "", "", "", 4096);
				cht->ShowTimedText("2 Activated", 1000);
			}
		}
		if (activated)
		{
			cht->SetText(std::string("IP:PORT/HOST? -> ") + (char*)kbmgr.GetSequence(128, false).data());
		}*/
		if (vkey == VK_RETURN)
		{
			activated ^= 1;
			if (activated)
			{
				if (kbmgr.Down('S'))
				{
					ckt->DISPLAY_ONSCREEN_KEYBOARD(0, L"OtherText HelloLol!", "", "", "", "", "", 500);
				}
				else
				{
					ckt->DISPLAY_ONSCREEN_KEYBOARD(0, L"InitialText 1234567890", "", "", "", "", "", 500);
				}
			}
		}
	}
}

void main()
{
	EnableCustomConsole();

	//game = std::unique_ptr<GTA::V>(GTA::V::Setup());
	cht = std::make_unique<GTA::CustomHelpText>("GZ-LGM| ");
	ckt = std::make_unique<GTA::CustomKeyboardText>();

	//printf("Game Base Addr: 0x%p\r\n", game);

	kbmgr.SetOnKeyStateChangeFunction(OnKeyStateChange);

	KeyboardManager::TACS safetyMechanism;
	while (true)
	{
		while (kbmgr.AntiCrashSychronization.try_pop(safetyMechanism))
		{
			kbmgr.CheckKeys(safetyMechanism.first, safetyMechanism.second);
		}

		//Multiplayer->Process();

		switch (GAMEPLAY::UPDATE_ONSCREEN_KEYBOARD())
		{
		case 0://0 - User still editing
			break;
		case 1://1 - User has finished editing
			break;
		case 2://2 - User has canceled editing
			break;
		case 3://3 - Keyboard isn't active
			break;
		}

		char* result = GAMEPLAY::GET_ONSCREEN_KEYBOARD_RESULT();
		if (result)
		{
			lastresult.assign(result);
			cht->ShowTimedText("TextUpdate: " + lastresult, 1000);
		}

		cht->Tick();

		WAIT(0);
	}
}

void ScriptMain()
{
	srand(GetTickCount());
	main();
}