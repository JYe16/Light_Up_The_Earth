using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayRank : MonoBehaviour
{
    //Score imported from Levels
    private int Score;
    //Text to show
    public Text rankText;
	string rank = "";
    // Start is called before the first frame update
    void Start()
    {
       	string content = Utils.ReadDataFromFile("Rank.json");
		Records recvJSON = JsonUtility.FromJson<Records>(content);
		for(int i = 0; i < recvJSON.list.Count; i++)
		{
			rank += recvJSON.list[i].name + "\t\t\t\t\t\t" + recvJSON.list[i].score + "\n";
		}
    }

    // Update is called once per frame
    void Update()
    {
        rankText.text = rank;
    }
}
