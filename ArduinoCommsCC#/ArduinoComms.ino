#include "Arduino.h"
#include "Message.h"

void setup()
{
  Message_Init();
  Serial.begin(38400);
}

long lastSend;
long timeNow;
void loop()
{
  timeNow = millis();

  if (Message_Receive())
  {
    if (!Message_IsCorrupt())
    {
      switch (Message_GetAction())
      {
        case CHECK_FOR_DECOMPRESSION_DEVICE:
          {
            byte readval = 0;
            if (Message_Read_byte(&readval))
            {
              if (readval == MS_DataBegin)
              {
                Message_BeginWrite();
                Message_SetAction(HERE_IS_A_DECOMPRESSION_DEVICE);
                Message_Write_byte(MS_DataEnd);
                Message_Send();
              }
            }
          }
          break;

        case PING:
          Message_BeginWrite();
          Message_SetAction(PING);
          Message_Send();
          break;

        case UPDATE_GLOBAL_PRESSURE:

          break;

        case PC_AQUIRE_CONTROL:
          Message_BeginWrite();
          Message_SetAction(PC_AQUIRE_CONTROL_SUCCESS);
          Message_Send();
          break;

        case PC_RELEASE_CONTROL:
          Message_BeginWrite();
          Message_SetAction(PC_RELEASE_CONTROL_SUCCESS);
          Message_Send();
          break;
      }
    }
  }

  if (timeNow - lastSend > 100)
  {
    lastSend = timeNow;
    Message_BeginWrite();

    Message_SetAction(UPDATE_SETTINGS);
    Message_Write_byte(0);  //VentValveOpen;
    Message_Write_byte(0);  //AirPumpOn;
    Message_Write_byte(0);  //PressureGlobalBar;
    Message_Write_byte(0);  //ControlFromPC;
    Message_Write_Int16(0); //PressureMilliBar;
    Message_Send();
  }
}
