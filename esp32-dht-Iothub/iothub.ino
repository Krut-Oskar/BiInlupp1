void initIotHub(){
  if(!Esp32MQTTClient_Init((const uint8_t *) connectionString)){
    isConnected = false;
    return;
  }
  isConnected = true;
  Serial.println("IoT Hub connected");
}

void sendCallback(IOTHUB_CLIENT_CONFIRMATION_RESULT result)
{
  if(result == IOTHUB_CLIENT_CONFIRMATION_OK){
    Serial.println("Message confirmed");
    messagePending = false;
  }
}

void initDevice(){
  Esp32MQTTClient_Init((uint8_t*) connectionString, true);
  Esp32MQTTClient_SetSendConfirmationCallback(sendCallback);
}

void sendMessage(char *payload){
  
  messagePending = true;
  EVENT_INSTANCE *message = Esp32MQTTClient_Event_Generate(payload, MESSAGE);

  Esp32MQTTClient_Event_AddProp(message, "School", "Nackademin");
  Esp32MQTTClient_Event_AddProp(message, "Name", "William");

  Esp32MQTTClient_SendEventInstance(message);

}