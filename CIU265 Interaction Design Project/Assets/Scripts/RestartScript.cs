using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartScript : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("r")) { //If you press R
            //SceneManager.LoadScene("TestMapWater 1"); //Load scene called TestMapWater 1
            Application.LoadLevel (Application.loadedLevel);
            Time.timeScale = 1;
        }
        if (Input.GetKeyDown(KeyCode.Escape)) { //If you press R
            //SceneManager.LoadScene("TestMapWater 1"); //Load scene called TestMapWater 1
            Application.Quit();
        }
    }
}
