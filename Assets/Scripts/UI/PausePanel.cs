using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanel : MonoBehaviour
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
    
	/*
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	*/
}
