using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class GenerateRank : MonoBehaviour
{
    public Button SubmitBut;
    public InputField nameInputField;
    // Start is called before the first frame update
    void Start()
    {
        SubmitBut.onClick.AddListener(generateJSON);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void generateJSON()
    {
		string json = "";
        Record record = new Record();
        record.score = PlayerPrefs.GetInt("total");
        //If the player did not enter his/her name
        if (nameInputField.text == "")
        {
            //output John Doe
            record.name = "John Doe";
        }
        else
        {
            //Output the name
			record.name = nameInputField.text;
        }
		string content = Utils.ReadDataFromFile("Rank.json");
		//if file does not exist
        if (content == "")
        {
			Records recordList = new Records();
			recordList.list.Add(record);
         	json = JsonUtility.ToJson(recordList);
			Utils.WriteJSON("Rank.json", json);
        }
        else
        {
			Records recvJSON = JsonUtility.FromJson<Records>(content);
			//add the current record to the list
			recvJSON.list.Add(record);
			//sort the list
			recvJSON.list.Sort((x, y) => y.score.CompareTo(x.score));
			//if the records have reached the limit
			if(recvJSON.list.Count > 5)
			{
			    //remove the last element in the list
				recvJSON.list.RemoveAt(recvJSON.list.Count - 1);
			}
			json = JsonUtility.ToJson(recvJSON);
			Utils.WriteJSON("Rank.json", json);
        }
		//delete the temp variables stored
		PlayerPrefs.DeleteAll();
		SceneManager.LoadScene("ScoreRank");
    }
}

//new class for score record(s)
[System.Serializable]
public class Record
{
    public int score;
    public string name;
}

[System.Serializable]
public class Records
{
	public List<Record> list = new List<Record>();
}
