#include <Adafruit_Sensor.h>
#include <DHT.h>
#include <WiFi.h>
#include <ArduinoJson.h>
#include <Esp32MQTTClient.h>

#define DHT_PIN 4
#define DHT_TYPE DHT11
DHT dht(DHT_PIN, DHT_TYPE);
const char* ssid = "Nackas hörna";
const char* pass = "Lennartskoglund";
static char* connectionString = "HostName=williamsnatverk.azure-devices.net;DeviceId=Esp32;SharedAccessKey=cXN/m4m9euU2zKghHzadzxPh4bJ6DdKGI2slR7GGYwI=";
static bool isConnected = false;



void setup() {
  // put your setup code here, to run once:
  Serial.begin(115200);
  dht.begin();
  initWifi();
  initIotHub();
  if(isConnected == false){
    Serial.println("Iot hub failed");
  }
  

}

void loop() {
  Serial.println("Running loop");
  Serial.println(isConnected);
  if(isConnected){
    
    float temperature = dht.readTemperature();
    float humidity = dht.readHumidity();
    char temp[10]; 
    dtostrf(temperature, 6,2, temp); 
    char hum[10]; 
    dtostrf(humidity, 6,2, hum);
  
    char payload[32];
    sprintf(payload, "{\"temp\": %s, \"hum\": %s}", temp, hum);
    if(Esp32MQTTClient_SendEvent(payload)){
      Serial.println("Success");
      Serial.println(payload);
    }
   delay(10 * 1000);
  }
}
