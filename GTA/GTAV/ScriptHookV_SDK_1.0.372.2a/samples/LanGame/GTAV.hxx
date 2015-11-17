/*
	Authors: 
		- BartekDVD
		- Gamer_Z/grasmanek94
*/
#pragma once

#include <enet/enetpp.hxx>
#include <Windows.h>
#include <Psapi.h>
#include <iostream>
#include <vector>
#include <array>
#include <set>
#include <memory>
#include <functional>
#include "script.hxx"

#pragma comment(lib, "Psapi.lib")

namespace GTA
{
	#define MACRO_COMBINE_INTERNAL(X,Y) X##Y
	#define MACRO_COMBINE(X,Y) MACRO_COMBINE_INTERNAL(X,Y)

	#ifdef __COUNTER__
	#define PAD(bytes) char MACRO_COMBINE(MACRO_COMBINE(__PAD__,__COUNTER__),[(bytes)])
	#elif defined __LINE__
	#define PAD(bytes) char MACRO_COMBINE(MACRO_COMBINE(__PAD__,__LINE__),[(bytes)])
	#else
	#error Your Compiler Sucks
	#endif

	template <typename T> struct Vector2
	{
		T x;
		T y;
	};

	template <typename T> struct Vector3
	{
		T x;
		T y;
		T z;

		Vector3()
			: x(T()), y(T()), z(T())
		{ }

		Vector3(T x, T y, T z)
			: x(x), y(y), z(z)
		{ }

		Vector3<T> operator*(T rhs)
		{
			return Vector3(x * rhs, y * rhs, z * rhs);
		} 

		Vector3<T>& operator*=(T rhs)
		{
			x *= rhs;
			y *= rhs;
			z *= rhs;

			return *this;
		}
	};

	template <typename T> struct Vector4
	{
		T x;
		T y;
		T z;
		T w;

		Vector4()
			: x(T()), y(T()), z(T()), w(T())
		{ }

		Vector4(T x, T y, T z, T w)
			: x(x), y(y), z(z), w(w)
		{ }

		Vector4<T> operator*(T rhs)
		{
			return Vector4(x * rhs, y * rhs, z * rhs, w * rhs);
		}

		Vector4<T>& operator*=(T rhs)
		{
			x *= rhs;
			y *= rhs;
			z *= rhs;
			w *= rhs;

			return *this;
		}
	};

	template <typename T, typename U> T ref(U what)
	{
		return (T)what;
	}

	template <typename T, typename U> T deref(U what)
	{
		size_t * addr = (size_t*)what;
		if (addr == nullptr)
		{
			return nullptr;
		}
		return (T)*addr;
	}

	class PedControlData	// [ [ [ [ [ [ [ [GTA5.EXE + 0x2210458] + 0x0 ] + 0x1070 + ID * 0x18E0 ] + 0x360 ] + 0x40 ] + [{Next()->Next()->...}] + 0x10 ] + 0x50 ] + 0x50 ]
	{
		PedControlData()
		{
			static_assert(sizeof(PedControlData) == 0x270, "PedControlData != OK");
		}
	public:
		PAD(0x2C);
		unsigned long MOVEMENT; //ALLOW MOVEMENT ONLY IF THIS VALUE EQUALS 0x3C28E83F
		Vector2<float> CameraSomething_1;
		PAD(0x08);
		Vector2<float> CameraSomething_2;
		PAD(0x08);
		Vector2<float> RotationSomething;
		PAD(0x08);
		Vector3<float> Position;
		PAD(0x04);
		Vector3<float> Velocity;
		PAD(0x04);//some counter? in float?
		PAD(0x54);
		float MovementSpeed; //def = 150.0, max = 150.0
		PAD(0x08);
		Vector3<float> SameAsVelocity_1;
		PAD(0x04);//some counter? in float?
		PAD(0x10);
		Vector3<float> SameAsVelocity_2;
		PAD(0x04);//some counter? in float?
		PAD(0x20);
		Vector2<float> SameAsCameraSomething_1_1;
		PAD(0x08);
		Vector2<float> SameAsCameraSomething_2_1;
		PAD(0x18);
		Vector3<float> SameAsPosition_1;
		PAD(0x04);
		Vector2<float> SameAsCameraSomething_2_2;
		PAD(0x18);
		Vector3<float> SameAsPosition_2;
		PAD(0x84);
		PAD(0x20);//3C28E83F
		PAD(0x30);
	};

