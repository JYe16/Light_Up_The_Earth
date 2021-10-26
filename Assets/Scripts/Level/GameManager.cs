using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static public GameManager gm;
    
    public Slider timeBar;
    public float timeRemaining;
    [HideInInspector]public int currentScore;
    [HideInInspector]public GameObject currentGoal;
    [HideInInspector] public int currentLevel;
    public enum GameState
    {
        Playing,
        GameOver,
        Winning,
        Pausing
    };
    public GameState gameState;
    public Text scoreText;
    public Text levelText;
    public GameObject platform;
    //declare skyboxes
    public Material[] skyboxes;
    //mesh materials
    public Mesh[] platforms;
    public GameObject gamePlayMusic;
    
    private int targetScore;
    private GameObject player;

    void Start()
    {
        //generate a random number for selecting skybox
        RenderSettings.skybox = skyboxes[Random.Range(0, skyboxes.Length)];
        //TODO: get correct mesh files and then uncomment the next line
        // platform.GetComponent<MeshFilter>().mesh = platforms[Random.Range(0, platforms.Length)];
        if(gm == null) 
            gm = GetComponent<GameManager>();
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");
        if (PlayerPrefs.HasKey("level"))
        {
            currentLevel = PlayerPrefs.GetInt("level") + 1;
            //targetScore = 30 + (10 * (currentLevel - 1));
            targetScore = 30 + (90 * (currentLevel - 1));
        }
        else
        {
            currentLevel = 1;
            targetScore = 30;
        }
        PlayerPrefs.SetInt("level", currentLevel);
        InitUI();
        // init spawner after PlayerPrefs set
        InitSpawner();
        gm.gameState = GameState.Playing;
        //start playing music if no bgm is currently playing
        if(Tutorial.isPlaying == false)
        {
            DontDestroyOnLoad(gamePlayMusic.gameObject);
            if(PlayerPrefs.GetInt("music") == 1)
            {
                gamePlayMusic.gameObject.GetComponent<AudioSource>().Play();
            }
            Tutorial.isPlaying = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (gameState)
        {
            case GameState.Pausing:
                // pause game without time scale
                break;
            case GameState.Playing:
                //update score text
                scoreText.text = currentScore + "/" + targetScore;
                //update time remaining
                if(timeRemaining > 0){
                    timeRemaining -= Time.deltaTime;
                    timeBar.value = timeRemaining;
                }
                else{
                    //if no time left and not enough points collected, player lost
                    if (currentScore < targetScore)
                    {
                        gm.gameState = GameState.GameOver;
                    }
                    else
                    {
                        gm.gameState = GameState.Winning;
                    }
                }
                break;
            case GameState.Winning:
                //calculate the base score for the next level
                int newBase = currentScore - targetScore;
                //save the new base score to system
                PlayerPrefs.SetInt("baseScore", newBase);
                //jump to the winning page
                SceneManager.LoadScene("Pass_Level");
                break;
            case GameState.GameOver:
                GameObject playMusic = GameObject.Find("gamePlayMusic");
                Destroy(playMusic.gameObject);
                Tutorial.isPlaying = false;
                SceneManager.LoadScene("GameOver");
                break;
        }
    }

    public void PauseGame(bool isPause)
    {
        gameState = isPause ? GameState.Pausing : GameState.Playing;
    }
    
    public void AddScore(int value)
    {
        //update the playerprefs also
        int newTotal;
        //update the total score when the player has captured something
        if (PlayerPrefs.HasKey("total"))
        {
            newTotal = PlayerPrefs.GetInt("total") + value;
        }
        else
        {
            newTotal = value;
        }
        PlayerPrefs.SetInt("total", newTotal);
        currentScore += value;
    }

    public void AddRemainingTime(int bounsTime)
    {
        GameObject handle = GameObject.FindGameObjectWithTag("TimeHandle");
        if (handle != null)
        {
            Vector3 maxScale = handle.transform.localScale * 1.5f;
            handle.transform.DOScale(maxScale, 0.5f);
            handle.transform.DOScale(Vector3.one, 0.5f);
        }
        timeRemaining += bounsTime;
    }

    private void InitUI()
    {
        //load game data from playerprefs
        if (PlayerPrefs.HasKey("baseScore"))
        {
            currentScore = PlayerPrefs.GetInt("baseScore");
        }
        else
        {
            currentScore = 0;   
        }
        scoreText.text = currentScore + "/" + targetScore;
        timeBar.value = timeRemaining;
        timeBar.maxValue = timeRemaining;
        levelText.text = "level " + currentLevel;
    }

    private void InitSpawner()
    {
        RandomSpawner spawner = GetComponent<RandomSpawner>();
        spawner.platformTransform = platform.transform;
        spawner.enabled = true;
    }
}
