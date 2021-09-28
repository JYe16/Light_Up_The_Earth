using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ToScoreRank : MonoBehaviour
{
    // Start is called before the first frame update
    public Button scoreButton;
    void Start()
    {
        scoreButton.onClick.AddListener(scoreOnClick);
    }


    void scoreOnClick()
    {
        SceneManager.LoadScene("ScoreRank");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
