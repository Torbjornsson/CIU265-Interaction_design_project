using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetParent : MonoBehaviour
{
    public GameObject rectangle;
    public GameObject parentSphere;
    // Start is called before the first frame update
    void Start()
    {
        rectangle.transform.SetParent(parentSphere.transform);
    }
}