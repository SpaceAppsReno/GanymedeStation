/*
 Copyright (C) 2011 J. Coliz <maniacbug@ymail.com>

 This program is free software; you can redistribute it and/or
 modify it under the terms of the GNU General Public License
 version 2 as published by the Free Software Foundation.

 03/17/2013 : Charles-Henri Hallard (http://hallard.me)
              Modified to use with Arduipi board http://hallard.me/arduipi
                    Changed to use modified bcm2835 and RF24 library
TMRh20 2014 - Updated to work with optimized RF24 Arduino library

 */

#include <cstdlib>
#include <iostream>
#include <sstream>
#include <string>
#include <unistd.h>
#include <RF24/RF24.h>
#include <ctime>
#include <curl/curl.h>

using namespace std;

struct s_data {
   int16_t moisture;
   int16_t light;
   int16_t temp;
   int16_t volts;
} sensorData;


void sendDataToServer(const char* json);
void scaleData(s_data* incomingData);


/********** Radio Config *********/
// Assign a unique identifier for this node, 0 or 1
bool radioNumber = 1;
RF24 radio(22,0);
// Radio pipe addresses for the 2 nodes to communicate.
const uint8_t pipes[][6] = {"1Node","2Node"};
/********************************/


/********** JSON Config *********/
const char *url = "http://ec2-54-186-39-241.us-west-2.compute.amazonaws.com:3000/api/BaseStation1/Pod1";
/********************************/


int main(int argc, char** argv) {

   cout << "Ganymede Station Garden Controller\n";

   // Setup and configure rf radio
   radio.begin();

   // optionally, increase the delay between retries & # of retries
   radio.setRetries(15,15);
   // Dump the configuration of the rf unit for debugging
   radio.printDetails();

   // set up radio to receive
   radio.openWritingPipe(pipes[1]);
   radio.openReadingPipe(1,pipes[0]);
   radio.startListening();
   
   clock_t startTime = clock(); //Start timer
   double secondsPassed;
   double uploadTimer = 15; // FASTER
   
   // forever loop
   while (1)
   { 
      // if there is data ready
      if ( radio.available() )
      {
         // Fetch the payload, and see if this was the last one.
         while(radio.available()){
            radio.read( &sensorData, (sizeof(sensorData)) );
         }
         
         scaleData(&sensorData);

         // report what we got
         printf("Got moisture %d...\n", sensorData.moisture);
         printf("Got light %d...\n", sensorData.light);
         printf("Got temp %d...\n", sensorData.temp);
         printf("Got volts %d...\n\n", sensorData.volts);
         
         
         delay(925); //Delay after payload responded to, minimize RPi CPU time
      }
      
      secondsPassed = (clock() - startTime) / CLOCKS_PER_SEC;
      
      if(secondsPassed >= uploadTimer)
      {
         // compose JSON data
         ostringstream json;
         json << "light=" << sensorData.light <<
             "&moisture=" << sensorData.moisture <<
            "&temperature=" << sensorData.temp <<
            "&voltage=" << sensorData.volts <<
             "&name=Pod1";
         
         // send data to the server
         sendDataToServer(json.str().c_str());
         
            
         printf("\nUpload just happened!\n\n");
         
         // reset uplaod clock
         startTime = clock();
      }
      
      
   } // forever loop

   curl_global_cleanup();
   return 0;
}


void sendDataToServer(const char* json)
{
   // send this data to the server
   CURL *curl;
   CURLcode res;
   
   cout << json << endl;
   
   curl_global_init(CURL_GLOBAL_ALL);
   curl = curl_easy_init();
   
   if(curl)
   {
      struct curl_slist *headers = NULL;
      curl_slist_append(headers, "Accept: application/json");
      curl_slist_append(headers, "Content-Type: application/json");
      curl_slist_append(headers, "charsets: utf-8");

      curl_easy_setopt(curl, CURLOPT_URL, url);

      curl_easy_setopt(curl, CURLOPT_CUSTOMREQUEST, "PUT");
      curl_easy_setopt(curl, CURLOPT_HTTPHEADER, headers);
      curl_easy_setopt(curl, CURLOPT_POSTFIELDS, json);
      curl_easy_setopt(curl, CURLOPT_USERAGENT, "libcrp/0.1");

      res = curl_easy_perform(curl);
   }
   else {
      printf("curl error!");
   }
   
   
   curl_easy_cleanup(curl);
}

void scaleData(s_data* incomingData)
{
   incomingData->moisture = (1024-incomingData->moisture)/10;
   incomingData->light = (incomingData->light)/3;
   incomingData->temp = (incomingData->light + 10);
}
