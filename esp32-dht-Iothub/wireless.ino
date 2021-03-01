void initWifi(){
  WiFi.begin(ssid, pass);
  while(WiFi.status() != WL_CONNECTED){
    delay(1000);
    Serial.print(".");
  }
  Serial.print("\nIP Adress: ");
  Serial.println(WiFi.localIP());
}
