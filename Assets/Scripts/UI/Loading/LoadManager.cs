using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadManager : MonoBehaviour
{
    public Slider slider;
    public Text text;
    public Text headingText;
    public Image progressBar;
    
    private int curProgressValue = 0;
    private AsyncOperation operation;
    
    
    // Start is called before the first frame update
    void Start()
    {
        slider.gameObject.SetActive(true);
        text.gameObject.SetActive(true);
        headingText.gameObject.SetActive(true);
        // StartCoroutine(Loadlevel());
        Invoke("LoadlevelStart", 0.5f);
    }

    public void LoadlevelStart()
    {
        StartCoroutine(Loadlevel());
    }

    IEnumerator Loadlevel()
    {
        operation = SceneManager.LoadSceneAsync("Level_00_Scene");
        
        // refuse loading game automatically
        operation.allowSceneActivation = false;

        // while (!operation.isDone)
        // {
        //     slider.value = operation.progress;
        //
        //     text.text = operation.progress * 100 + " %";
        //
        //     if (operation.progress >= 0.9f)
        //     {
        //         slider.value = 1;
        //
        //         text.text = "Tap anywhere to continue";
        //
        //         if (Input.GetMouseButton(0))
        //         {
        //             operation.allowSceneActivation = true;
        //         }
        //     }
        //
        //     yield return null;
        // }

        yield return operation;
    }

    // Update is called once per frame
    void Update()
    {
        // slider.value = operation.progress;
        //
        // text.text = operation.progress * 100 + " %";
        //
        // progressBar.fillAmount = (operation.progress * 100) / 100f;
        
        int progressValue = 100;

        if (curProgressValue < progressValue)
        {
            curProgressValue++;
        }
        
        text.text = curProgressValue + " %";
        
        slider.value = curProgressValue / 100f;
        
        // progressBar.fillAmount = curProgressValue / 100f;
        
        if (curProgressValue == 100)
        {

            if (operation.progress >= 0.9f)
            {
                slider.value = 1;
        
                text.text = "Tap anywhere to continue";

                headingText.text = "It's ready!";
        
                if (Input.GetMouseButton(0))
                {
                    operation.allowSceneActivation = true;
                }
            }
            
        }
    }
}
