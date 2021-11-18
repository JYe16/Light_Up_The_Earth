using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartPage : MonoBehaviour
{
	public GameObject infoToast;
	public Button infoBtn;

	public GameObject settingPanel;
    public Button settingButton;
    public Button settingPanelCloseBtn;
    public Button soundBtn;
    public Button musicBtn;
    public GameObject soundOffImg;
    public GameObject musicOffImg;

	public GameObject fadePanel;
    public GameObject loadPanel;
    
	public GameObject gameLaunchMusic;
	public Button startBtn;
	//for background music playing
	public static bool isPlaying = false;
	
	private CanvasGroup settingPanelCanvasGroup;
	private GameObject settingPanelBody;
    void Start()
    {
		loadPanel.SetActive(true);
	    //add onClick functions to all buttons
        AddEventListeners();
        //set playerprefs for sound&music settings
		SetMusicAndSound();
		settingPanelBody = settingPanel.transform.GetChild(0).gameObject;
		settingPanelCanvasGroup = settingPanelBody.GetComponent<CanvasGroup>();
    }

    private void SetMusicAndSound()
    {
	    //if no playerprefs found
	    if(!PlayerPrefs.HasKey("sound") && !PlayerPrefs.HasKey("music"))
	    {
		    //default: on
		    PlayerPrefs.SetInt("sound", 1);
		    PlayerPrefs.SetInt("music", 1);
	    }
	    else
	    {
		    //show the corresponding icon for music and sound
		    if(PlayerPrefs.GetInt("music") == 0)
			    musicOffImg.gameObject.SetActive(true);
		    if(PlayerPrefs.GetInt("sound") == 0)
			    soundOffImg.gameObject.SetActive(true);
	    }
	    //Debug.Log(PlayerPrefs.GetInt("music"));
	    if(!isPlaying)
	    {
		    DontDestroyOnLoad(gameLaunchMusic.gameObject);
		    if(PlayerPrefs.GetInt("music") == 1)
		    {
			    gameLaunchMusic.gameObject.GetComponent<AudioSource>().Play();
		    }
		    isPlaying = true;
	    }
    }

    private void AddEventListeners()
    {
	    startBtn.onClick.AddListener(NewGameOnClick);

	    settingButton.onClick.AddListener(settingOnClick);
	    settingPanelCloseBtn.onClick.AddListener(settingPanelClose);

	    soundBtn.onClick.AddListener(soundBtnOnClick);
	    musicBtn.onClick.AddListener(musicBtnOnClick);
	    
	    infoBtn.onClick.AddListener(OpenInfoToast);
    }
    
    void settingOnClick()
    {
	    settingPanel.SetActive(true);
	    DisplayPanel(true);
    }

    void settingPanelClose()
    {
        DisplayPanel(false);
        settingPanel.SetActive(false);
    }
    
    private void DisplayPanel(bool show)
    {
	    settingPanelCanvasGroup.DOFade(show ? 1 : 0, Gloable.POPUP_ANIMATION_DURATION);
	    settingPanelBody.transform.DOScale(show ? 1 : 0, Gloable.POPUP_ANIMATION_DURATION);
	    settingPanelCanvasGroup.interactable = show;
	    settingPanelCanvasGroup.blocksRaycasts = show;
    }

    void OpenInfoToast()
    {
	    string context = "NINE A.M.\r\nShenqi Ye\tTianshu Wang\r\nZhankai Ye\tYingqi Liang\r\nXiaoxuan Sun\nSpecial Thanks: Rui-Han Xia";
	    infoToast.GetComponent<Toast>().ShowToast(context);
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
		GameObject launchMusic = GameObject.Find("gameLaunchMusic");
        if (PlayerPrefs.GetInt("music") == 1)
		{
			PlayerPrefs.SetInt("music", 0);
			launchMusic.gameObject.GetComponent<AudioSource>().Pause();
		}
        else
		{
			PlayerPrefs.SetInt("music", 1);
			launchMusic.gameObject.GetComponent<AudioSource>().Play();
		}
    }

	public void NewGameOnClick()
    {
        Utils.clearCache();
		fadePanel.SetActive(true);
        SceneManager.LoadScene("Story_Plot");
    }
}
