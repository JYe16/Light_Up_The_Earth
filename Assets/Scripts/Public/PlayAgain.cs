using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayAgain : MonoBehaviour
{
    // Start is called before the first frame update
    public Button PlayAgainBut;
    void Start()
    {
        PlayAgainBut.onClick.AddListener(PlayAgainOnClick);
    }

    void PlayAgainOnClick()
    {
		PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Level_00_Scene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
