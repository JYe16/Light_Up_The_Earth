using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class DisplayRank : MonoBehaviour
{
    //Score imported from Levels
    private int Score;
    //Text to show
    public Text rankText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Score = PlayerPrefs.GetInt("Score");
        rankText.text = "Score Rank\nScore: " + Score;
        //Debug.Log(Score);
    }
}

//class for storing the rank
public class Rank
{
    public int score;
    public string name;
}
