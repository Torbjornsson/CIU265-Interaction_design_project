using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeState : MonoBehaviour
{
    public Camera effectCamera;
    private BlurController blurController;
    public int state = 0;
    public float iceSize, waterSize, gasSize;
    public GameObject[] particles;
    // Start is called before the first frame update
    void Start()
    {
       particles = GameObject.FindGameObjectsWithTag("Particle");
       blurController = effectCamera.GetComponent<BlurController>();
       changeToIce();
    }

    // Update is called once per frame
    void Update()
    {
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
    }
    void changeToWater(){
        foreach(GameObject particle in particles){
            particle.GetComponent<Rigidbody2D>().gravityScale = 1;
            particle.GetComponent<CircleCollider2D>().enabled = true;
            particle.GetComponent<BoxCollider2D>().enabled = false;
            particle.transform.localScale = new Vector3(waterSize, waterSize, 1);
        }
        blurController.blurSpread = 0.2f;
    }

    void changeToGas(){
        foreach(GameObject particle in particles){
            particle.GetComponent<Rigidbody2D>().gravityScale = -1;
            particle.GetComponent<CircleCollider2D>().enabled = true;
            particle.GetComponent<BoxCollider2D>().enabled = false;
            particle.transform.localScale = new Vector3(gasSize, gasSize, 1);
        }
        blurController.blurSpread = 0.3f;
    }
}
