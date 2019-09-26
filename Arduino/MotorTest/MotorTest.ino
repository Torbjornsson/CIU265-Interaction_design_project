// Include the Servo library 
#include <Servo.h> 
// Declare the Servo pin 
int servoPin = 3; 
// Create a servo object 
Servo Servo1; 
void setup() { 
   Serial.begin(9600);
   // We need to attach the servo to the used pin number 
   Servo1.attach(servoPin); 
}
void loop(){ 
   Serial.println("START");
   // Make servo go to 0 degrees 
   Servo1.write(20); 
   Serial.println("0");
   delay(1000); 
   // Make servo go to 90 degrees 
   Servo1.write(90); 
   Serial.println("90");
   delay(1000); 
   // Make servo go to 180 degrees 
   Servo1.write(160); 
   Serial.println("180");
   delay(1000); 
   Serial.println("END");
}
