using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO.Ports;
using System;

public class ChangeState : MonoBehaviour
{
    public Camera effectCamera;
    public GameObject meshWithText;
    private BlurController blurController;
    public int state = 0;
    public float iceSize, waterSize, gasSize;
    public GameObject[] particles;

    public Color ice, water, gas;
    public MeshRenderer textureWithShade;
    
    bool isIce = false;
    bool isWater = false;
    bool isGas = false;

    //Serial Port init
    //name of serial port is different between computers, check under Port in Arduino IDE

    //Serial port for Mac, right USB
    //SerialPort sp = new SerialPort("/dev/cu.usbmodem1421", 115200);

    //Serial port for Windows, xx USB
    // SerialPort sp = new SerialPort("COM3", 115200);

    // Start is called before the first frame update
    void Start()
    {
       particles = GameObject.FindGameObjectsWithTag("Particle");
       blurController = effectCamera.GetComponent<BlurController>();
       meshWithText = GameObject.Find("MeshWithTextureFromCamera");
       textureWithShade = meshWithText.GetComponent<MeshRenderer>();
       changeToIce();

        //Start reading from serial monitor
        //sp.Open();
        //sp.ReadTimeout = 1;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        try{
            string readLine = sp.ReadLine();
            //print(readLine);
            
            if (float.Parse(readLine) < 0.1)
            {
                changeToIce();
            }
            else if (float.Parse(readLine) > 0.1 && float.Parse(readLine) < 2.0)
            {
                changeToWater();
            }
            else if (float.Parse(readLine) >= 2.0){
                changeToGas();
            }
        }
        catch(System.Exception){
        }
        */

        //Control particles with space:

        if(Input.GetKeyDown("space")) 
        {
            state++;
            if (state > 2){
                state = 0;
            }
            if (state == 0){
                changeToIce();
            }
            else if (state == 1){
                changeToWater();
            }
            else if (state == 2){
                changeToGas();
            }
        }
    }

    void changeToIce(){
        foreach(GameObject particle in particles){
            particle.GetComponent<Rigidbody2D>().gravityScale = 1;
            particle.GetComponent<CircleCollider2D>().enabled = false;
            particle.GetComponent<BoxCollider2D>().enabled = true;
            particle.transform.localScale = new Vector3(iceSize, iceSize, 1);
        }
        blurController.blurSpread = 0.1f;
        blurController.iterations = 3;
        textureWithShade.materials[0].SetColor("_Color", ice);
    }
    void changeToWater(){
        foreach(GameObject particle in particles){
            particle.GetComponent<Rigidbody2D>().gravityScale = 1;
            particle.GetComponent<CircleCollider2D>().enabled = true;
            particle.GetComponent<BoxCollider2D>().enabled = false;
            particle.transform.localScale = new Vector3(waterSize, waterSize, 1);
        }
        blurController.blurSpread = 0.5f;
        blurController.iterations = 7;
        textureWithShade.materials[0].SetColor("_Color", water);
    }

    void changeToGas(){
        foreach(GameObject particle in particles){
            particle.GetComponent<Rigidbody2D>().gravityScale = -1;
            particle.GetComponent<CircleCollider2D>().enabled = true;
            particle.GetComponent<BoxCollider2D>().enabled = false;
            particle.transform.localScale = new Vector3(gasSize, gasSize, 1);
        }
        blurController.blurSpread = 0.2f;
        blurController.iterations = 5;
        textureWithShade.materials[0].SetColor("_Color", gas);
    }
}
