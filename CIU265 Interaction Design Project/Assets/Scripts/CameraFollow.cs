using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Camera effectCamera;
    public GameObject meshWithTexture;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 vec = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
        effectCamera.transform.position = gameObject.transform.position;

        meshWithTexture.transform.position = vec;
    }
}
