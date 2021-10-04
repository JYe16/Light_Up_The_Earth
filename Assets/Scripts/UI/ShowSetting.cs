using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShowSetting : MonoBehaviour
{
    public Button settingButton;
    public GameObject settingPanel;
    // Start is called before the first frame update
    void Start()
    {
        settingButton.onClick.AddListener(settingOnClick);
    }
    
    void settingOnClick()
    {
        settingPanel.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
