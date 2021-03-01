void initIotHub(){
  if(!Esp32MQTTClient_Init((const uint8_t *) connectionString)){
    isConnected = false;
    return;
  }
  isConnected = true;
}
