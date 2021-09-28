using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NewGame : MonoBehaviour
{
    // Start is called before the first frame update
    public Button newgameButton;
  

    void Start()
    {
        newgameButton.onClick.AddListener(newGameOnClick);
        
    }

    void newGameOnClick()
    {
        SceneManager.LoadScene("Level_00_Scene");
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
