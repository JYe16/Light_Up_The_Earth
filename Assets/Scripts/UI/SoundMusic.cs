using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SoundMusic : MonoBehaviour
{
    public Button button;
    public GameObject img;

    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(buttonOnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void buttonOnClick()
    {
        if (img.activeSelf == true)
            img.gameObject.SetActive(false);
        else
            img.gameObject.SetActive(true);
    }
}
