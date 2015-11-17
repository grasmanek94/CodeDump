#include <Bounce2.h>

Bounce bouncer  = Bounce();

int greenPin = 4;
int redPin = 3;

int lastbuttstate = 0;
unsigned long timer_start = millis();

void setup()
{
  pinMode(greenPin, OUTPUT);
  pinMode(redPin, OUTPUT);
  pinMode(A0, INPUT);

  Serial.begin(9600);
  bouncer.attach( A0 );
  bouncer.interval(5);
}

void loop()
{
  bouncer.update();
  if (bouncer.read() != lastbuttstate)
  {
    digitalWrite(greenPin, LOW);
    digitalWrite(redPin, LOW);
    lastbuttstate = bouncer.read();
    if (lastbuttstate == 0)
    {
      timer_start = millis();
    }
    else
    {
      unsigned long timeTaken = millis() - timer_start;
      Serial.write((char*)&timeTaken, sizeof(unsigned long));
    }
  }

  if (Serial.available() >= 2)
  {
    int me = Serial.read();
    if (me == 0)//en in andere == 1
    {
      switch (Serial.read())
      {
        case 0:
          digitalWrite(greenPin, LOW);
          digitalWrite(redPin, LOW);
          break;
        case 1:
          digitalWrite(redPin, LOW);
          digitalWrite(greenPin, HIGH);
          break;
        case 2:
          digitalWrite(redPin, HIGH);
          digitalWrite(redPin, LOW);
          break;
      }
    }
  }
}
