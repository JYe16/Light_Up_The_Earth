using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PausePanel : MonoBehaviour
{
    public Text pauseScoreText;
    public GameObject pausePanel;
    public GameObject settingPanel;
    public GameObject soundOffImg;
    public GameObject musicOffImg;

    private CanvasGroup pausePanelCanvasGroup;
    private CanvasGroup settingPanelCanvasGroup;
    private GameObject pausePanelBody;
    private GameObject settingPanelBody;
    private static float ANIMATION_DURATION = 0.2F;

    private void Awake()
    {
        pausePanelBody = pausePanel.GetComponentsInChildren<Transform>()[0].gameObject;
        settingPanelBody = settingPanel.GetComponentsInChildren<Transform>()[0].gameObject;
        
        settingPanelCanvasGroup = settingPanel.GetComponent<CanvasGroup>();
        pausePanelCanvasGroup = pausePanel.GetComponent<CanvasGroup>();
    }

    public void OpenPausePanel()
    {
        pauseScoreText.text = GameManager.gm.currentScore.ToString();
        GameManager.gm.PauseGame(true);
        DisplayPanel(pausePanelCanvasGroup, pausePanelBody, pausePanel);
    }

    public void OnClickExitBtn()
    {
        Utils.clearCache();
        GameObject playMusic = GameObject.Find("gamePlayMusic");
        Destroy(playMusic.gameObject);
        Tutorial.isPlaying = false;
        SceneManager.LoadScene("StartPage");
    }

    public void ClosePausePanel()
    {
        GameManager.gm.PauseGame(false);
        StartCoroutine(Fadeout(pausePanelCanvasGroup, pausePanelBody, pausePanel));
    }

    public void OpenSettingPanel()
    {
        pausePanel.SetActive(false);
        DisplayPanel(settingPanelCanvasGroup, settingPanelBody, settingPanel);
        if (PlayerPrefs.GetInt("sound") == 1)
            soundOffImg.gameObject.SetActive(false);
        else
            soundOffImg.gameObject.SetActive(true);

        if (PlayerPrefs.GetInt("music") == 1)
            musicOffImg.gameObject.SetActive(false);
        else
            musicOffImg.gameObject.SetActive(true);
    }

    public void CloseSettingPanel()
    {
        StartCoroutine(Fadeout(settingPanelCanvasGroup, settingPanelBody, settingPanel));
        pausePanel.SetActive(true);
    }

    public void OnClickMusicBtn()
    {
        if (musicOffImg.activeSelf)
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

    public void OnClickSoundBtn()
    {
        if (soundOffImg.activeSelf)
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

    // open panel
    private void DisplayPanel(CanvasGroup canvasGroup, GameObject panelBody, GameObject panel)
    {
        panel.SetActive(true);
        canvasGroup.DOFade(1, ANIMATION_DURATION);
        panelBody.transform.DOScale(1, ANIMATION_DURATION);
    }

    // close panel
    IEnumerator Fadeout(CanvasGroup canvasGroup, GameObject panelBody, GameObject panel)
    {
        canvasGroup.DOFade(0, ANIMATION_DURATION);
        panelBody.transform.DOScale( 0, ANIMATION_DURATION);
        yield return new WaitForSeconds(ANIMATION_DURATION);
        panel.SetActive(false);
    }
}