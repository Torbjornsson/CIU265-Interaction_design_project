using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System;

public class ChangeStateParticle : MonoBehaviour
{
    public Controller master;
    public float iceThreshold, waterThreshold;
    private float iceSize, waterSize, gasSize;
    private Color ice, water, gas;
    private BlurController blurController;
    private MeshRenderer textureWithShade;
    private int state = 0;

    private SerialPort sp;
    private float inc = 0.0f;

    // Start is called before the first frame update
    public void Start()
    {

    }

    // Update is called once per frame
    public void Update()
    {
        if (sp.IsOpen)
        {
        
        try{
            if (inc > 1.0f){
                inc = 0.0f;
            }
            else{
                inc += 0.05f * Time.deltaTime;
            }
            //string readLine = sp.ReadLine();
            //print(readLine);
            
            if (master.rpm < iceThreshold && state > 0)
            {
                changeToIce();
                //sp.Write("i");
            }
            else if (master.rpm >= iceThreshold && master.rpm < waterThreshold && state != 1)
            {
                changeToWater();
                //sp.Write("w");
            }
            else if (master.rpm >= waterThreshold && state < 2){
                changeToGas();
                //sp.Write("g");
            }
        }
        catch{

        }
        }
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
    }
    

    public void changeToIce(){
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        gameObject.transform.localScale = new Vector3(iceSize, iceSize, 1);
        blurController.blurSpread = 0.1f;
        blurController.iterations = 3;
        textureWithShade.materials[0].SetColor("_Color", ice);
        state = 0;
    }
    public void changeToWater(){
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
        gameObject.GetComponent<CircleCollider2D>().enabled = true;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        gameObject.transform.localScale = new Vector3(waterSize, waterSize, 1);
        blurController.blurSpread = 0.5f;
        blurController.iterations = 7;
        textureWithShade.materials[0].SetColor("_Color", water);
        state = 1;
    }

    public void changeToGas(){
        gameObject.GetComponent<Rigidbody2D>().gravityScale = -1;
        gameObject.GetComponent<CircleCollider2D>().enabled = true;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        gameObject.transform.localScale = new Vector3(gasSize, gasSize, 1);
        blurController.blurSpread = 0.2f;
        blurController.iterations = 5;
        textureWithShade.materials[0].SetColor("_Color", gas);
        state = 2;
    }
    
    void OnTriggerEnter2D(Collider2D other){
        if (other.name.Contains("Room")){
            blurController = other.GetComponentInChildren<BlurController>();
            textureWithShade = other.GetComponentInChildren<MeshRenderer>();
            iceThreshold = other.GetComponent<Room>().iceThreshold;
            waterThreshold = other.GetComponent<Room>().waterThreshold;

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

    public void startParticle(){
        master = GameObject.Find("Controller").GetComponent<Controller>();
        this.iceSize = master.iceSize;
        this.waterSize = master.waterSize;
        this.gasSize = master.gasSize;
        this.ice = master.ice;
        this.water = master.water;
        this.gas = master.gas;

        this.blurController = master.blurController;
        this.textureWithShade = master.textureWithShade;
        this.sp = master.sp;
        changeToIce();
    }
}
