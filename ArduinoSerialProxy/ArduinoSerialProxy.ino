#include <SoftwareSerial.h>

SoftwareSerial ss(5, 6);

void setup() 
{
  Serial.begin(9600);
  ss.begin(9600);
}

void loop() 
{
  while(Serial.available())
  {
    ss.write(Serial.read());
  }
  while(ss.available())
  {
    Serial.write(ss.read());
  }
}
