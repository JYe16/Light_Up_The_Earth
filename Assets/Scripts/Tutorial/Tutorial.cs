using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    /*
     *
     *  step order:
     *  0. move
     *  1. props
     *  2. look
     *  3. shoot
     *  4. timer
     *
     */
    public Button skipButton;
    public Button nextStep;
    public Button startGame;

    public Canvas moveHandle;
    public Canvas lookHandle;
    public Canvas shootHandle;
    public Canvas propsArea;
    public Canvas timer;

    public Canvas moveHighlight;
    public Canvas lookHighlight;
    public Canvas shootHighlight;
    public Canvas propsHighlight;
    public Canvas timerHighlight;
    public GameObject tutorialText;

    public int currentStep = 0;
    public GameObject gamePlayMusic;
    public static bool isPlaying = false;
    // Start is called before the first frame update
    void Start()
    {
        //by default, show skip button and next step button  
        skipButton.onClick.AddListener(start_game);
        nextStep.onClick.AddListener(showNextStep);
        startGame.onClick.AddListener(start_game);
        //start playing background music
        if(!isPlaying)
        {
            DontDestroyOnLoad(gamePlayMusic.gameObject);
            if(PlayerPrefs.GetInt("music") == 1)
            {
                gamePlayMusic.gameObject.GetComponent<AudioSource>().Play();
            }
            isPlaying = true;
        }
        //destroy the launchMusic
        //GameObject launchMusic = GameObject.Find("gameLaunchMusic");
        //Destroy(launchMusic.gameObject);
    }

    void start_game()
    {
        // SceneManager.LoadScene("Level_00_Scene");
        SceneManager.LoadScene("LoadingPage");
    }

    void showNextStep()
    {
        switch (currentStep)
        {
            case 0:
                moveHandle.overrideSorting = true;
                currentStep++;
                skipButton.gameObject.SetActive(false);
                moveHighlight.gameObject.SetActive(true);
                tutorialText.gameObject.SetActive(false);
                break;
            case 1:
                moveHandle.overrideSorting = false;
                propsArea.overrideSorting = true;
                currentStep++;
                moveHighlight.gameObject.SetActive(false);
                propsHighlight.gameObject.SetActive(true);
                break;
            case 2:
                propsArea.overrideSorting = false;
                lookHandle.overrideSorting = true;
                currentStep++;
                propsHighlight.gameObject.SetActive(false);
                lookHighlight.gameObject.SetActive(true);
                break;
            case 3:
                lookHandle.overrideSorting = false;
                shootHandle.overrideSorting = true;
                currentStep++;
                lookHighlight.gameObject.SetActive(false);
                shootHighlight.gameObject.SetActive(true);
                break;
            case 4:
                shootHandle.overrideSorting = false;
                timer.overrideSorting = true;
                currentStep = 10;
                nextStep.gameObject.SetActive(false);
                startGame.gameObject.SetActive(true);
                shootHighlight.gameObject.SetActive(false);
                timerHighlight.gameObject.SetActive(true);
                break;

        }
    }
}
