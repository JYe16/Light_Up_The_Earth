using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;

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

    void sort()
    {
        
    }

    void generateJSON()
    {
        if (File.Exists("Rank.json"))
        {
            string content = Utils.ReadDataFromFile("Configuration/Rank.json");
        }
        else
        {
            
        }
    }
}
