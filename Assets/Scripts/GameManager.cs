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
    [HideInInspector] public int currentLevel;
    public enum GameState
    {
        Playing,
        GameOver,
        Winning
    };
    public GameState gameState;
    public Text scoreText;
    public Text targetScoreText;
    public Text levelText;
    public Text plusScoreText;
    public int currentScore;
    
    public Button pauseBtn;

    public GameObject pausePanel;

    private bool isPause;

    // Start is called before the first frame update
    void Start()
    {
        if(gm == null) 
            gm = GetComponent<GameManager>();
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");
        currentLevel = 1;
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
                scoreText.text = currentScore.ToString();
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
            PlayerPrefs.SetInt("total", newTotal);
        }
        else
        {
            newTotal = value;
            PlayerPrefs.SetInt("total", newTotal);
        }
        currentScore += value;
    }

    public void PlusScore(int value)
    {
        Time.timeScale = 1.5f;
        StartCoroutine(PlusScoreAnimation(value));
    }

    IEnumerator PlusScoreAnimation(int value)
    {
        plusScoreText.enabled = true;
        plusScoreText.text = "+" + value;
        plusScoreText.GetComponentInChildren<ParticleSystem>().Play();
        AddScore(value);
        yield return new WaitForSeconds(1);
        plusScoreText.enabled = false;
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
        plusScoreText.enabled = false;
        plusScoreText.GetComponentInChildren<ParticleSystem>().Stop();
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
        levelText.text = currentLevel.ToString();
    }
    
    public void PauseGame()
    {
        isPause = !isPause;

        if (isPause == true)
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
