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
    //variables for fading function
    public float Duration = 0.4f;
    public int cur = 0;
    // Start is called before the first frame update
    void Start()
    {
        //add onClick functions to all buttons
		startBtn.onClick.AddListener(NewGameOnClick);
        //set info panel to invisible at start
        infoPanel.gameObject.SetActive(false);
        //set setting panel to invisible at start
        settingPanel.gameObject.SetActive(false);

        settingButton.onClick.AddListener(settingOnClick);
        settingPanelCloseBtn.onClick.AddListener(settingPanelClose);

        infoButton.onClick.AddListener(infoOnClick);
        infoPanelCloseBtn.onClick.AddListener(infoPanelClose);

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
        Fade(settingPanel, true);
    }

    void settingPanelClose()
    {
        StartCoroutine(SettingPanelAnimation());
        //Time.timeScale = 100f;
    }

    IEnumerator SettingPanelAnimation()
    {
        Fade(settingPanel, false);
        yield return new WaitForSeconds(0.5f);
        settingPanel.gameObject.SetActive(false);
    }

    void infoOnClick()
    {
        infoPanel.gameObject.SetActive(true);
        Fade(infoPanel, true);
    }

    void infoPanelClose()
    {
        StartCoroutine(InfoPanelAnimation());
    }

    IEnumerator InfoPanelAnimation()
    {
        Fade(infoPanel, false);
        yield return new WaitForSeconds(0.5f);
        infoPanel.gameObject.SetActive(false);
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
			gameLaunchMusic.gameObject.GetComponent<AudioSource>().Pause();
		}
        else
		{
			PlayerPrefs.SetInt("music", 1);
			gameLaunchMusic.gameObject.GetComponent<AudioSource>().UnPause();
		}
    }

	public void NewGameOnClick()
    {
        Utils.clearCache();
		Destroy(gameLaunchMusic.gameObject);
		SceneManager.LoadScene("Story_Plot");
    }
    
    public void Fade(GameObject Panel, bool isActive)
    {
        var canvGroup = Panel.GetComponent<CanvasGroup>();          
        StartCoroutine(DoFade(canvGroup, canvGroup.alpha, isActive ? 1 : 0));
    }

    public IEnumerator DoFade(CanvasGroup canvGroup, float start, float end)
    {
        float counter = 0f;
        while (counter < Duration)
        {
            counter += Time.deltaTime;
            canvGroup.alpha = Mathf.Lerp(start, end, counter / Duration);
            yield return null;
        }
    }
    
    // Update is called once per frame
    void Update()
    {

    }
}
