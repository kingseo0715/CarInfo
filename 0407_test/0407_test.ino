int led = 8;
int led2 = 12;

void setup() {
  
  Serial.begin(9600);

  pinMode(led, OUTPUT);
  pinMode(led2, OUTPUT);
  
}

void loop() {
  if (Serial.available()) {
    char ch = Serial.read();

    if (ch == '1')
      digitalWrite(led2, HIGH);
    else if (ch == '2') 
      digitalWrite(led, HIGH);
    else{
      digitalWrite(led2, LOW);
      digitalWrite(led, LOW);
   
    }
      
  }

}