	class EntityProxyB		//   [ [ [ [ [ [ [GTA5.EXE + 0x2210458] + 0x0 ] + 0x1070 + ID * 0x18E0 ] + 0x360 ] + 0x40 ] + [{Next()->Next()->...}] + 0x10 ] + 0x50 ]
	{
		PAD(0x50);
		PedControlData* _PedControlData;
	public:
		PedControlData* GetController()
		{
			return ref<PedControlData*>(_PedControlData);
		}
	};

	class EntityProxyA		//     [ [ [ [ [ [GTA5.EXE + 0x2210458] + 0x0 ] + 0x1070 + ID * 0x18E0 ] + 0x360 ] + 0x40 ] + [{Next()->Next()->...}] + 0x10 ]
	{
		PAD(0x50);
		EntityProxyB* _EntityProxyB;
		PAD(0x3C);
	public:
		Vector3<float> SomePosition;
	private:
		PAD(0x70);
	public:
		Vector3<float> SameAsSomePosition_1; //0x110
	private:
		PAD(0x04);
	public:
		Vector3<float> SameAsSomePosition_2; //0x120
		EntityProxyB* GetProxyB()
		{
			return ref<EntityProxyB*>(_EntityProxyB);
		}
	};

	class EntityLinkedList	//       [ [ [ [ [GTA5.EXE + 0x2210458] + 0x0 ] + 0x1070 + ID * 0x18E0 ] + 0x360 ] + 0x40 ]
	{
		PAD(0x10);
		EntityProxyA* _EntityProxyA;
		PAD(0x08);
		EntityLinkedList* _NextData;
	public:
		EntityLinkedList* Next()
		{
			return ref<EntityLinkedList*>(_NextData);
		}
		EntityProxyA* GetProxyA()
		{
			return ref<EntityProxyA*>(_EntityProxyA);
		}
	};

	class EntityDataTable	//         [ [ [ [GTA5.EXE + 0x2210458] + 0x0 ] + 0x1070 + ID * 0x18E0 ] + 0x360 ]
	{
		const static size_t ID = 1;
	public:
		EntityLinkedList* GetLinkedLists()
		{
			return deref<EntityLinkedList*>((size_t)this + 0x20 + ID * 0x20);
		}
	};

	class VehicleControlData// [ [ [ [ [ [ [ [GTA5.EXE + 0x2210458] + 0x0 ] + 0x1070 + ID * 0x18E0 ] + 0xF8 ] + 0x2C8 ] + 0x50 ] + 0x48 ] + 0x0 ]
	{
	public:
		PAD(0x50);
		Vector3<float> Position;
	};

	class CarDataProxyD		//   [ [ [ [ [ [ [GTA5.EXE + 0x2210458] + 0x0 ] + 0x1070 + ID * 0x18E0 ] + 0xF8 ] + 0x2C8 ] + 0x50 ] + 0x48 ]
	{
		VehicleControlData* _VehicleControlData;
	public:
		VehicleControlData* GetVehicleControlData()
		{
			return ref<VehicleControlData*>(_VehicleControlData);
		}
	};

	class CarDataProxyC		//     [ [ [ [ [ [GTA5.EXE + 0x2210458] + 0x0 ] + 0x1070 + ID * 0x18E0 ] + 0xF8 ] + 0x2C8 ] + 0x50 ]
	{
		PAD(0x48);
		CarDataProxyD* _CarDataProxyD;
	public:
		CarDataProxyD* GetProxyD()
		{
			return ref<CarDataProxyD*>(_CarDataProxyD);
		}
	};

	class CarDataProxyB		//       [ [ [ [ [GTA5.EXE + 0x2210458] + 0x0 ] + 0x1070 + ID * 0x18E0 ] + 0xF8 ] + 0x2C8 ]
	{
		PAD(0x50);
		CarDataProxyC* _CarDataProxyC;
	public:
		CarDataProxyC* GetProxyC()
		{
			return ref<CarDataProxyC*>(_CarDataProxyC);
		}
	};

