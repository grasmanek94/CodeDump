#include "Arduino.h"

void setup()
{
  Serial.begin(38400);
}

void loop()
{
  while (Serial.available())
  {
    if ((byte)Serial.read() == (byte)0x05)
    {
      byte data[] = { 0xE6, 0xC9, 0x02, 0x01, (0x1E ^ 0x01) + 0x02, 0x1E };
      Serial.write(data, 6);
    }
  }
}
