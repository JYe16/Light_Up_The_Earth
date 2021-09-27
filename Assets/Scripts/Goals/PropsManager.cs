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
        bombBtText.text = "BOMB " + propsCounter.bombCounter;
        timeExtensionBtText.text = "TIME EXTENSION " + propsCounter.timeIncreaseCounter;
        powerWaterBtText.text = "POWER WATER " + propsCounter.powerWaterCounter;
        bonusBtText.text = "BONUS " + propsCounter.bombCounter;
    }

    public void IncreaseScore()
    {
        GameManager.gm.AddScore(BOUNS_VALUE);
        propsCounter.bombCounter--;
    }

    public void IncreaseTime()
    {
        GameManager.gm.AddRemainingTime(BONUS_TIME);
        propsCounter.timeIncreaseCounter--;
    }

    public void DestoryGoal(GameObject goal)
    {
        goal.GetComponent<GoalMove>().HideGoal();
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
