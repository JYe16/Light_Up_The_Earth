using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
                statusText.text = "You Win!!";
                // TODO: load next level
                break;
            case GameState.GameOver:
                statusText.text = "You Lost!!";
                break;
        }
    }

    public void AddScore(int value)
    {
        currentScore += value;
    }

    public void AddRemainingTime(int bounsTime)
    {
        timeRemaining += bounsTime;
    }

    private void initUI()
    {
        currentScore = 0;
        targetScoreText.text = targetScore.ToString();
        timeBar.value = timeRemaining;
        timeBar.maxValue = timeRemaining;
    }
}
