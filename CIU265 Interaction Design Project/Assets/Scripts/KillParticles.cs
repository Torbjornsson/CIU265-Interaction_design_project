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

    void OnCollisionEnter2D(Collision2D col) 
    {
        if(col.gameObject.tag == "Particle")
        {
            Destroy(col.gameObject);
        }
    }
}
