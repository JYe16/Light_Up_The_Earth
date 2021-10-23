using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurchasePopup : MonoBehaviour
{
    public GameObject buyList;
    public GameObject toast;
    public Text totalText;
    [HideInInspector] public int totalMoney;
    [HideInInspector] public int score;
    
    private CanvasGroup canvasGroup;
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        DisplayPopup(false);
    }

    private void DisplayPopup(bool show)
    {
        canvasGroup.alpha = show ? 1 : 0;
        canvasGroup.interactable = show;
        canvasGroup.blocksRaycasts = show;
    }

    public void OpenPopup()
    {
        totalText.text = totalMoney.ToString();
        DisplayPopup(true);
    }
    
    public void ClosePopup()
    {
        DisplayPopup(false);
    }

    public void OnClickCheck()
    {
        if (totalMoney <= score)
        {
            int newBase = PlayerPrefs.GetInt("baseScore") - totalMoney;
            PlayerPrefs.SetInt("baseScore", newBase);
            ShopSystem.shopSystem.UpdateScore();
            ShopSystem.shopSystem.ClearAfterCheck();
            toast.GetComponent<Toast>().ShowToast("Thanks for your purchase!");
            DisplayPopup(false);
        }
    }

    public void OpenBuyList()
    {
        buyList.GetComponent<BuyList>().OpenList();
    }
}
