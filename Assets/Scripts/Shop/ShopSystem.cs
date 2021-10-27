using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ShopSystem : MonoBehaviour
{
    public static ShopSystem shopSystem;

    public GameObject purchasePopup;
    public GameObject toast;
    public Text costMoney;
    public Text propsInfo;
    public Text propsName;
    public Text countText;
    public Text totalMoney;
    public Image[] dotsList;

    public Dictionary<Gloable.PropsType, PropInfo> propsInfoMap;
    private static float DOT_SCALE = 1.25f;
    private int score;
    private int cost = 0;
    public class PropInfo
    {
        public string name;
        public string des;
        public int money;
        public int buyStep;
    }

    private int curIndex;
    void Start()
    {
        if (shopSystem == null)
        {
            shopSystem = GetComponent<ShopSystem>();
        }
        Init();
    }

    private void Update()
    {
        costMoney.text = "cost :  " + cost;
    }

    private void Init()
    {
        curIndex = 0;
        propsInfoMap = new Dictionary<Gloable.PropsType, PropInfo>();
        propsInfoMap.Add(Gloable.PropsType.BOMB, new PropInfo()
        {
            name = "BOMB",
            des = "No matter what kind of goal it is, bomb can destroy it immediately during the capture processing after you use it.",
            money = 20,
            buyStep = 0
        });
        propsInfoMap.Add(Gloable.PropsType.TIME_INCREASE, new PropInfo()
        {
            name = "EXTENSION",
            des = "Extend 10 seconds to the remaining time immediately after you use it.",
            money = 30,
            buyStep = 0
        });
        propsInfoMap.Add(Gloable.PropsType.SCORE_INCREASE, new PropInfo()
        {
            name = "BONUS",
            des ="Add  five bonus to your current score at once after use.",
            money = 10,
            buyStep = 0
        });
        propsInfoMap.Add(Gloable.PropsType.POWER_WATER, new PropInfo()
        {
            name = "SPEED",
            des = "Speed up the movement by two times when you are dragging any kind of goal back. Only effective to current capture.",
            money = 15,
            buyStep = 0
        });
        costMoney.text = "cost :  " + cost;
        UpdateScore();
    }

    private void SwitchDetail(int dir, bool isJumpTo)
    {
        int oldIndex = curIndex;
        curIndex = isJumpTo ? dir : (curIndex + dir) % propsInfoMap.Count;
        PropInfo curPropInfo = propsInfoMap.ElementAt(curIndex).Value;
        
        propsName.text = curPropInfo.name;
        propsInfo.text = curPropInfo.des;
        countText.text = curPropInfo.buyStep.ToString();
        ScrollSelectedDot(oldIndex, curIndex);
    }

    private void ScrollSelectedDot(int oldIndex, int newIndex)
    {
        (dotsList[oldIndex].sprite, dotsList[newIndex].sprite) = (dotsList[newIndex].sprite, dotsList[oldIndex].sprite);
        dotsList[oldIndex].transform.localScale = Vector3.one;
        dotsList[newIndex].transform.localScale = Vector3.one * DOT_SCALE;
    }

    public void UpdateScore()
    {
        score = PlayerPrefs.HasKey("baseScore") ? PlayerPrefs.GetInt("baseScore") : 0;
        totalMoney.text = score.ToString();
    }

    public void ClearAfterCheck()
    {
        foreach (var prop in propsInfoMap)
        {
            string key = prop.Key.ToString();
            int curCount = PlayerPrefs.GetInt(key) + prop.Value.buyStep;
            PlayerPrefs.SetInt(key, curCount);
            prop.Value.buyStep = 0;
        }
        countText.text = "0";
        cost = 0;
    }

    public void OnClickOverviewItem(int targetIndex)
    {
        SwitchDetail(targetIndex, true);
    }

    public void OnClickPreOrNext(int dir)
    {
        SwitchDetail(dir, false);
    }
    
    public void OnClickPlus()
    {
        if (propsInfoMap.ElementAt(curIndex).Value.buyStep == 99) return;
        countText.text = (++propsInfoMap.ElementAt(curIndex).Value.buyStep).ToString();
        cost += propsInfoMap.ElementAt(curIndex).Value.money;
    }
    
    public void OnClickMinus()
    {
        if (propsInfoMap.ElementAt(curIndex).Value.buyStep == 0) return;
        countText.text = (--propsInfoMap.ElementAt(curIndex).Value.buyStep).ToString();
        cost -= propsInfoMap.ElementAt(curIndex).Value.money;
    }

    public void OnClickCart()
    {
        if (cost > score)
        {
            toast.GetComponent<Toast>().ShowToast("oops!\r\nYou do not have enough score to buy them!");
        }
        else if (cost == 0)
        {
            toast.GetComponent<Toast>().ShowToast("You pick out nothing!");
        }
        else
        {
            PurchasePopup popup = purchasePopup.GetComponent<PurchasePopup>();
            popup.score = score;
            popup.totalMoney = cost;
            popup.OpenPopup();
        }
    }

    public void OnClickExit()
    {
        SceneManager.LoadScene("Pass_Level");
    }

}
