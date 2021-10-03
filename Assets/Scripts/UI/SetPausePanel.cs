using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetPausePanel : MonoBehaviour
{
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

    public void exitLevel()
    {
        SceneManager.LoadScene("GameOver");
    }
}
