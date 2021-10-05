using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BuyList : MonoBehaviour
{
    public Text[] texts;
    
    private CanvasGroup canvasGroup;
    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void DisplayToast(bool show)
    {
        canvasGroup.alpha = show ? 1 : 0;
        canvasGroup.interactable = show;
        canvasGroup.blocksRaycasts = show;
    }

    public void OpenList()
    {
        var propsInfoMap = ShopSystem.shopSystem.propsInfoMap;
        for (int i = 0; i < propsInfoMap.Count; i++)
        {
            var propInfo = propsInfoMap.ElementAt(i).Value;
            texts[i].text = "x " + propInfo.buyStep + " = " + (propInfo.buyStep * propInfo.money);
        }
        DisplayToast(true);
    }

    public void CloseList()
    {
        DisplayToast(false);
    }
}
