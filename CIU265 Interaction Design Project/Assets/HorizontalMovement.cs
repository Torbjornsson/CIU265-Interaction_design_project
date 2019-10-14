using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMovement : MonoBehaviour
{
    public float moveSpeed;
    public int top;
    public float bottom;
    Vector3 right = new Vector3(1.0f, 0.0f, 0.0f);
    Vector3 left = new Vector3(-1.0f, 0.0f, 0.0f);
    private Vector3 moveDirection;
    float waitUntilTime = -1f;
    
    void Update ()
    {
        move();
    }
    void move () 
    { 
        if (transform.position.x >= 10) 
        {
            waitUntilTime = Time.time + 10f; //3 second(s) in the future.
            delay();
            moveDirection = left; 
        } 
        else if (transform.position.x <= 0 ) 
        {
            waitUntilTime = Time.time + 10f; //3 second(s) in the future.
            delay();
            moveDirection = right;
        } 
        
        transform.Translate (moveDirection * Time.deltaTime * moveSpeed, Space.World);
    } 

    void delay(){
          if (Time.time > waitUntilTime) return;

    }
}
