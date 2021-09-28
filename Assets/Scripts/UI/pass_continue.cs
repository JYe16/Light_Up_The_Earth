using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class pass_continue : MonoBehaviour
{
    // Start is called before the first frame update
    public Button passContinueButton;
    void Start()
    {
        passContinueButton.onClick.AddListener(passContinueOnClick);
    }

    void passContinueOnClick()
    {
        SceneManager.LoadScene("Level_00_Scene");
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
