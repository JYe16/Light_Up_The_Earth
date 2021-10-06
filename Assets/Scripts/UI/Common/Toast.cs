using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Toast : MonoBehaviour
{
    private Text tipsText;
    private CanvasGroup canvasGroup;
    void Start()
    {
        tipsText = gameObject.GetComponentInChildren<Text>();
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
        DisplayToast(false);
    }

    private void DisplayToast(bool show)
    {
        canvasGroup.alpha = show ? 1 : 0;
        canvasGroup.interactable = show;
        canvasGroup.blocksRaycasts = show;
    }

    public void ShowToast(string tip)
    {
        tipsText.text = tip;
        DisplayToast(true);
    }
    
    public void HideToast()
    {
        tipsText.text = "";
        DisplayToast(false);
    }
}
