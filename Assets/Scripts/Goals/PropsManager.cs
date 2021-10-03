using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

public class PropsManager : MonoBehaviour
{
    public static PropsManager manager;
    [System.Serializable]
    public class PropsCounter
    {
        public int bombCounter = 0;
        public int powerWaterCounter = 0;
        public int timeIncreaseCounter = 0;
        public int scoreIncreaseCounter = 0;
    }
    public PropsCounter propsCounter;
    
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
    }

    void FixedUpdate()
    {
        bombBtText.text = propsCounter.bombCounter.ToString();
        timeExtensionBtText.text = propsCounter.timeIncreaseCounter.ToString();
        powerWaterBtText.text = propsCounter.powerWaterCounter.ToString();
        bonusBtText.text = propsCounter.scoreIncreaseCounter.ToString();
    }

    public void IncreaseScore(bool isAuto)
    {
        GameManager.gm.PlusScore(BOUNS_VALUE);
        if (!isAuto)
        {
            propsCounter.timeIncreaseCounter--;
        }
    }

    public void IncreaseTime(bool isAuto)
    {
        GameManager.gm.AddRemainingTime(BONUS_TIME);
        if (!isAuto)
        {
            propsCounter.timeIncreaseCounter--;
        }
    }

    public void DestoryGoal(GameObject goal)
    {
        goal.GetComponent<GoalMove>().ExplosionAndHide();
        propsCounter.bombCounter--;
    }

    public void FastMove(GameObject goal)
    {
        goal.GetComponent<GoalMove>().moveSpeed *= BONUS_SPEED;
        propsCounter.powerWaterCounter--;
    }

    public void UpdatePropsCounter(Gloable.PropsType type)
    {
        switch (type)
        {
            case Gloable.PropsType.BOMB:
                propsCounter.bombCounter++;
                break;
            case Gloable.PropsType.POWER_WATER:
                propsCounter.powerWaterCounter++;
                break;
            case Gloable.PropsType.TIME_INCREASE:
                propsCounter.timeIncreaseCounter++;
                break;
            case Gloable.PropsType.SCORE_INCREASE:
                propsCounter.scoreIncreaseCounter++;
                break;
        }
    }
}
