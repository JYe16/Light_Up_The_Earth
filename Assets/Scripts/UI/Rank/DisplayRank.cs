using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayRank : MonoBehaviour
{
    //Score imported from Levels
    private int Score;
    //Text to show
    public Text rankNameText;
    public Text rankScoreText;
    public Text rankLevelText;
    
    // private string rank = "";
	string rankName = "";
    string rankScore = "";
    string rankLevel = "";

    // Start is called before the first frame update
    void Start()
    {
       	string content = Utils.ReadDataFromFile("Rank.json");
        if (content != "")
        {
            Records recvJSON = JsonUtility.FromJson<Records>(content);
            for(int i = 0; i < recvJSON.list.Count; i++)
            {
                // rank += recvJSON.list[i].name + "\t\t\t" + recvJSON.list[i].score + "\t\t\t" + recvJSON.list[i].level + "\n";
                // rankName = recvJSON.list[i].name + "\n";
                rankName = ((recvJSON.list[i].name).Length <= 8)
                    ? (recvJSON.list[i].name + "\n")
                    : ((recvJSON.list[i].name).Substring(0, 8) + "\n");
                rankScore = recvJSON.list[i].score + "\n";
                rankLevel = recvJSON.list[i].level + "\n";
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        rankNameText.text = rankName;
        rankScoreText.text = rankScore;
        rankLevelText.text = rankLevel;
    }
}
