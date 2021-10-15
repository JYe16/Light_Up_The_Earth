using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartPage : MonoBehaviour
{
    public Button settingButton;
    public GameObject settingPanel;
    public Button infoButton;
    public GameObject infoPanel;
    public Button infoPanelCloseBtn;
    public Button settingPanelCloseBtn;
    public Button soundBtn;
    public Button musicBtn;
    public GameObject soundOffImg;
    public GameObject musicOffImg;
	public GameObject gameLaunchMusic;
	public Button startBtn;
	//for background music playing
	public static bool isPlaying = false;
    // Start is called before the first frame update
    void Start()
    {
        //add onClick functions to all buttons
        settingButton.onClick.AddListener(settingOnClick);
        infoButton.onClick.AddListener(infoOnClick);
		startBtn.onClick.AddListener(NewGameOnClick);
        //set info panel to invisible at start
        infoPanel.gameObject.SetActive(false);
        infoPanelCloseBtn.onClick.AddListener(infoPanelClose);
        //set setting panel to invisible at start
        settingPanel.gameObject.SetActive(false);
        settingPanelCloseBtn.onClick.AddListener(settingPanelClose);
        soundBtn.onClick.AddListener(soundBtnOnClick);
        musicBtn.onClick.AddListener(musicBtnOnClick);
		//set playerprefs for sound&music settings
        PlayerPrefs.SetInt("sound", 1);
        PlayerPrefs.SetInt("music", 1);
		if(!isPlaying)
		{
			DontDestroyOnLoad(gameLaunchMusic.gameObject);
			isPlaying = true;
		}
    }
    
    void settingOnClick()
    {
        settingPanel.gameObject.SetActive(true);
    }

    void infoOnClick()
    {
        infoPanel.gameObject.SetActive(true);
    }

    void infoPanelClose()
    {
        infoPanel.gameObject.SetActive(false);
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

        if (PlayerPrefs.GetInt("sound")==1)
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
        if (PlayerPrefs.GetInt("music")==1)
            PlayerPrefs.SetInt("music", 0);
        else
            PlayerPrefs.SetInt("music", 1);
    }

	public void NewGameOnClick()
    {
		PlayerPrefs.DeleteKey("level");
		PlayerPrefs.DeleteKey("baseScore");
		PlayerPrefs.DeleteKey("total");
		Destroy(gameLaunchMusic.gameObject);
        // SceneManager.LoadScene("Tutorial");
		SceneManager.LoadScene("Story_Plot");
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
