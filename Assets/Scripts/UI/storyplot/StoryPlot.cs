using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StoryPlot : MonoBehaviour
{
    
    public Button skipButton;
    public Button startButton;
    public GameObject storyPic1;
    public GameObject storyPic2;
    public GameObject storyPic3;

    
    // Start is called before the first frame update
    void Start()
    {
        storyPic2.gameObject.SetActive(false);
        storyPic3.gameObject.SetActive(false);
        startButton.gameObject.SetActive(false);
        skipButton.onClick.AddListener(startTutorial);
        startButton.onClick.AddListener(startTutorial);
        Invoke("showPic2", 5f);
        Invoke("showPic3", 10f);
    }
    
    void startTutorial()
    {
        GameObject launchMusic = GameObject.Find("gameLaunchMusic");
        Destroy(launchMusic.gameObject);
		StartPage.isPlaying = false;
        SceneManager.LoadScene("Tutorial");
    }

    void showPic2()
    {
        storyPic1.gameObject.SetActive(false);
        storyPic2.gameObject.SetActive(true);
    }
    
    void showPic3()
    {
        storyPic2.gameObject.SetActive(false);
        skipButton.gameObject.SetActive(false);
        storyPic3.gameObject.SetActive(true);
        startButton.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
