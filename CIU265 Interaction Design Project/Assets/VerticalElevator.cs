using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalElevator : MonoBehaviour
{
    public float moveSpeed;
    public int top;
    public float bottom;
    Vector3 upwards = new Vector3(0.0f, 1.0f, 0.0f);
    Vector3 downwards = new Vector3(0.0f, -1.0f, 0.0f);
    private Vector3 moveDirection;
    float waitUntilTime = -1f;
    
    void Update ()
    {
        move();
    }
    void move () 
    { 
        if (transform.position.y >= 10) 
        {
            waitUntilTime = Time.time + 10f; //3 second(s) in the future.
            delay();
            moveDirection = downwards; 
        } 
        else if (transform.position.y <= 0 ) 
        {
            waitUntilTime = Time.time + 10f; //3 second(s) in the future.
            delay();
            moveDirection = upwards;
        } 
        
        transform.Translate (moveDirection * Time.deltaTime * moveSpeed, Space.World);
    } 

    void delay(){
          if (Time.time > waitUntilTime) return;

    }

}
