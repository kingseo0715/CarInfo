int ledBlue = 12;
int ledred = 8;

void setup() {
  // put your setup code here, to run once:
  pinMode(ledBlue, OUTPUT);
  pinMode(ledred, OUTPUT);

}

void loop() {
  // put your main code here, to run repeatedly:
  digitalWrite(ledBlue, HIGH);
  digitalWrite(ledred, HIGH);

}