	class CarDataProxyA		//         [ [ [ [GTA5.EXE + 0x2210458] + 0x0 ] + 0x1070 + ID * 0x18E0 ] + 0xF8 ]
	{
		PAD(0x2C8);
		CarDataProxyB* _CarDataProxyB;
	public:
		CarDataProxyB* GetProxyB()
		{
			return ref<CarDataProxyB*>(_CarDataProxyB);
		}
	};

	class EntityBase		//           [ [ [GTA5.EXE + 0x2210458] + 0x0 ] + 0x1070 + ID * 0x18E0 ]
	{
		EntityBase()
		{
			static_assert(sizeof(EntityBase) == 0x368, "EntityBase != OK");
		}
		PAD(0xF8);
		CarDataProxyA* _CarDataProxyA;
		PAD(0x0258); // cast to base (from inherited) [or reverse?]
		PAD(0x08); //some kind of index /offset or something in the class
		EntityDataTable* _EntityDataTable;
	public:
		EntityDataTable* GetDataTable()
		{
			return ref<EntityDataTable*>(_EntityDataTable);
		}
		CarDataProxyA* GetCarDataProxyA()
		{
			return ref<CarDataProxyA*>(_CarDataProxyA);
		}
	};

	class PlayerPedInfo		//               [GTA5.EXE + ??????]
	{
		PAD(0x98);
		unsigned char data[4];
	public:
		size_t PedID()
		{
			return (size_t)data[1];
		}
	};

	class EntityManager		//             [ [GTA5.EXE + 0x2210458] + 0x0 ]
	{
		const static size_t Offset = 0x1070;
		const static size_t Incrementor = 0x18E0;

	public:
		EntityBase* GetEntityBase(size_t entity_index)
		{
			return deref<EntityBase*>((size_t)this + EntityManager::Offset + (entity_index * EntityManager::Incrementor));
		}
	};

	class Game				//               [GTA5.EXE + 0x2210458]
	{
		EntityManager* _EntityManager;		//						+ 0x0
	public:
		PedControlData* GetEntityPedControlData(size_t entity_index)
		{
			EntityManager* manager = this->GetManager();
			if (manager == nullptr)
			{
				return nullptr;
			}

			EntityBase* base = manager->GetEntityBase(entity_index);
			if (base == nullptr)
			{
				return nullptr;
			}

			EntityDataTable* table = base->GetDataTable();
			if (table == nullptr)
			{
				return nullptr;
			}

			EntityLinkedList* list = table->GetLinkedLists();

			while (list != nullptr)
			{
				EntityProxyA* ProxyA = list->GetProxyA();
				if (ProxyA != nullptr)
				{
					EntityProxyB* ProxyB = ProxyA->GetProxyB();
					if (ProxyB != nullptr)
					{
						PedControlData* pcd = ProxyB->GetController();
						if (pcd != nullptr)
						{
							return pcd;
						}
					}
				}
				list = list->Next();
			}

			return nullptr;
		}

		VehicleControlData* GetEntityVehicleControlData(size_t entity_index)
		{
			EntityManager* manager = this->GetManager();
			if (manager == nullptr)
			{
				return nullptr;
			}

			EntityBase* base = manager->GetEntityBase(entity_index);
			if (base == nullptr)
			{
				return nullptr;
			}

			CarDataProxyA* ProxyA = base->GetCarDataProxyA();
			if (ProxyA == nullptr)
			{
				return nullptr;
			}

			CarDataProxyB* ProxyB = ProxyA->GetProxyB();
			if (ProxyB == nullptr)
			{
				return nullptr;
			}

			CarDataProxyC* ProxyC = ProxyB->GetProxyC();
			if (ProxyB == nullptr)
			{
				return nullptr;
			}

			CarDataProxyD* ProxyD = ProxyC->GetProxyD();
			if (ProxyD == nullptr)
			{
				return nullptr;
			}

			return ref<VehicleControlData*>(ProxyD->GetVehicleControlData());
		}

