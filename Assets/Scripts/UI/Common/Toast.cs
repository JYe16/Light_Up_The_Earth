using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Toast : MonoBehaviour
{
    private Text tipsText;
    private GameObject context;
    private CanvasGroup canvasGroup;
    void Start()
    {
        context = gameObject.transform.GetChild(0).gameObject;
        tipsText = gameObject.GetComponentInChildren<Text>();
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
        DisplayToast(false);
    }

    private void DisplayToast(bool show)
    {
        canvasGroup.interactable = show;
        canvasGroup.blocksRaycasts = show;
        canvasGroup.DOFade(show ? 1 : 0, Gloable.POPUP_ANIMATION_DURATION);
        context.transform.DOScale(show ? 1 : 0, Gloable.POPUP_ANIMATION_DURATION);
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
