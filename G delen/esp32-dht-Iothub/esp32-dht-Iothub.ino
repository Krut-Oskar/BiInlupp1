#include <Adafruit_Sensor.h>
#include <DHT.h>
#include <WiFi.h>
#include <ArduinoJson.h>
#include <Esp32MQTTClient.h>

#define DHT_PIN 4
#define DHT_TYPE DHT11
#define INTERVAL 5000
DHT dht(DHT_PIN, DHT_TYPE);
const char* ssid = "**********";
const char* pass = "***********";
static char* connectionString = "********************";
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
  

