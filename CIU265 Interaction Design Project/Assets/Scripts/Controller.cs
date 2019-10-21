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
    public ArrayList particles = new ArrayList();

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
        GameObject[] ps =GameObject.FindGameObjectsWithTag("Particle"); 
        foreach(GameObject p in ps){
            particles.Add(p);
        }
       blurController = effectCamera.GetComponent<BlurController>();
       meshWithText = GameObject.Find("MeshWithTextureFromCamera");
       textureWithShade = meshWithText.GetComponent<MeshRenderer>();

        //Start reading from serial monitor
        //sp.Open();
        //sp.ReadTimeout = 1;
    }

    // Update is called once per frame
    public void Update()
    {
        //string readLine = sp.ReadLine();
        //print(readLine);
        moveCamera();
    }

    public void moveCamera()
    {
        if (particles.Count == 0){
            return;
        }
        GameObject p = (GameObject)particles[0];
        
        Vector2 minPos = p.transform.position;
        Vector2 maxPos = p.transform.position;
        foreach(GameObject particle in particles){
            minPos = Vector2.Min(minPos, particle.transform.position);
            maxPos = Vector2.Max(maxPos, particle.transform.position);
        }
        Vector3 newPos = new Vector3((minPos.x + maxPos.x)/2, (minPos.y + maxPos.y)/2, -10);
        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, newPos, Time.deltaTime);
    }

    public void RemoveParticle(GameObject particle){
        particles.Remove(particle);
        Destroy(particle);
    }
}
