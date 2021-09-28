using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static public GameManager gm;
    public GameObject player;
    public int targetScore;
    private int currentScore;
    public float timeRemaining;
    public enum GameState
    {
        Playing,
        GameOver,
        Winning
    };
    public GameState gameState;
    public Text scoreText;
    public Text timeText;

    // Start is called before the first frame update
    void Start()
    {
        if(gm == null) 
            gm = GetComponent<GameManager>();
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");
		//load game data from playerprefs
        if (PlayerPrefs.HasKey("baseScore"))
        {
            currentScore = PlayerPrefs.GetInt("baseScore");
        }
        else
        {
            currentScore = 0;   
        }
        gm.gameState = GameState.Playing;
    }

    // Update is called once per frame
    void Update()
    {
        switch (gameState)
        {
            case GameState.Playing:
                //update scure text
                scoreText.text = "Score: " + currentScore.ToString() + "/" + targetScore;
                //update time remaining
                if(timeRemaining > 0){
                    timeRemaining -= Time.deltaTime;
                    timeText.text = "Time: " +  timeRemaining.ToString("f0") + "s";
                }else{
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
}
