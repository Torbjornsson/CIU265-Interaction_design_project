using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    
    private List<Material> m;
    // Start is called before the first frame update
    void Start()
    {
       particles = GameObject.FindGameObjectsWithTag("Particle");
       blurController = effectCamera.GetComponent<BlurController>();
       meshWithText = GameObject.Find("MeshWithTextureFromCamera");
       textureWithShade = meshWithText.GetComponent<MeshRenderer>();
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
        }
        blurController.blurSpread = 0.2f;
        blurController.iterations = 5;
        textureWithShade.materials[0].SetColor("_Color", gas);
    }
}
