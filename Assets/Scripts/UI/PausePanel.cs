using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PausePanel : MonoBehaviour
{
    public Text scoreText;
    public void ShowPausePanel(bool isShow)
    {
        gameObject.SetActive(isShow);
        // pause
        if (isShow)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    private void OnEnable()
    {
        scoreText.text = GameManager.gm.currentScore.ToString();
    }

    public void exitLevel()
    {
        SceneManager.LoadScene("StartPage");
    }
}
