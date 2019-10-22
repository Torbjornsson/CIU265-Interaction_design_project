using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System.IO.Ports;
using System;

public class Controller : MonoBehaviour
{
    public Camera effectCamera;
    public Camera mainCamera;
    public GameObject meshWithText;
    public BlurController blurController;
    public int baseScore = 100;
    public int scoreScale = 1;

    private int leaderboardsize = 10;
    private int maxParticles;
    public float iceSize, waterSize, gasSize;
    public GameObject[] particles;

    public Color ice, water, gas;
    public MeshRenderer textureWithShade;

    private ArrayList highscorelist = new ArrayList();
    
    bool isIce = false;
    bool isWater = false;
    bool isGas = false;
    GameObject highscore;
    public static String serialPort =  "/dev/cu.usbmodem1421";

    //Serial Port init
    //name of serial port is different between computers, check under Port in Arduino IDE

    //Serial port for Mac, right USB
    public SerialPort sp = new SerialPort(serialPort, 115200);

    //Serial port for Windows, xx USB
    // SerialPort sp = new SerialPort("COM3", 115200);

    // Start is called before the first frame update
    public void Start()
    {
        highscore = GameObject.Find("High Score");
        highscore.SetActive(false);
        particles = GameObject.FindGameObjectsWithTag("Particle");
        blurController = effectCamera.GetComponent<BlurController>();
        meshWithText = GameObject.Find("MeshWithTextureFromCamera");
        textureWithShade = meshWithText.GetComponent<MeshRenderer>();

        foreach (var particle in particles){
            particle.GetComponent<ChangeStateParticle>().startParticle();
        }
        maxParticles = particles.Length;
        //Start reading from serial monitor
        //sp.Open();
        //sp.ReadTimeout = 1;
    }

    // Update is called once per frame
    public void Update()
    {
        //string readLine = sp.ReadLine();
        //print(readLine);
        moveCamera();
    }

    public void moveCamera(){
        Vector2 minPos = particles[0].transform.position;
        Vector2 maxPos = particles[0].transform.position;
        foreach(GameObject particle in particles){
            minPos = Vector2.Min(minPos, particle.transform.position);
            maxPos = Vector2.Max(maxPos, particle.transform.position);
        }
        Vector3 newPos = new Vector3((minPos.x + maxPos.x)/2, (minPos.y + maxPos.y)/2, -10);
        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, newPos, Time.deltaTime);
    }

    public void Score(int numberOfParticles){
        if (numberOfParticles >= particles.Length - 4){
            highscore.SetActive(true);
            GameObject yourScore = GameObject.Find("Your Score");
            Text textScore = yourScore.GetComponent<Text>();
            int score = calculateScore(numberOfParticles);
            textScore.text = score.ToString();
            SaveHighScore(score);
            UpdateHighscoreList();
        }
    }

    private int calculateScore(int numberOfParticles){
        float time = Time.timeSinceLevelLoad;
        int score = baseScore;        

        score -= numberOfParticles - maxParticles;
        score -= (int)Mathf.Round(time);
        return score * scoreScale;
    }

    private void SaveHighScore(int score){
        highscorelist = GetHighScoreList();
        int i = 0;
        
        while (i < leaderboardsize && i < highscorelist.Count){
            if ((int)highscorelist[i] <= score){
                highscorelist.Insert(i, score);
                PlayerPrefs.SetInt("Score"+i, score);
            }
            i++;
        }
        while (i < leaderboardsize && i >= highscorelist.Count){
            highscorelist.Add(score);
            PlayerPrefs.SetInt("Score"+i, score);
            i++;
        }
    }

    private ArrayList GetHighScoreList(){
        ArrayList list = new ArrayList();
        int i = 0;

        while (i < leaderboardsize && PlayerPrefs.HasKey("Score"+i)){
            list.Add(PlayerPrefs.GetInt("Score"+i));
        }

        return list;
    }

    private void UpdateHighscoreList(){
        highscorelist = GetHighScoreList();
        for (int i = 0; i < leaderboardsize && i < highscorelist.Count; i++){
            GameObject.Find("Score"+i).GetComponent<Text>().text = highscorelist[i].ToString();
        }

    }

    private void ClearHighScoreList(){
        highscorelist = GetHighScoreList();

        for(int i = 0; i < highscorelist.Count; i++){
            PlayerPrefs.DeleteKey("Score"+i);
        }
    }

    void OnApplicationQuit(){
        PlayerPrefs.Save();
    }
}
