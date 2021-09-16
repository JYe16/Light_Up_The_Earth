using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    static public GameManager gm;
    public GameObject player;
    public int targetScore = 150;
    private int currentScore;
    public enum GameState
    {
        Playing,
        GameOver,
        Winning
    };
    public GameState gameState;
    public Text scoreText;
    
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
                scoreText.text = "Score: " + currentScore;
                if (currentScore >= targetScore)
                    gm.gameState = GameState.Winning;
                // else if ()
                //     gm.gameState = GameState.GameOver;
                break;
            case GameState.Winning:
                // TODO: load next level
                break;
            case GameState.GameOver:
                // TODO: restart menu
                break;
        }
    }

    public void AddScore(int value)
    {
        currentScore += value;
    }
}
