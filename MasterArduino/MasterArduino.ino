#include <AltSoftSerial.h>
#include <SoftwareSerial.h>
#include "Arduino.h"

SoftwareSerial bluetooth_a(5, 6);
AltSoftSerial bluetooth_b;

void setup()
{
  pinMode(12, OUTPUT);
  Serial.begin(1200);
  bluetooth_a.begin(1200);
  bluetooth_b.begin(1200);
  digitalWrite(12, HIGH);
}

void PrintToAll(char x)
{
  Serial.print(x);
  bluetooth_a.print(x);
  bluetooth_b.print(x);
}

void loop()
{
  if (bluetooth_a.available())
  {
    PrintToAll(bluetooth_a.read());
  }
  if (bluetooth_b.available())
  {
    PrintToAll(bluetooth_b.read());
  }
  if (Serial.available())
  {
      PrintToAll(Serial.read());
  }
}
