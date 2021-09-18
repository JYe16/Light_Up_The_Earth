using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        currentScore = 0;
        gm.gameState = GameState.Playing;
    }

    // Update is called once per frame
    void Update()
    {
        switch (gameState)
        {
            case GameState.Playing:
                //update scure text
                scoreText.text = "Score: " + currentScore.ToString();
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
                Debug.Log("You Win!");
                // TODO: load next level
                break;
            case GameState.GameOver:
                Debug.Log("You Lost!");
                break;
        }
    }

    public void AddScore(int value)
    {
        currentScore += value;
    }
}
