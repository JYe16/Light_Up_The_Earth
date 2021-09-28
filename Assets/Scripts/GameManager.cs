using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static public GameManager gm;
    
    public GameObject player;
    public Slider timeBar;
    public int targetScore;
    public float timeRemaining;
    [HideInInspector] public GameObject currentGoal;
    public enum GameState
    {
        Playing,
        GameOver,
        Winning
    };
    public GameState gameState;
    public Text scoreText;
	public Text statusText;
    public Text targetScoreText;
    
    private int currentScore;

    // Start is called before the first frame update
    void Start()
    {
        if(gm == null) 
            gm = GetComponent<GameManager>();
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");
        initUI();
        gm.gameState = GameState.Playing;
    }

    // Update is called once per frame
    void Update()
    {
        switch (gameState)
        {
            case GameState.Playing:
                //update score text
                scoreText.text = currentScore.ToString();
                //update time remaining
                if(timeRemaining > 0){
                    timeRemaining -= Time.deltaTime;
                    timeBar.value = timeRemaining;
                }
                else{
                    //if no time left and not enough points collected, player lost
                    if (currentScore < targetScore){
                        gm.gameState = GameState.GameOver;
                    }else{
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
                SceneManager.LoadScene("WinPage");
                break;
            case GameState.GameOver:
                //load the score rank scene
                PlayerPrefs.SetInt("Score", currentScore);
                //TODO: replace this scene with GameOver
                SceneManager.LoadScene("EnterName");
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
            PlayerPrefs.SetInt("total", newTotal);
        }
        else
        {
            newTotal = value;
            PlayerPrefs.SetInt("total", newTotal);
        }
        currentScore += value;
    }

    public void AddRemainingTime(int bounsTime)
    {
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
        targetScoreText.text = targetScore.ToString();
        timeBar.value = timeRemaining;
        timeBar.maxValue = timeRemaining;
    }
}
