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
    public ArrayList particles = new ArrayList();
    private Text[] scores;

    public Color ice, water, gas;
    public MeshRenderer textureWithShade;

    private ArrayList highscorelist;
    
    bool isIce = false;
    bool isWater = false;
    bool isGas = false;
    GameObject highscore;
    public static String serialPort =  "/dev/cu.usbmodem1421";
    public string readLine;
    public float rpm;

    private GameObject Pointy;

    private Vector3 zAxis = new Vector3(0,0,1);

    //Serial Port init
    //name of serial port is different between computers, check under Port in Arduino IDE

    //Serial port for Mac, right USB
    //public SerialPort sp = new SerialPort(serialPort, 115200);

    //Serial port for Windows, xx USB
    public SerialPort sp = new SerialPort("COM3", 115200);
    //public SerialPort sp;

    // Start is called before the first frame update
    public void Start()
    {
        // foreach (string s in SerialPort.GetPortNames()){
        //     if (s.Contains("COM")){
        //         sp = new SerialPort(s, 115200);
        //         break;
        //     }
        // }
        highscorelist = new ArrayList();
        highscore = GameObject.Find("High Score");
        highscore.SetActive(false);
        blurController = effectCamera.GetComponent<BlurController>();
        meshWithText = GameObject.Find("MeshWithTextureFromCamera");
        textureWithShade = meshWithText.GetComponent<MeshRenderer>();
        //sp.Open();
        //sp.ReadTimeout = 1;
        GameObject[] ps = GameObject.FindGameObjectsWithTag("Particle"); 
        foreach(GameObject p in ps){
            if (p != null){
                particles.Add(p);
        
                p.GetComponent<ChangeStateParticle>().startParticle();
            }
       }
        maxParticles = particles.Count;
        //Start reading from serial monitor
        Pointy = GameObject.Find("Pointy Thing");
        rpm = 0.0f;
    }

    // Update is called once per frame
    public void Update()
    {
        try{
            readLine = sp.ReadLine();
            if (readLine == "restart"){
                Time.timeScale = 1;
                Application.LoadLevel (Application.loadedLevel);
            }
            //print(readLine);
            if (readLine != "i" && readLine != "w" && readLine != "g" && readLine != "restart")
                float.TryParse(readLine, out rpm);
            
            Debug.Log(rpm);
            if (rpm < 0.05){
                sp.Write("i");
                Pointy.transform.rotation = Quaternion.AngleAxis(60, zAxis);
            }else if (rpm < 3){
                sp.Write("w");
                Pointy.transform.rotation = Quaternion.AngleAxis(0, zAxis);
            }else if (rpm >= 3){
                sp.Write("g");
                Pointy.transform.rotation = Quaternion.AngleAxis(-60, zAxis);
            }
        }catch{

        }
        if (particles.Count > 0)
            moveCamera();

        if (Input.GetKeyDown("9")){
            ClearHighScoreList();
        }
    }

    public void moveCamera(){
        ArrayList positionsX = new ArrayList();
        ArrayList positionsY = new ArrayList();
        foreach(GameObject particle in particles){
            positionsX.Add(particle.transform.position.x);
            positionsY.Add(particle.transform.position.y);
        }
        positionsX.Sort();
        positionsY.Sort();
        
        float medianX = (float)positionsX[positionsX.Count/2]; 
        float medianY = (float)positionsY[positionsY.Count/2];

        Vector3 newPos = new Vector3(medianX, medianY, -10);
        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, newPos, Time.deltaTime);
    }

    public void RemoveParticle(GameObject particle){
        particles.Remove(particle);
        Destroy(particle);
        if (particles.Count == 0){
            EndLevel();
        }
    }

    private void EndLevel(){
        highscorelist = GetHighScoreList();
        highscore.SetActive(true);
        GameObject yourScore = GameObject.Find("Your Score");
        Text textScore = yourScore.GetComponent<Text>();
        textScore.text = "0";
        UpdateHighscoreList();
        Time.timeScale = 0;
    }

    public void Score(int numberOfParticles){
        if (numberOfParticles >= particles.Count){
            highscorelist = GetHighScoreList();
            highscore.SetActive(true);
            GameObject yourScore = GameObject.Find("Your Score");
            Text textScore = yourScore.GetComponent<Text>();
            int score = calculateScore(numberOfParticles);
            textScore.text = score.ToString();
            SaveHighScore(score);
            UpdateHighscoreList();
            Time.timeScale = 0;
        }
    }

    private int calculateScore(int numberOfParticles){
        float time = Time.timeSinceLevelLoad;
        int score = baseScore;        
        float scoremultiplier = (float)numberOfParticles / (float)maxParticles;

        score = (int)Mathf.Round(score * scoremultiplier);
        score -= (int)Mathf.Round(time);
        return Mathf.Max(0, score * scoreScale);
    }

    private void SaveHighScore(int score){
        int i = 0;
        while (i < leaderboardsize && i < highscorelist.Count){
            if ((int)highscorelist[i] < score){
                highscorelist.Insert(i, score);
                PlayerPrefs.SetInt("Score"+i, score);
                break;
            }
            i++;
        }
        if (i < leaderboardsize && i >= highscorelist.Count){
            highscorelist.Add(score);
            PlayerPrefs.SetInt("Score"+i, score);
        }
    }

    private ArrayList GetHighScoreList(){
        ArrayList list = new ArrayList();
        int i = 0;

        while (i < leaderboardsize && PlayerPrefs.HasKey("Score"+i)){
            list.Add(PlayerPrefs.GetInt("Score"+i));
            i++;
        }

        return list;
    }

    private void UpdateHighscoreList(){
        for (int i = 0; i < leaderboardsize && i < highscorelist.Count; i++){
            Text text = GameObject.Find("Score"+i).GetComponent<Text>();
            int test = (int)highscorelist[i];
            text.text = (i+1) + ": " + test.ToString();
        }
    }

    private void ClearHighScoreList(){
        highscorelist = GetHighScoreList();
        for(int i = 0; i < highscorelist.Count; i++){
            PlayerPrefs.DeleteKey("Score"+i);
        }
        highscorelist.Clear();
    }

    void OnApplicationQuit(){
        PlayerPrefs.Save();
    }
}
