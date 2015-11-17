void setup(void)
{
  Serial.begin(115200);
}

double AverF(int x, unsigned int weight)
{
  static bool firstInsert = false;
  static int insertedElems = 0;
  static unsigned long movingAverage[50];
  static double previousAverage	;

  if (weight > 50)
  {
    weight = 50;
  }

  if (!firstInsert)
  {
    previousAverage = (double)x;
    firstInsert = true;
  }

  movingAverage[insertedElems] = x;

  if (++insertedElems == weight)
  {
    insertedElems = 0;

    previousAverage = 0.0;
    for (int j = 0; j < weight; ++j)
    {
      previousAverage += (double)movingAverage[j];
    }
    previousAverage /= (double)weight;
  }

  return previousAverage;
}

double MoveAverF(int x, unsigned int weight)
{
  static bool firstInsert = false;
  static bool firstMeasurements = false;
  static int insertedElems = 0;
  static unsigned long movingAverage[50];
  static double previousAverage	;

  if (weight > 50)
  {
    weight = 50;
  }

  if (!firstInsert)
  {
    previousAverage = (double)x;
    firstInsert = true;
  }

  movingAverage[insertedElems] = x;

  if (++insertedElems == weight)
  {
    insertedElems = 0;
    firstMeasurements = true;
  }
  
  if (firstMeasurements)
  {
    previousAverage = 0.0;
    for (int j = 0; j < weight; ++j)
    {
      previousAverage += (double)movingAverage[j];
    }
  }
  
  previousAverage /= (double)weight;

  return previousAverage;
}

void loop()
{
  //int tijd = millis();
  int val = analogRead(A0);
  double gem = MoveAverF(val, 50);

  Serial.print(val);
  Serial.print(",");
  Serial.println(gem);
  delay(60);//60 FPS
}