		EntityManager* GetManager()
		{
			return ref<EntityManager*>(_EntityManager);
		}
	};

	class PatternScanner
	{
	public:
		static bool memory_compare(const BYTE *data, const BYTE *pattern, const char *mask)
		{
			for (; *mask; ++mask, ++data, ++pattern)
			{
				if (*mask == 'x' && *data != *pattern)
				{
					return false;
				}
			}
			return (*mask) == NULL;
		}

		static size_t FindPattern(std::vector<unsigned short> pattern)
		{
			size_t i;
			size_t size;
			size_t address;

			MODULEINFO info = { 0 };

			address = (size_t)GetModuleHandle(NULL);
			GetModuleInformation(GetCurrentProcess(), GetModuleHandle(NULL), &info, sizeof(MODULEINFO));
			size = (size_t)info.SizeOfImage;

			std::vector<unsigned char> search;
			std::vector<char> mask;

			for (auto elem : pattern)
			{
				if (elem != 0xFFFF)
				{
					search.push_back((unsigned char)elem);
					mask.push_back('x');
				}
				else
				{
					search.push_back(0);
					mask.push_back('?');
				}
			}

			for (i = 0; i < size; ++i)
			{
				if (memory_compare((BYTE *)(address + i), (BYTE *)search.data(), mask.data()))
				{
					return (UINT64)(address + i);
				}
			}
			return 0;
		}
	};

	class V					//                GTA5.EXE + 0x2210458
	{
		Game* _Game;
	public:
		static V* Setup()
		{
			//-0xFB
			std::vector<unsigned short> base_pattern = 
			{
				0x00, 0x00, 0x00, 0x90, 0xFFFF, 0xFFFF, 0xFFFF, 0xFFFF, 0x00, 0x00, 0x00, 0xFFFF, 0xFFFF, 0xFFFF, 0xFFFF, 0xFFFF, 0x00, 0x00, 0x00, 0x08, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x03, 
				0x00, 0x00, 0x00, 0xFFFF, 0x00, 0x00, 0x00, 0xF0, 0xFFFF, 0xFFFF, 0xFFFF, 0xFFFF, 0x00, 0x00, 0x00, 0x0D, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 
				0x00, 0x00, 0x03, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xFFFF, 0xFFFF, 0xFFFF, 0xFFFF, 0xFFFF, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
				0x00, 0x02, 0x00, 0x00, 0x00
			};
	
			//return (V*)((size_t)GetModuleHandle(NULL) + (size_t)0x2210458);//steam 393
			return ref<V*>(PatternScanner::FindPattern(base_pattern) + (size_t)0xFB);
		}
		Game* GetGame()
		{
			return ref<Game*>(_Game);
		}
	};


	class CustomHelpText
	{
		const unsigned short bytes[3] =
		{
			0x4B75,
			0x1D75,
			0x9090
		};

		char* source_string;

		unsigned short* jumps[2];
		DWORD protections[2];

		size_t timer;

		bool Drawing()
		{
			return ((char*)jumps[0])[0] == 0x90;
		}

		std::string TAG;
	public:
		CustomHelpText(const std::string& TAG)
			: TAG(TAG)
		{
			timer = 0;

			size_t offset_addr = PatternScanner::FindPattern(std::vector<unsigned short>(
			{
				0x41, 0xF6, 0xC0, 0x01, 
				0x75, 0x43, 0x83, 0xF9, 
				0x03, 0x7D, 0x3E
			}));

			size_t offset = *((unsigned long*)(offset_addr + 0x23));
			source_string = (char*)((size_t)GetModuleHandle(NULL) + offset);

			size_t begin_addr = PatternScanner::FindPattern(std::vector<unsigned short>(
			{
				0x44, 0x8B, 0xC7, 0x48, 
				0x8B, 0xD6, 0x49, 0x8B, 
				0xCB, 0x89, 0x44
			}));

			jumps[0] = (unsigned short*)(begin_addr + 0x28);
			jumps[1] = (unsigned short*)(begin_addr + 0x56);

			VirtualProtect((LPVOID)jumps[0], 2, PAGE_EXECUTE_READWRITE, &protections[0]);
			VirtualProtect((LPVOID)jumps[1], 2, PAGE_EXECUTE_READWRITE, &protections[1]);
		}

