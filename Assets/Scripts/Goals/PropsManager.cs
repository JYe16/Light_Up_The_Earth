using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

public class PropsManager : MonoBehaviour
{
    public static PropsManager manager;
    public Dictionary<Gloable.PropsType, int> propsCounter;
    public Text bombBtText;
    public Text timeExtensionBtText;
    public Text powerWaterBtText;
    public Text bonusBtText;
    
    private Gloable.PropsType curActiveProp;
    
    private static int BOUNS_VALUE = 10;
    private static int BONUS_TIME = 5;
    private static int BONUS_SPEED = 2;

    void Start()
    {
        if(manager == null)  manager = GetComponent<PropsManager>();
        Init();
    }
    
    private void Init()
    {
        propsCounter = new Dictionary<Gloable.PropsType, int>();
        propsCounter.Add(Gloable.PropsType.BOMB, InitCountByType(Gloable.PropsType.BOMB));
        propsCounter.Add(Gloable.PropsType.TIME_INCREASE, InitCountByType(Gloable.PropsType.TIME_INCREASE));
        propsCounter.Add(Gloable.PropsType.SCORE_INCREASE, InitCountByType(Gloable.PropsType.SCORE_INCREASE));
        propsCounter.Add(Gloable.PropsType.POWER_WATER, InitCountByType(Gloable.PropsType.POWER_WATER));
    }

    private int InitCountByType(Gloable.PropsType type)
    {
        string key = type.ToString();
        int res = 1;
        if (PlayerPrefs.HasKey(key))
        {
            res += PlayerPrefs.GetInt(key);
        }
        else
        {
            PlayerPrefs.SetInt(key, 0);
        }
        return res;
    }
    void FixedUpdate()
    {
        bombBtText.text = propsCounter[Gloable.PropsType.BOMB].ToString();
        timeExtensionBtText.text = propsCounter[Gloable.PropsType.TIME_INCREASE].ToString();
        powerWaterBtText.text = propsCounter[Gloable.PropsType.POWER_WATER].ToString();
        bonusBtText.text = propsCounter[Gloable.PropsType.SCORE_INCREASE].ToString();
    }

    public void IncreaseScore(bool isAuto)
    {
        GameManager.gm.AddScore(BOUNS_VALUE);
        if (!isAuto)
        {
            propsCounter[Gloable.PropsType.SCORE_INCREASE]--;
        }
    }

    public void IncreaseTime(bool isAuto)
    {
        GameManager.gm.AddRemainingTime(BONUS_TIME);
        if (!isAuto)
        {
            propsCounter[Gloable.PropsType.TIME_INCREASE]--;
        }
    }

    public void DestoryGoal(GameObject goal)
    {
        goal.GetComponent<GoalMove>().ExplosionAndHide();
        propsCounter[Gloable.PropsType.BOMB]--;
    }

    public void FastMove(GameObject goal)
    {
        goal.GetComponent<GoalMove>().moveSpeed *= BONUS_SPEED;
        propsCounter[Gloable.PropsType.POWER_WATER]--;
    }

    public void UpdatePropsCounter(Gloable.PropsType type)
    {
        propsCounter[type]++;
    }
}
