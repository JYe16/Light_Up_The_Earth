using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Return_MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public Button button;
    void Start()
    {
        button.onClick.AddListener(mainMenuOnClick);
    }

    void mainMenuOnClick()
    {
        SceneManager.LoadScene("StartPage");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
