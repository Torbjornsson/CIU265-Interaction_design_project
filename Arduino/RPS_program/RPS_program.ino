// digital pin 2 is the hall pin
int hall_pin = 2;

unsigned long start_time;
float hall_count;
bool on_state = true;

void setup() {
  // initialize serial communication at 9600 bits per second:
  Serial.begin(115200);
  Serial.println("Starting...");
  // make the hall pin an input:
  pinMode(hall_pin, INPUT);
}



// the loop routine runs over and over again forever:
void loop() {
  hall_count = 0.0;
  start_time = millis();
  // counting number of times the hall sensor is tripped
  // but without double counting during the same trip
  while(millis()-start_time < 1000){
    if (start_time > millis()) {
      start_time = millis();
      hall_count = 0.0;
    }
    
    if (digitalRead(hall_pin)==0){
      if (!on_state){
        on_state = true;
        hall_count+=1.0;
//        Serial.println("Hall count: " + String(hall_count));
      }
    } else{
      on_state = false;
    }
  }
  
  Serial.println(String(hall_count/2));
}
