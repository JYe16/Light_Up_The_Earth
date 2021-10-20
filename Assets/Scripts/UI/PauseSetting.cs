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
    public float Duration = 0.4f;
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
        //StartCoroutine(PausePanelFadeIn());  
        pausePanel.gameObject.SetActive(true);
        Time.timeScale = 0.1f;//pause the time
    }

    IEnumerator PausePanelFadeIn()
    {
        pausePanel.gameObject.SetActive(true);
        Fade(pausePanel, true);
        yield return new WaitForSeconds(0.5f);
        Time.timeScale = 0.0f;//pause the time
    }




    void endGame()
    {
        Utils.clearCache();
        GameObject playMusic = GameObject.Find("gamePlayMusic");
        Destroy(playMusic.gameObject);
        Tutorial.isPlaying = false;
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
        if (PlayerPrefs.GetInt("sound") == 1)
            soundOffImg.gameObject.SetActive(false);
        else
            soundOffImg.gameObject.SetActive(true);

        if (PlayerPrefs.GetInt("music") == 1)
            musicOffImg.gameObject.SetActive(false);
        else
            musicOffImg.gameObject.SetActive(true);
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
        {
            PlayerPrefs.SetInt("sound", 0);
        }
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
        GameObject playMusic = GameObject.Find("gamePlayMusic");
        if (PlayerPrefs.GetInt("music") == 1)
        {
            PlayerPrefs.SetInt("music", 0);
            playMusic.gameObject.GetComponent<AudioSource>().Pause();
        }
        else
        {
            PlayerPrefs.SetInt("music", 1);
            playMusic.gameObject.GetComponent<AudioSource>().Play();
        }
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
        pauseScoreText.text = GameManager.gm.currentScore.ToString();
    }
}
