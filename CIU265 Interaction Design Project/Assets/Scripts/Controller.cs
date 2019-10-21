using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO.Ports;
using System;

public class Controller : MonoBehaviour
{
    public Camera effectCamera;
    public Camera mainCamera;
    public GameObject meshWithText;
    public BlurController blurController;
    public int state = 0;
    public float iceSize, waterSize, gasSize;
    public GameObject[] particles;

    public Color ice, water, gas;
    public MeshRenderer textureWithShade;
    
    bool isIce = false;
    bool isWater = false;
    bool isGas = false;

    public static String serialPort =  "/dev/cu.usbmodem1421";

    //Serial Port init
    //name of serial port is different between computers, check under Port in Arduino IDE

    //Serial port for Mac, right USB
    public SerialPort sp = new SerialPort(serialPort, 115200);

    //Serial port for Windows, xx USB
    // SerialPort sp = new SerialPort("COM3", 115200);

    // Start is called before the first frame update
    public void Start()
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
    public void Update()
    {
        /* 
        try{
            string readLine = sp.ReadLine();
            print(readLine);
            
            if (float.Parse(readLine) < 0.05)
            {
                changeToIce();
            }
            else if (float.Parse(readLine) > 0.05 && float.Parse(readLine) < 1.0)
            {
                changeToWater();
            }
            else if (float.Parse(readLine) >= 1.0){
                changeToGas();
            }
        }
        catch(System.Exception){
        }
 */
        //Control particles with space:

        // if(Input.GetKeyDown("space")) 
        // {
        //     state++;
        //     if (state > 2){
        //         state = 0;
        //     }
        //     if (state == 0){
        //         changeToIce();
        //     }
        //     else if (state == 1){
        //         changeToWater();
        //     }
        //     else if (state == 2){
        //         changeToGas();
        //     }
        // }

        if(Input.GetKeyDown(KeyCode.LeftArrow)) 
        {
            changeToIce();
        }
        if(Input.GetKeyDown(KeyCode.DownArrow)) 
        {
            changeToWater();
        }
        if(Input.GetKeyDown(KeyCode.RightArrow)) 
        {
            changeToGas();
        }
        moveCamera();
    }

    public void changeToIce(){
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
    public void changeToWater(){
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

    public void changeToGas(){
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

    public void moveCamera(){
        Vector2 minPos = particles[0].transform.position;
        Vector2 maxPos = particles[0].transform.position;
        foreach(GameObject particle in particles){
            minPos = Vector2.Min(minPos, particle.transform.position);
            maxPos = Vector2.Max(maxPos, particle.transform.position);
        }
        Vector3 newPos = new Vector3((minPos.x + maxPos.x)/2, (minPos.y + maxPos.y)/2, -10);
        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, newPos, Time.deltaTime);
    }
}
