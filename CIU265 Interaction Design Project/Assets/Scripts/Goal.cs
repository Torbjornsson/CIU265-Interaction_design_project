using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    int count;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.tag == "Particle"){
            count++;
            GameObject.Find("Controller").GetComponent<Controller>().Score(count);
        }
    }

    void OnTriggerExit2D(Collider2D other){
        if (other.tag == "Particle" && count > 0){
            count--;
            GameObject.Find("Controller").GetComponent<Controller>().Score(count);
        }
    }
}
