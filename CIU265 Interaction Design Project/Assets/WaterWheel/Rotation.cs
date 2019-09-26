using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public int rotationSpeed;
    void Update (){
        transform.Rotate (0,0,rotationSpeed*Time.deltaTime); //rotates 50 degrees per second around z axis
    }

}