		~CustomHelpText()
		{
			unsigned long dwProtect;
		
			*jumps[0] = bytes[0];
			*jumps[1] = bytes[1];

			VirtualProtect((LPVOID)jumps[0], 2, protections[0], &dwProtect);
			VirtualProtect((LPVOID)jumps[1], 2, protections[1], &dwProtect);
		}

		void Begin()
		{
			if (!Drawing())
			{
				*jumps[0] = bytes[2];
				*jumps[1] = bytes[2];

				timer = 0;
			}
		}

		void SetText(const std::string& text)//char limit is ~599
		{
			std::string combined(TAG + text);
			memcpy(source_string, combined.c_str(), combined.size() + 1);
		}

		void ShowThisFrame()
		{
			UI::DISPLAY_HELP_TEXT_THIS_FRAME("LETTERS_HELP2", false);
		}

		void End()
		{
			if (Drawing())
			{
				source_string[0] = 0;

				*jumps[0] = bytes[0];
				*jumps[1] = bytes[1];

				UI::HIDE_HELP_TEXT_THIS_FRAME();

				timer = 0;
			}
		}

		void Tick()
		{
			if (timer)
			{
				size_t time_now = GetTickCount();
				if (time_now < timer)
				{
					ShowThisFrame();
				}
				else
				{
					End();
				}
			}
		}

		void ShowTimedText(const std::string& text, size_t how_many_ms)
		{
			Begin();
			SetText(text);
			timer = (size_t)GetTickCount() + how_many_ms;
		}
	};

	class CustomKeyboardText
	{
		wchar_t* title;
		char bytes[5];
		unsigned long dwProtect;
		char* calladdr;
	public:

		CustomKeyboardText()
		{
			size_t begin_addr = PatternScanner::FindPattern(std::vector<unsigned short>(
			{
				0x41, 0xB8, 0x40, 0x00,
				0x00, 0x00, 0x48, 0x8B,
				0xD0, 0x48, 0x8B, 0xCF
			}));

			size_t offset = *((unsigned long*)(begin_addr - 0x04));
			calladdr = (char*)((unsigned long*)(begin_addr + 0x0C));

			title = (wchar_t*)(begin_addr + offset);

			VirtualProtect((LPVOID)calladdr, 5, PAGE_EXECUTE_READWRITE, &dwProtect);

			memcpy(bytes, calladdr, 5);
		}

		~CustomKeyboardText()
		{
			unsigned long unused;	

			VirtualProtect((LPVOID)calladdr, 5, dwProtect, &unused);
		}

		void DISPLAY_ONSCREEN_KEYBOARD(BOOL p0, const std::wstring& windowTitle, char* p2, char* defaultText, char* defaultConcat1, char* defaultConcat2, char* defaultConcat3, int maxInputLength)
		{
			memset(calladdr, 0x90, 5);
			memcpy(title, windowTitle.c_str(), windowTitle.size() * sizeof(wchar_t) + sizeof(wchar_t));
			GAMEPLAY::DISPLAY_ONSCREEN_KEYBOARD(p0, "CELL_EMAIL_BOD", p2, defaultText, defaultConcat1, defaultConcat2, defaultConcat3, maxInputLength);
			memcpy(calladdr, bytes, 5);
		}
	};
};

namespace Server
{
	struct ServerVehicleInfo
	{
		struct ModInfo
		{
			int Mod;
			int Variation;
		};
		Hash Model;
		GTA::Vector3<int> TireSmokeColor;
		GTA::Vector3<int> NeonLightColor;
		std::array<GTA::Vector3<int>, 2> VehicleColor;
		std::array<ModInfo, 25> Mods;
		std::array<int, 2> ExtraColor;
		int WindowTint;
		int WheelType;
		int CRoofstate;
		std::array<int, 4> NeonLights;
		int Livery;
		int Plate;
		GTA::Vector3<float> Position;
		GTA::Vector4<float> Quatenion;

