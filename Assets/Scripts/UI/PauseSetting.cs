using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseSetting : MonoBehaviour
{
    public Text pauseScoreText;
    public GameObject pausePanel;
    public Button pauseBtn;
    public GameObject settingPanel;
    public Button settingBtn;
    public Button resumeBtn;
    public Button soundBtn;
    public Button musicBtn;
    public GameObject soundOffImg;
    public GameObject musicOffImg;
    public Button settingPanelCloseBtn;
    public Button exitBtn;
    // Start is called before the first frame update
    void Start()
    {
        pausePanel.gameObject.SetActive(false);
        settingPanel.gameObject.SetActive(false);
        pauseBtn.onClick.AddListener(pauseBtnOnClick);
        resumeBtn.onClick.AddListener(resumeGame);
        settingBtn.onClick.AddListener(showSettings);
        settingPanelCloseBtn.onClick.AddListener(settingPanelClose);
        soundBtn.onClick.AddListener(soundBtnOnClick);
        musicBtn.onClick.AddListener(musicBtnOnClick);
        exitBtn.onClick.AddListener(endGame);
    }
    
    void pauseBtnOnClick()
    {
        //pause the time
        Time.timeScale = 0.0f;
        pausePanel.gameObject.SetActive(true);
    }

    void endGame()
    {
        PlayerPrefs.DeleteKey("level");
		PlayerPrefs.DeleteKey("baseScore");
		PlayerPrefs.DeleteKey("total");
        SceneManager.LoadScene("StartPage");
    }

    void resumeGame()
    {
        //time resumes
        Time.timeScale = 1.0f;
        pausePanel.gameObject.SetActive(false);
    }

    void showSettings()
    {
        settingPanel.gameObject.SetActive(true);
    }
    
    void settingPanelClose()
    {
        settingPanel.gameObject.SetActive(false);
    }

    void soundBtnOnClick()
    {
        if (soundOffImg.activeSelf == true)
        {
            soundOffImg.gameObject.SetActive(false);
        }
        else
        {
            soundOffImg.gameObject.SetActive(true);
        }
		if (PlayerPrefs.GetInt("sound") == 1)
            PlayerPrefs.SetInt("sound", 0);
        else
            PlayerPrefs.SetInt("sound", 1);
    }

    void musicBtnOnClick()
    {
        if (musicOffImg.activeSelf == true)
        {
            musicOffImg.gameObject.SetActive(false);
        }
        else
        {
            musicOffImg.gameObject.SetActive(true);
        }
		if (PlayerPrefs.GetInt("music") == 1)
		{
			PlayerPrefs.SetInt("music", 0);
		}
        else
		{
			PlayerPrefs.SetInt("music", 1);
		}
    }

    // Update is called once per frame
    void Update()
    {
        pauseScoreText.text = GameManager.gm.currentScore.ToString();
    }
}
