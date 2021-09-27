using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class Rank_Score : MonoBehaviour
{
    //Score imported from Levels
    private int Score;
    //Text to show
    public Text rankText;
    // Start is called before the first frame update
    void Start()
    {
        if(File.Exists("Configuration/Level_1/RankData.json"))
        {
            
        }
        else
        {
            
        }

        }

    // Update is called once per frame
    void Update()
    {
        Score = PlayerPrefs.GetInt("Score");
        rankText.text = "You Lost!!!\nScore: " + Score;
        //Debug.Log(Score);
    }
}
