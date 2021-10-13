using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static public GameManager gm;
    
    public Slider timeBar;
    private int targetScore;
    public float timeRemaining;
    [HideInInspector] public GameObject currentGoal;
    [HideInInspector] public int currentLevel;
    public enum GameState
    {
        Playing,
        GameOver,
        Winning
    };
    public GameState gameState;
    public Text scoreText;
    public Text levelText;
    public int currentScore;
    public Button pauseBtn;
    public GameObject pausePanel;

    private bool isPause;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        if(gm == null) 
            gm = GetComponent<GameManager>();
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");
        if (PlayerPrefs.HasKey("level"))
        {
            currentLevel = PlayerPrefs.GetInt("level") + 1;
            targetScore = 30 + (10 * (currentLevel - 1));
        }
        else
        {
            currentLevel = 1;
            targetScore = 30;
        }
        PlayerPrefs.SetInt("level", currentLevel);
        initUI();
        gm.gameState = GameState.Playing;
        isPause = false;
        pauseBtn.onClick.AddListener(PauseGame);
        pausePanel.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        switch (gameState)
        {
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
                //TODO: replace this scene with GameOver
                SceneManager.LoadScene("GameOver");
                break;
        }
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
        if(handle != null)
            handle.GetComponent<SmoothScale>().enabled = true;
        timeRemaining += bounsTime;
    }

    private void initUI()
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
    
    public void PauseGame()
    {
        isPause = !isPause;

        if (isPause)
        {
            // PauseButton.image.sprite = Resources.Load<Sprite>("Sprites/resume");
            pausePanel.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            // PauseButton.image.sprite = Resources.Load<Sprite>("Sprites/pause");
            pausePanel.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
