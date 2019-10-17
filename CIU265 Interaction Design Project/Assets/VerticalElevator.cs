using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalElevator : MonoBehaviour
{
    public float moveSpeed;
    public int top;
    public float bottom;
    public float waitTime;
    Vector3 upwards = new Vector3(0.0f, 1.0f, 0.0f);
    Vector3 downwards = new Vector3(0.0f, -1.0f, 0.0f);
    private Vector3 moveDirection;
    float waitUntilTime = -1f;

    private void Start()
    {
        moveDirection = upwards;
    }
    
    void Update ()
    {
        Move();
    }

    void Move()
    {
        if ( transform.position.y > top && moveDirection == upwards )
        {
            waitUntilTime = Time.time + waitTime;
            moveDirection = downwards;
        }
        else if ( transform.position.y < bottom && moveDirection == downwards )
        {
            waitUntilTime = Time.time + waitTime;
            moveDirection = upwards;
        }
        if ( Time.time > waitUntilTime )
            transform.Translate( moveDirection * Time.deltaTime * moveSpeed, Space.World );
    }
    
    // void Update ()
    // {
    //     move();
    // }
    // void move () 
    // { 
    //     if (transform.position.y >= top) 
    //     {
    //         waitUntilTime = Time.time + 10f; //3 second(s) in the future.
    //         delay();
    //         moveDirection = downwards; 
    //     } 
    //     else if (transform.position.y <= bottom ) 
    //     {
    //         waitUntilTime = Time.time + 10f; //3 second(s) in the future.
    //         delay();
    //         moveDirection = upwards;
    //     } 
        
    //     transform.Translate (moveDirection * Time.deltaTime * moveSpeed, Space.World);
    // } 

    // void delay(){
    //       if (Time.time > waitUntilTime) return;

    // }

}
