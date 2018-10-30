using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class GameController : MonoBehaviour {

    [SerializeField]
    AudioSource soundSource;
    [SerializeField]
    AudioClip[] soundsGame;
    [SerializeField]
    HighscoreManager highscores;
    [SerializeField]
    private bool inGame = false;
    [SerializeField]
    private float respawnRate;
    [SerializeField]
    private float timeAlive;
    [SerializeField]
    private float timeAliveRate;
    [SerializeField]
    private float speed;
    [SerializeField]
    MolesHole[] holes;
    [SerializeField]
    Hammer hammer;
    [SerializeField]
    Text playerPoints;
    [SerializeField]
    Text displayTime;
    [SerializeField]
    float gameTime = 60f;
    [SerializeField]
    float gameRate;
    float timer;
    private float remainTime;
    private bool endGame;
    
    float timeStamp;
    [SerializeField]
    DefaultTrackableEventHandler[] gameObjects;
    [SerializeField]
    Button startButton;
    [SerializeField]
    Button exitButton;

    // Use this for initialization
    void Start () {

        inGame = false;
        playerPoints.gameObject.SetActive(false);
        displayTime.gameObject.SetActive(false);

        highscores.showScores();
        endGame = false;
        displayTime.text = "Time: " + gameTime.ToString("#.00");
        playerPoints.text = "Points: 0";
        timeStamp = Time.time;

        
        soundSource.clip = soundsGame[0];
        soundSource.Play();


    }
	
	// Update is called once per frame
	void Update () {

        if (!endGame)
        {
            remainTime = gameTime - (Time.time - timer);
        }
       
        if(inGame && remainTime <= 0)
        {
            gameOver();
        }

        if (inGame && !endGame)
        {
            if (timeStamp <= Time.time)
            {
                if (createMole())
                    timeStamp = Time.time + respawnRate;
            }

            gameEvolution();
            updateCanvas();
        }

	}

    bool createMole()
    {
        int tryRespawn = Random.Range(0, holes.Length);
        int type = Random.Range(0, 2);
        type = 0;
        bool tryPlace = false;
        int next = tryRespawn;
        do
        {
            tryPlace = holes[next].respawnMole(type);
            if (!tryPlace)
            {
                next++;
                next = next % holes.Length;
                if (next == tryRespawn)
                    return false;
            }

        } while (!tryPlace);


        return true;
    }

    void updateCanvas()
    {
        this.playerPoints.text = "Points: " + hammer.Points.ToString();
        this.displayTime.text = "Time: " + remainTime.ToString("#.00");
    }

    void gameEvolution()
    {
        float minRespwan = remainTime / gameRate + 0.5f;
        float minAlive = remainTime / timeAliveRate;

        float minSpeed = speed + (timeAliveRate/ remainTime)*0.25f;
        
        if (minRespwan < 1.0f)
        {
            respawnRate = 1.0f;
        }
        else
        {
            respawnRate = minRespwan;
        }

        if (minAlive < 1.0f)
        {
            timeAlive = 1.0f;
        }
        else
        {
            timeAlive = minAlive;
        }

        if (minSpeed > 50f)
        {
            speed = 50f;
        }else
        {
            speed = minSpeed;
        }
        

        for (int i = 0; i < holes.Length; i++)
        {
            holes[i].TimeAlive = timeAlive;
            //holes[i].Speed = speed;
        }
    }

    void gameOver()
    {
        endGame = true;
        inGame = false;
        displayTime.text = "Time: 0";
        this.playerPoints.text = "Points: " + hammer.Points.ToString();
        highscores.InputName.gameObject.SetActive(true);
        highscores.InputName.interactable = true;
    }

    public void addScore()
    {
        playerPoints.gameObject.SetActive(false);
        displayTime.gameObject.SetActive(false);

        highscores.addScore(hammer.Points);
        startButton.gameObject.SetActive(true);
        exitButton.gameObject.SetActive(true);
    }

    public bool checkForAllObjects()
    {
        for(int i=0; i < gameObjects.Length; i++)
        {
            if (!gameObjects[i].Active)
                return false;
        }

        return true;
    }

    public void startGame()
    {
        if (!checkForAllObjects())
        {
            print("Missing");
            return;
        }
        startButton.gameObject.SetActive(false);
        exitButton.gameObject.SetActive(false);

        playerPoints.gameObject.SetActive(true);
        displayTime.gameObject.SetActive(true);

        soundSource.Stop();
        soundSource.clip = soundsGame[1];
        soundSource.Play();

        hammer.Points = 0;
        inGame = true;
        endGame = false;
        timer = Time.time;
        displayTime.text = "Time: " + gameTime.ToString("#.00");
        playerPoints.text = "Points: 0";
        timeStamp = Time.time;
        highscores.hideScores();
    }

    public void exitGame()
    {
        print("Exit");
        Application.Quit();
    }
}