		ServerVehicleInfo()
		{
			memset(this, 0, sizeof(ServerVehicleInfo));
		}

		bool VehicleToVariable(Vehicle vehicle)
		{
			if (!ENTITY::DOES_ENTITY_EXIST(vehicle))
			{
				return false;
			}

			this->Model = ENTITY::GET_ENTITY_MODEL(vehicle);
			this->WindowTint = VEHICLE::GET_VEHICLE_WINDOW_TINT(vehicle);
			this->WheelType = VEHICLE::GET_VEHICLE_WHEEL_TYPE(vehicle);
			this->CRoofstate = VEHICLE::GET_CONVERTIBLE_ROOF_STATE(vehicle);
			this->Livery = VEHICLE::GET_VEHICLE_LIVERY(vehicle);
			this->Plate = VEHICLE::GET_VEHICLE_NUMBER_PLATE_TEXT_INDEX(vehicle);

			VEHICLE::_GET_VEHICLE_NEON_LIGHTS_COLOUR(vehicle, &this->NeonLightColor.x, &this->NeonLightColor.y, &this->NeonLightColor.z);
			VEHICLE::GET_VEHICLE_CUSTOM_PRIMARY_COLOUR(vehicle, &this->VehicleColor[0].x, &this->VehicleColor[0].y, &this->VehicleColor[0].z);
			VEHICLE::GET_VEHICLE_CUSTOM_SECONDARY_COLOUR(vehicle, &this->VehicleColor[1].x, &this->VehicleColor[1].y, &this->VehicleColor[1].z);
			VEHICLE::GET_VEHICLE_TYRE_SMOKE_COLOR(vehicle, &this->TireSmokeColor.x, &this->TireSmokeColor.y, &this->TireSmokeColor.z);
			VEHICLE::GET_VEHICLE_EXTRA_COLOURS(vehicle, &this->ExtraColor[0], &this->ExtraColor[1]);

			for (int i = 0; i < this->Mods.size(); ++i)
			{
				if (i >= 17 && i <= 22)
				{
					this->Mods[i].Mod = VEHICLE::IS_TOGGLE_MOD_ON(vehicle, i);
					this->Mods[i].Variation = 0;
				}
				else
				{
					this->Mods[i].Mod = VEHICLE::GET_VEHICLE_MOD(vehicle, i);
					this->Mods[i].Variation = VEHICLE::GET_VEHICLE_MOD_VARIATION(vehicle, i);
				}
			}

			for (int i = 0; i < this->NeonLights.size(); ++i)
			{
				this->NeonLights[i] = VEHICLE::_IS_VEHICLE_NEON_LIGHT_ENABLED(vehicle, i);
			}

			Vector3 coords = ENTITY::GET_ENTITY_COORDS(vehicle, false);

			this->Position.x = coords.x;
			this->Position.y = coords.y;
			this->Position.z = coords.z;

			ENTITY::GET_ENTITY_QUATERNION(vehicle, &this->Quatenion.x, &this->Quatenion.y, &this->Quatenion.z, &this->Quatenion.w);

			return true;
		}

