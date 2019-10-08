using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySwitchScript : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Physics2D.gravity = new Vector2(0,1);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            Physics2D.gravity = new Vector2(0, -9);
        }
    }
}
