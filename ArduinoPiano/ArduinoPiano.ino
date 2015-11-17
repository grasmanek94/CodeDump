#include <Bounce2.h>

int pins[] =
{
  A0,
  A1,
  A2,
  A3,
  A4,
  A5,
  6
};

const int amount_of_keys = sizeof(pins) / sizeof(int);

Bounce bouncer[amount_of_keys];
int pinsState[amount_of_keys];

void setup() 
{
  for(int i = 0; i < amount_of_keys; ++i)
  {
    pinMode(pins[i], INPUT_PULLUP);
    bouncer[i].attach(pins[i]);
    bouncer[i].interval(5);
    pinsState[i] = 0;
  }
  Serial.begin(115200);
}

void loop() 
{
  for(int i = 0; i < amount_of_keys; ++i)
  {
    bouncer[i].update();
    if(pinsState[i] != bouncer[i].read())
    {
      pinsState[i] = bouncer[i].read();
      char action = 'A' + i + pinsState[i] * amount_of_keys;
      Serial.write(action);
    }
  }
}