		Vehicle CreateVehicle(float x, float y, float z, float a)
		{
			if (STREAMING::IS_MODEL_IN_CDIMAGE(this->Model) && STREAMING::IS_MODEL_A_VEHICLE(this->Model))
			{
				STREAMING::REQUEST_MODEL(this->Model);

				while (!STREAMING::HAS_MODEL_LOADED(this->Model))
				{
					WAIT(0);
				}

				Vehicle vehicle = VEHICLE::CREATE_VEHICLE(this->Model, x, y, z, a, 1, 1);

				this->Position.x = x;
				this->Position.y = y;
				this->Position.z = z;

				VEHICLE::SET_VEHICLE_ON_GROUND_PROPERLY(vehicle);

				VEHICLE::SET_VEHICLE_MOD_KIT(vehicle, 0);
				VEHICLE::SET_VEHICLE_WINDOW_TINT(vehicle, this->WindowTint);
				VEHICLE::SET_VEHICLE_WHEEL_TYPE(vehicle, this->WheelType);
				VEHICLE::SET_VEHICLE_LIVERY(vehicle, this->Livery);
				VEHICLE::SET_VEHICLE_NUMBER_PLATE_TEXT_INDEX(vehicle, this->Plate);

				if (this->CRoofstate != 0 && this->CRoofstate != 3)//0 & 3 == closed/closing, 1 & 2 == opening/open
				{
					VEHICLE::LOWER_CONVERTIBLE_ROOF(vehicle, true);
				}

				VEHICLE::_SET_VEHICLE_NEON_LIGHTS_COLOUR(vehicle, this->NeonLightColor.x, this->NeonLightColor.y, this->NeonLightColor.z);
				VEHICLE::SET_VEHICLE_CUSTOM_PRIMARY_COLOUR(vehicle, this->VehicleColor[0].x, this->VehicleColor[0].y, this->VehicleColor[0].z);
				VEHICLE::SET_VEHICLE_CUSTOM_SECONDARY_COLOUR(vehicle, this->VehicleColor[1].x, this->VehicleColor[1].y, this->VehicleColor[1].z);
				VEHICLE::SET_VEHICLE_TYRE_SMOKE_COLOR(vehicle, this->TireSmokeColor.x, this->TireSmokeColor.y, this->TireSmokeColor.z);
				VEHICLE::SET_VEHICLE_EXTRA_COLOURS(vehicle, this->ExtraColor[0], this->ExtraColor[1]);

				for (int i = 0; i < this->Mods.size(); ++i)
				{
					if (i >= 17 && i <= 22)
					{
						VEHICLE::TOGGLE_VEHICLE_MOD(vehicle, i, this->Mods[i].Mod);
					}
					else
					{
						VEHICLE::SET_VEHICLE_MOD(vehicle, i, this->Mods[i].Mod, this->Mods[i].Variation);
					}
				}

				for (int i = 0; i < this->NeonLights.size(); ++i)
				{
					VEHICLE::_SET_VEHICLE_NEON_LIGHT_ENABLED(vehicle, i, this->NeonLights[i]);
				}

				WAIT(0);

				ENTITY::GET_ENTITY_QUATERNION(vehicle, &this->Quatenion.x, &this->Quatenion.y, &this->Quatenion.z, &this->Quatenion.w);

				STREAMING::SET_MODEL_AS_NO_LONGER_NEEDED(this->Model);
				ENTITY::SET_VEHICLE_AS_NO_LONGER_NEEDED(&vehicle);

				return vehicle;
			}
			return 0;
		}
	};

	struct Synchronization
	{
		GTA::Vector3<float> newpos;
		GTA::Vector4<float> newquat;
		GTA::Vector3<float> newvelocity;
		GTA::Vector3<float> newrotvelocity;
		int health;

		static void SyncUpate(Entity entity, const Synchronization& update)
		{
			ENTITY::SET_ENTITY_HEALTH(entity, update.health);

			ENTITY::SET_ENTITY_COORDS(entity, update.newpos.x, update.newpos.y, update.newpos.z, false, false, false, false);
			ENTITY::SET_ENTITY_VELOCITY(entity, update.newvelocity.x, update.newvelocity.y, update.newvelocity.z);

			ENTITY::SET_ENTITY_QUATERNION(entity, update.newquat.x, update.newquat.y, update.newquat.z, update.newquat.w);

			ENTITY::APPLY_FORCE_TO_ENTITY(entity, 0, 0.0, 0.0, 0.0, update.newrotvelocity.x, update.newrotvelocity.y, update.newrotvelocity.z, false, true, false, false, false, false);
		}

