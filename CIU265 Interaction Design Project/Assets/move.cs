using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class move : MonoBehaviour
{
    SerialPort sp = new SerialPort("/dev/cu.usbmodem1421", 115200);
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
            string goLeft = "go left";
            string goRight = "go right";
            print(readLine);
            if (readLine.Contains(goLeft))
            {
                Vector3 position = this.transform.position;
                position.x--;
                this.transform.position = position;
            }
            else if (readLine.Contains(goRight))
            {
                Vector3 position = this.transform.position;
                position.x++;
                this.transform.position = position;
            }
        }
        catch(System.Exception){
        }

        try{

        }
        catch(System.Exception){

        }
        
    }
}
