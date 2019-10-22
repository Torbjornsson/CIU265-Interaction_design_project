#include <FastLED.h>

#define LED_PIN     5
#define NUM_LEDS    11
#define BRIGHTNESS  64
#define LED_TYPE    WS2811
#define COLOR_ORDER GRB
CRGB leds[NUM_LEDS];

#define UPDATES_PER_SECOND 100

CRGBPalette16 currentPalette;
TBlendType    currentBlending;

extern CRGBPalette16 myRedWhiteBluePalette;
extern const TProgmemPalette16 myRedWhiteBluePalette_p PROGMEM;

// digital pin 2 is the hall pin
int hall_pin = 2;

const int buttonPin = 7;
int buttonState = 0;

unsigned long start_time;
int hall_count;
int temp = 0;
bool on_state = true;
bool pressed = false;
char receivedChar = "i";

void setup() {
  // initialize serial communication at 9600 bits per second:
  Serial.begin(115200);
  Serial.println("Starting...");
  // make the hall pin an input:
  Serial.println(String(0));
  pinMode(hall_pin, INPUT);
  pinMode(buttonPin, INPUT_PULLUP);

  delay( 2000 ); // power-up safety delay
  FastLED.addLeds<LED_TYPE, LED_PIN, COLOR_ORDER>(leds, NUM_LEDS).setCorrection( TypicalLEDStrip );
  FastLED.setBrightness(  BRIGHTNESS );
  
  SetupIcePalette();
  currentBlending = LINEARBLEND;
}



// the loop routine runs over and over again forever:
void loop() {
  
  static uint8_t startIndex = 0;
  startIndex = startIndex + 1; /* motion speed */
  
  FillLEDsFromPaletteColors( startIndex);
  
  FastLED.show();
  
  hall_count = 0;
  start_time = millis();
  // counting number of times the hall sensor is tripped
  // but without double counting during the same trip
  while(millis()-start_time < 1000){
    if (start_time > millis()) {
      start_time = millis();
      hall_count = 0;
    }

    buttonState = digitalRead(buttonPin);
    
    if (buttonState == LOW ) {
      if(!pressed){
        pressed = true;
        Serial.println("restart");
        hall_count = 0;
      }
      delay(100);
    }else{
        pressed = false;
    }

    if (Serial.available() > 0) {
      receivedChar = Serial.read();
      Serial.println(receivedChar);
      ChangePalettePeriodically(receivedChar);
    }
    
    if (digitalRead(hall_pin)==0){
      if (!on_state){
        on_state = true;
        hall_count+=1;
      }
    } else{
      on_state = false;
    }
  }
  
  if(temp != hall_count){
    Serial.println(String(hall_count));
    temp = hall_count;
  }
  
}

void FillLEDsFromPaletteColors( uint8_t colorIndex)
{
    uint8_t brightness = 255;
    
    for( int i = 0; i < NUM_LEDS; i++) {
        leds[i] = ColorFromPalette( currentPalette, colorIndex, brightness, currentBlending);
        colorIndex += 3;
    }
}

void ChangePalettePeriodically(char receivedChar)
{
  
  if(receivedChar == 'i'){
    SetupIcePalette();
    currentBlending = LINEARBLEND;
  }
//  if(receivedChar == "){
//    SetupIceToWaterPalette();
//    currentBlending = LINEARBLEND;
//  }
  if(receivedChar == 'w'){
    SetupWaterPalette();
    currentBlending = LINEARBLEND;
  }
//  if(count == 3){
//    SetupWaterToGasPalette();
//    currentBlending = LINEARBLEND;
//  }
  if(receivedChar == 'g'){
    SetupGasPalette();
    currentBlending = LINEARBLEND;
  }

}

void SetupGasPalette(){
  fill_solid( currentPalette, 16, 0x95FFFC);
}

void SetupWaterPalette(){
  fill_solid( currentPalette, 16, CRGB::Blue);
}

void SetupIcePalette(){
  
  fill_solid(currentPalette, 16, CRGB( 0, 255, 247));
}

void SetupIceToWaterPalette(){
  
  fill_solid(currentPalette, 16, CRGB( 0, 255, 247));
  currentPalette[0] = CRGB::Blue;
  currentPalette[2] = CRGB::Blue;
  currentPalette[4] = CRGB::Blue;
  currentPalette[6] = CRGB::Blue;
  currentPalette[8] = CRGB::Blue;
  currentPalette[10] = CRGB::Blue;
  currentPalette[12] = CRGB::Blue;
  currentPalette[14] = CRGB::Blue;
}

void SetupWaterToGasPalette(){
  
  fill_solid( currentPalette, 16, CRGB::Blue);
  currentPalette[0] = 0x95FFFC;
  currentPalette[2] = 0x95FFFC;
  currentPalette[4] = 0x95FFFC;
  currentPalette[6] = 0x95FFFC;
  currentPalette[8] = 0x95FFFC;
  currentPalette[10] = 0x95FFFC;
  currentPalette[12] = 0x95FFFC;
  currentPalette[14] = 0x95FFFC;
}
