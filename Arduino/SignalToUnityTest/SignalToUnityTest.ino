const int blueButton = 2;
const int yellowButton = 4;

int right = 0;
int left = 0;

void setup() {
  Serial.begin(115200);
  Serial.println("Started");
  pinMode(blueButton, INPUT);

}

void loop() {
  // put your main code here, to run repeatedly:
  right = digitalRead(blueButton);
  left = digitalRead(yellowButton);

  if(right == HIGH){
    Serial.println("go right");
    delay(50);
  }
  if(left == HIGH){
    Serial.println("go left");
    delay(50);
  }

}
