using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillParticles : MonoBehaviour
{   
    //This shit ain't working.
    // public ChangeState changeStateScript;
    // void onStart(){
    //     changeStateScript = Controller.GetComponent<ChangeState>();
    // }
    private Controller master;

    void onStart(){
        master = GameObject.Find("Controller").GetComponent<Controller>();
    }

    void OnCollisionEnter2D(Collision2D col) 
    {
        master = GameObject.Find("Controller").GetComponent<Controller>();
        if(col.gameObject.tag == "Particle" && col.gameObject != null)
        {
            master.RemoveParticle(col.gameObject);
        }
    }
}
