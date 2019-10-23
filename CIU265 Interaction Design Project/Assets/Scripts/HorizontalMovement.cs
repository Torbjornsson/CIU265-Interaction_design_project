using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMovement : MonoBehaviour
{
    public string startDirection;
    public float moveSpeed;
    public float left;
    public float right;
    public float waitTime;
    Vector3 rightMovement = new Vector3(1.0f, 0.0f, 0.0f);
    Vector3 leftMovement = new Vector3(-1.0f, 0.0f, 0.0f);
    private Vector3 moveDirection;
    float waitUntilTime = -1f;

    private void Start()
    {
        if(startDirection == "right"){
            moveDirection = rightMovement;
        }
        else if(startDirection == "left") 
        {
            moveDirection = rightMovement;
        }
            
    }
    
    void Update ()
    {
        Move();
    }

    void Move()
    {
        if ( transform.position.x > right && moveDirection == rightMovement )
        {
            waitUntilTime = Time.time + waitTime;
            moveDirection = leftMovement;
        }
        else if ( transform.position.x < left && moveDirection == leftMovement )
        {
            waitUntilTime = Time.time + waitTime;
            moveDirection = rightMovement;
        }
        if ( Time.time > waitUntilTime )
            transform.Translate( moveDirection * Time.deltaTime * moveSpeed, Space.World );
    }
}
