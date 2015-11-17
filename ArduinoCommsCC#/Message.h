#ifndef MESSAGE_HEADER
#define MESSAGE_HEADER

#include <stdbool.h>

enum Actions
{
  PING = 1,
  CHECK_FOR_DECOMPRESSION_DEVICE,
  HERE_IS_A_DECOMPRESSION_DEVICE,
  TIMEOUT_OCCURED,
  UPDATE_SETTINGS,
  PC_AQUIRE_CONTROL,
  PC_RELEASE_CONTROL,
  INCORRECT_PRESSURE,
  CORRECT_PRESSURE,
  UPDATE_GLOBAL_PRESSURE,
  CONNECTION_CORRUPTED,
  PC_AQUIRE_CONTROL_SUCCESS,
  PC_RELEASE_CONTROL_SUCCESS
};

enum MsgStruct
{
  MS_CharA               = 0xE6,
  MS_CharB               = 0xC9,

  MS_StartA              = 0x00,
  MS_StartB              = 0x01,
  MS_Action              = 0x02,
  MS_DataLen             = 0x03,
  MS_CorrCheck           = 0x04,
  MS_DataBegin           = 0x05,

  MS_DataEnd             = 0x1E,
  MS_AlwaysZero          = 0x1F,
  MS_MaxDataLen          = 0x1A,
  MS_BufferSize          = 0x20,

  MS_CorrModulo          = 0x100
};

void Message_Init();
byte Message_GetAction();
void Message_SetAction(byte action);
byte Message_GetDataLenght();
bool Message_Read_byte(byte* output);
bool Message_Write_byte(byte input);
bool Message_Read_Int16(int16_t* output);
bool Message_Write_Int16(int16_t input);
byte Message_Receive();
byte Message_Send();
bool Message_IsCorrupt();

#endif