		static const Synchronization& SyncUpate(Entity entity)
		{
			Synchronization update;
			update.health = ENTITY::GET_ENTITY_HEALTH(entity);

			Vector3 coords = ENTITY::GET_ENTITY_COORDS(entity, false);

			update.newpos.x = coords.x;
			update.newpos.y = coords.y;
			update.newpos.z = coords.z;

			Vector3 velocity = ENTITY::GET_ENTITY_VELOCITY(entity);

			update.newvelocity.x = velocity.x;
			update.newvelocity.y = velocity.y;
			update.newvelocity.z = velocity.z;

			ENTITY::GET_ENTITY_QUATERNION(entity, &update.newquat.x, &update.newquat.y, &update.newquat.z, &update.newquat.w);

			Vector3 rotvelocity = ENTITY::GET_ENTITY_ROTATION_VELOCITY(entity);

			update.newrotvelocity.x = rotvelocity.x;
			update.newrotvelocity.y = rotvelocity.y;
			update.newrotvelocity.z = rotvelocity.z;

			return update;
		}
	};

	enum EntityType
	{
		ET_None,
		ET_Actor,
		ET_Vehicle,
		ET_Object,
		ET_Amount
	};

	template<typename T> class StateSwitchData
	{
	public:
		T Old;
		T New;
		std::pair<bool, T> Update(T New)
		{
			std::pair<bool, T> ret(New != Old, Old);
			Old = New;
			return ret;
		}
	};

	class EntityStateWatch
	{
	public:
		using HealthST				= std::pair<bool, float>;
		using AliveST				= std::pair<bool, bool>;
		using VehicleST				= std::pair<bool, Vehicle>;
		using EntityAttachedToST	= std::pair<bool, Entity>;
		using EntityAttachedTypeST	= std::pair<bool, EntityType>;

		Entity Handle;
		StateSwitchData<float> Health;
		StateSwitchData<bool> Alive;
		StateSwitchData<Vehicle> PedVehicle;

		StateSwitchData<Entity> AttachedToEntity;
		StateSwitchData<EntityType> AttachedToType;

		template<typename T> EntityStateWatch(T EntityHandle)
		{
			this->Handle = (Entity)EntityHandle;
		}
	};

	class StateWatch
	{
		std::set<Entity> Entities;
		std::array<std::set<std::unique_ptr<EntityStateWatch>>, ET_Amount> WatchedEntities;

		template <typename T> using Event = std::function<void(T, T)>;

		template <typename T> using EventProxy = std::pair<void*, Event<T>>;
		
		std::set<EventProxy<bool>> AliveChange;
	public:

		StateWatch()
		{

		}

		template <class T> void AddEventAlive(T* object, void(T::*func)(bool, bool))
		{
			AliveChange.insert(EventProxy((void*)object, std::bind(func, (T*), std::placeholders::_1, std::placeholders::_2)));
		}

		void AddEventAlive(Event<bool>& func)
		{
			AliveChange.insert(EventProxy<bool>(nullptr, func));
		}

		void Update()
		{
			for (auto& i : WatchedEntities[ET_Actor])
			{
				Entity Handle = i->Handle;

				{
					EntityStateWatch::AliveST RetVal(i->Alive.Update(!ENTITY::IS_ENTITY_DEAD(Handle)));

					if (RetVal.first)
					{

					}
				}

				{
					EntityStateWatch::HealthST RetVal(i->Alive.Update(!ENTITY::IS_ENTITY_DEAD(Handle)));
					if (RetVal.first)
					{

					}
				}
			}
		}

		template<typename T> bool Add(T EntityHandle)
		{
			Entity Handle = (Entity)EntityHandle;

			if (!ENTITY::IS_AN_ENTITY(Handle) || Entities.find(Handle) != Entities.end())
			{
				return false;
			}

			if (ENTITY::IS_ENTITY_A_PED(Handle))
			{
				Entities.insert(Handle);
				WatchedEntities[ET_Actor].insert(std::make_unique<EntityStateWatch>(Handle));
				return true;
			}

			if (ENTITY::IS_ENTITY_A_VEHICLE(Handle))
			{
				Entities.insert(Handle);
				WatchedEntities[ET_Vehicle].insert(std::make_unique<EntityStateWatch>(Handle));
				return true;
			}

			if (ENTITY::IS_ENTITY_AN_OBJECT(Handle))
			{
				Entities.insert(Handle);
				WatchedEntities[ET_Object].insert(std::make_unique<EntityStateWatch>(Handle));
				return true;
			}

			return false;
		}
	};
};