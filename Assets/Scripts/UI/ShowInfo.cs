using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShowInfo : MonoBehaviour
{
    public Button infoButton;
    public GameObject infoPanel;
    // Start is called before the first frame update
    void Start()
    {
        infoButton.onClick.AddListener(infoOnClick);
    }
    
    void infoOnClick()
    {
        infoPanel.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
