using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameover_Continue : MonoBehaviour
{
    // Start is called before the first frame update
    public Button button;
    void Start()
    {
       button.onClick.AddListener(gameOverOnClick);
    }

    void gameOverOnClick()
    {
        SceneManager.LoadScene("EnterName");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
