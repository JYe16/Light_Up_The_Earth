using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class YouWIn : MonoBehaviour
{
    // Start is called before the first frame update
    public Button ContinueBut;
    void Start()
    {
        ContinueBut.onClick.AddListener(ContinueOnClick);
    }

    void ContinueOnClick()
    {
        SceneManager.LoadScene("Level_00_Scene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
