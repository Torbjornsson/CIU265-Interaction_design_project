using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System;

public class RPS_info : MonoBehaviour
{
    SerialPort sp = new SerialPort("/dev/cu.usbmodem1421", 115200); //name of serial port depends on computer

    // Start is called before the first frame update
    void Start()
    {
        sp.Open();
        sp.ReadTimeout = 1;
    }

    // Update is called once per frame
    void Update()
    {
        try{
            string readLine = sp.ReadLine();
            print(readLine);
            
            if (float.Parse(readLine) < 1.0)
            {
                print("should be ice");
            }
            else if (float.Parse(readLine) > 0.0 && float.Parse(readLine) < 4.0)
            {
                print("should be liquid");
            }
            else if (float.Parse(readLine) >= 5.0){
                print("should be gas");
            }
        }
        catch(System.Exception){
        }
        
    }
}
