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
    // Start is called before the first frame update
    void Start()
    {
        settingButton.onClick.AddListener(settingOnClick);
        infoButton.onClick.AddListener(infoOnClick);
        infoPanel.gameObject.SetActive(false);
        infoPanelCloseBtn.onClick.AddListener(infoPanelClose);
        settingPanel.gameObject.SetActive(false);
        settingPanelCloseBtn.onClick.AddListener(settingPanelClose);
        soundBtn.onClick.AddListener(soundBtnOnClick);
        musicBtn.onClick.AddListener(musicBtnOnClick);
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
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
