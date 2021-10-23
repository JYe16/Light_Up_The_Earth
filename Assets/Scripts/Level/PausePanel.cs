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
    public GameObject panelMask;
    public GameObject soundOffImg;
    public GameObject musicOffImg;

    private CanvasGroup pausePanelCanvasGroup;
    private CanvasGroup settingPanelCanvasGroup;

    private void Awake()
    {
        pausePanelCanvasGroup = pausePanel.GetComponent<CanvasGroup>();
        settingPanelCanvasGroup = settingPanel.GetComponent<CanvasGroup>();
    }

    public void OpenPausePanel()
    {
        pauseScoreText.text = GameManager.gm.currentScore.ToString();
        GameManager.gm.PauseGame(true);
        panelMask.SetActive(true);
        settingPanel.SetActive(false);
        DisplayPanel(pausePanel, pausePanelCanvasGroup);
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
        DOTween.Sequence()
            .Append(pausePanelCanvasGroup.DOFade(0, Gloable.POPUP_ANIMATION_DURATION))
            .Append(pausePanel.transform.DOScale(0, Gloable.POPUP_ANIMATION_DURATION))
            .OnComplete(() =>
            {
                panelMask.SetActive(false);
                GameManager.gm.PauseGame(false);
            })
            .SetRecyclable()
            .Play();
    }

    public void OpenSettingPanel()
    {
        pausePanel.SetActive(false);
        settingPanel.SetActive(true);
        DisplayPanel(settingPanel, settingPanelCanvasGroup);
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
        settingPanelCanvasGroup.alpha = 0;
        settingPanel.SetActive(false);
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
    private void DisplayPanel(GameObject panel, CanvasGroup canvasGroup)
    {
        panel.SetActive(true);
        canvasGroup.DOFade(1, Gloable.POPUP_ANIMATION_DURATION);
        panel.transform.DOScale(1, Gloable.POPUP_ANIMATION_DURATION);
    }
}