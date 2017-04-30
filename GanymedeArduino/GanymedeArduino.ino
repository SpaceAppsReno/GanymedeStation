#include <SPI.h>
#include "RF24.h"

/****************** Radio Config ***************************/
/***      Set this radio as radio number 0 or 1         ***/
bool radioNumber = 0;
/* Hardware configuration: Set up nRF24L01 radio on SPI bus plus pins 9 & 10 */
RF24 radio(9,10);

byte addresses[][6] = {"1Node","2Node"};
/**********************************************************/

// pin assignments
#define MOISTURE_SENSOR A3
#define LIGHT_SENSOR    A1
#define TEMP_SENSOR     A2

typedef struct{
  int16_t moisture;
  int16_t light;
  int16_t temp;
  int16_t volts;
}
s_data;

s_data sensorData;

void setup() {
   // start serial for debug output
   Serial.begin(9600);
   
   
   Serial.println(F("Ganymede Station Sensor Node"));
   
   
   // radio setup
   radio.begin();
   // Set the PA Level low to prevent power supply related issues due to
   // likelihood of close proximity of the devices. RF24_PA_MAX is default.
   radio.setPALevel(RF24_PA_LOW);
   
   // Open a writing and reading pipe on each radio, with opposite addresses
   radio.openWritingPipe(addresses[0]);
   radio.openReadingPipe(1,addresses[1]);
}

void loop() {
   sensorData.moisture = getMoistureReading();
   sensorData.light = getLightReading();
   sensorData.temp = getTempReading();
   sensorData.volts = getVccReading();

   Serial.println(F("Now sending sensor data"));
   if (!radio.write( &sensorData, sizeof(sensorData) )){
       Serial.println(F("failed to send sensor data"));
   }
   delay(10000);
}


int16_t getMoistureReading()
{
  int moistValue = analogRead(MOISTURE_SENSOR);
  Serial.print("Moisture: ");
  Serial.println(moistValue);

  return moistValue;
}


int16_t getLightReading()
{
  int lightValue = analogRead(LIGHT_SENSOR);
  Serial.print("Light: ");
  Serial.println(lightValue);

  return lightValue;
}

int16_t getTempReading()
{
   const int SERIESRESISTOR = 10000;
   const int NUMSAMPLES = 5;
   int samples[NUMSAMPLES];
   uint8_t i;
   float average;
 
   // take N samples in a row, with a slight delay
   for (i=0; i< NUMSAMPLES; i++) {
      samples[i] = analogRead(TEMP_SENSOR);
      delay(10);
   }

   // average all the samples out
   average = 0;
   for (i=0; i< NUMSAMPLES; i++) {
      average += samples[i];
   }
   average /= NUMSAMPLES;
   
   // convert the value to resistance
   average = 1023 / average - 1;
   average = SERIESRESISTOR / average;
   Serial.print("Thermistor resistance: ");
   Serial.println(average);

   return resistanceToCelsius(average);
}


// this code from: https://learn.adafruit.com/thermistor/using-a-thermistor
// thanks lady ada!
int16_t resistanceToCelsius(float resistance)
{
   // resistance at 25 degrees C
   const int THERMISTOR_NOMINAL = 10000;
   // temp. for nominal resistance (almost always 25 C)
   const int TEMPERATURE_NOMINAL = 26;
   // The beta coefficient of the thermistor (usually 3000-4000)
   const int B_COEFFICIENT = 3950; //3950?

   float steinhart;
   steinhart = resistance / THERMISTOR_NOMINAL;     // (R/Ro)
   steinhart = log(steinhart);                  // ln(R/Ro)
   steinhart /= B_COEFFICIENT;                   // 1/B * ln(R/Ro)
   steinhart += 1.0 / (TEMPERATURE_NOMINAL + 273.15); // + (1/To)
   steinhart = 1.0 / steinhart;                 // Invert
   steinhart -= 273.15;                         // convert to C
   
   Serial.print("Temperature: "); 
   Serial.print(steinhart);
   Serial.println(" *C");

   return (int16_t)steinhart;
}

// this code from: https://forum.arduino.cc/index.php?topic=356752.0
// thanks johnwasser!
int16_t getVccReading() {
  long result;
  // Read 1.1V reference against AVcc
  ADMUX = _BV(REFS0) | _BV(MUX3) | _BV(MUX2) | _BV(MUX1);
  delay(2); // Wait for Vref to settle
  ADCSRA |= _BV(ADSC); // Convert
  while (bit_is_set(ADCSRA,ADSC));
  result = ADCL;
  result |= ADCH<<8;
  result = 1126400L / result; // Back-calculate AVcc in mV

   Serial.print("Vcc: ");
   Serial.println(result);
   
  
  return (int16_t)result;
}
