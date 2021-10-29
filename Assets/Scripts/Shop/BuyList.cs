using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class BuyList : MonoBehaviour
{
    public Text[] texts;
    private GameObject listBody;
    private CanvasGroup canvasGroup;
    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        listBody = transform.GetChild(0).gameObject;
    }

    private void DisplayToast(bool show)
    {
        canvasGroup.interactable = show;
        canvasGroup.blocksRaycasts = show;
        canvasGroup.DOFade(show ? 1 : 0, Gloable.POPUP_ANIMATION_DURATION);
        listBody.transform.DOScale(show ? 1 : 0, Gloable.POPUP_ANIMATION_DURATION);
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
