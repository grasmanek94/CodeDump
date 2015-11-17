int temps[] =
  { 737, 684, 628, 570, 512, 456, 402, 353 };

double GetTemp(int readout, bool *error, bool *interpolated)
{
    int maxElem = sizeof(temps)/sizeof(int)-1;
    if(readout > temps[0] || readout < temps[maxElem])
    {
        *error = true;
        *interpolated = false;
        return 0.0;
    }
  
    int i = 0;
    for(; i <= maxElem; ++i)
    {
        if(readout <= temps[i])
        {
            break;
        }
    } 
    
    *interpolated = readout == temps[i]; 
    if(!*interpolated)
    {
        return ((double)i+1.0)*5.0;
    }
    
    if(i == maxElem)
    {
        *error = true;
        return 0.0; 
    }
    
    return 
    ((double)i+1)*5 + //basis temperatuur plus
    (5.0 * //standaard verschil maal
    (double)(temps[i] - readout) / (double)(temps[i]-temps[i+1]));//percentage op naar volgende temperatuur
}

void setup() 
{

}

void loop() 
{
    bool error;
    bool interpolated;
    int readout = analogRead(A5);
    double temp = GetTemp(readout, &error, &interpolated);
    
    if(error)
    {
        Serial.println("ERROR");
    }
    else
    {
        Serial.print("Readval: ");
        Serial.print(readout);
        Serial.print(" Temperature: ");
        Serial.print((int)temp);
        if(interpolated)
        {
           Serial.print(" *"); 
        }
        Serial.println("");
    }
    delay(1000);
}
