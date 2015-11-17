void setup()
{
  Serial.begin(9600);
  pinMode(A0, INPUT);
}

double GetDistanceA(int input) // in cm
{
    return 4134.5*pow((double)input, 1.1); //Gemiddelde van beide overdrachtsfuncties
}

double GetDistanceB(int input) // in cm
{
    return 0.0005*(double)input-0.0114; //Gemiddelde van beide overdrachtsfuncties
}

void loop()
{
  long    countA = 0, 
          countB = 0;
  long    time;
  
  randomSeed(millis());
  
  time = millis();
  while(millis() - time < 30000)
  {
    GetDistanceA(random(1024));
    ++countA;
  }
  
  time = millis();

  while(millis() - time < 30000)
  {
    GetDistanceB(random(1024));
    ++countB;
  }  
  
  Serial.print("CountA: ");
  Serial.print(countA);
  Serial.print("; CountB: ");
  Serial.println(countB);
}
