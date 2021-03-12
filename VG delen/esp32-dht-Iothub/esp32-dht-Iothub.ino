#include <Adafruit_Sensor.h>
#include <DHT.h>
#include <WiFi.h>
#include <ArduinoJson.h>
#include <Esp32MQTTClient.h>

#define DHT_PIN 4
#define DHT_TYPE DHT11
#define INTERVAL 5000
DHT dht(DHT_PIN, DHT_TYPE);
const char* ssid = "Nackas hörna";
const char* pass = "Lennartskoglund";
static char* connectionString = "HostName=williamsnatverk.azure-devices.net;DeviceId=Esp32;SharedAccessKey=cXN/m4m9euU2zKghHzadzxPh4bJ6DdKGI2slR7GGYwI=";
static bool isConnected = false;
bool messagePending = false;
time_t epochTime;
float prevData = 0.0;
float diff = 1.0;




void setup() {
  // put your setup code here, to run once:
  Serial.begin(115200);
  delay(1000);
  dht.begin();
  initWifi();
  initIotHub();
  initDevice();
  delay(2000);
  
}

void loop() {
  unsigned long currentMillis = millis();
  epochTime = time(NULL);
  float temperature = dht.readTemperature();
  float humidity = dht.readHumidity();
  if(!messagePending){
    
    if(temperature > (prevData + diff) || temperature < (prevData - diff)){
      prevData = temperature;
      Serial.printf("Time: %lu", epochTime);
      char epochTimeBuf[12];

      char payload[256];
      DynamicJsonDocument doc(1024);
      doc["temperature"] = temperature;
      doc["humidity"] = humidity;

      serializeJson(doc, payload);

      sendMessage(payload, itoa(epochTime, epochTimeBuf, 10));
        

    }
  }

  Esp32MQTTClient_Check();
  delay(10);
}
  

  /*Serial.println("Running loop");
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
  }*/
