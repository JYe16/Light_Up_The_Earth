using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PropsManager : MonoBehaviour
{
    public static PropsManager manager;
    public Dictionary<Gloable.PropsType, int> propsCounter;
    public GameObject explosion;
    public Button bombBtn;
    public Button timeExtensionBtn;
    public Button powerWaterBtn;
    public Button bonusBtn;

    [System.Serializable]
    public class BtnSprites
    {
        public Sprite TimeExtensionBtn;
        public Sprite BombBtn;
        public Sprite BonusBtn;
        public Sprite PowerWaterBtn;
    }
    public BtnSprites unableBtnLists;
    public BtnSprites enableBtnLists;
    
    public AudioClip destroyGoalAudio;
    public AudioClip extendTimeAudio;
    public AudioClip addScoreAudio;
    public AudioClip powerWaterAudio;

    private Gloable.PropsType curActiveProp;
    
    private Text bombBtText;
    private Text timeExtensionBtText;
    private Text powerWaterBtText;
    private Text bonusBtText;
    
    private int BOUNS_VALUE = 10;
    private static int BONUS_TIME = 5;
    private static int BONUS_SPEED = 2;
    
    public void OnClikeTimeExtension()
    {
        if (propsCounter[Gloable.PropsType.TIME_INCREASE] > 0)
        {
            IncreaseTime(false);
        }
    }

    public void OnClickBomb()
    {
        int count = propsCounter[Gloable.PropsType.BOMB];
        bool canClick = count > 0 && GameManager.gm.currentGoal;
        if (canClick)
        {
            DestroyGoal(GameManager.gm.currentGoal);
        }
    }

    public void OnClickBonus()
    {
        if (propsCounter[Gloable.PropsType.SCORE_INCREASE] > 0)
        {
            IncreaseScore(false);
        }
    }

    public void OnClickPowerWater()
    {
        int count = propsCounter[Gloable.PropsType.POWER_WATER];
        bool canClick = count > 0 && GameManager.gm.currentGoal;
        if (canClick)
        {
            FastMove(GameManager.gm.currentGoal);
        }
    }

    void Start()
    {
        if(manager == null)  manager = GetComponent<PropsManager>();
        Init();
    }

    private void OnDestroy()
    {
        UpdatePlayerPrefs();
    }

    private void Init()
    {
        BOUNS_VALUE = Random.Range(10, 50);
        
        propsCounter = new Dictionary<Gloable.PropsType, int>();
        
        bombBtText = bombBtn.GetComponentInChildren<Text>();
        timeExtensionBtText = timeExtensionBtn.GetComponentInChildren<Text>();
        powerWaterBtText = powerWaterBtn.GetComponentInChildren<Text>();
        bonusBtText = bonusBtn.GetComponentInChildren<Text>();
    
        propsCounter.Add(Gloable.PropsType.BOMB, InitCountByType(Gloable.PropsType.BOMB));
        propsCounter.Add(Gloable.PropsType.TIME_INCREASE, InitCountByType(Gloable.PropsType.TIME_INCREASE));
        propsCounter.Add(Gloable.PropsType.SCORE_INCREASE, InitCountByType(Gloable.PropsType.SCORE_INCREASE));
        propsCounter.Add(Gloable.PropsType.POWER_WATER, InitCountByType(Gloable.PropsType.POWER_WATER));
    }

    private int InitCountByType(Gloable.PropsType type)
    {
        string key = type.ToString();
        int res = PlayerPrefs.HasKey(key) ? PlayerPrefs.GetInt(key) : 1;
        if (!PlayerPrefs.HasKey(key))
        {
            PlayerPrefs.SetInt(key, 1);
        }
        return res;
    }

    private void UpdatePlayerPrefs()
    {
        foreach (var prop in propsCounter)
        {
            PlayerPrefs.SetInt(prop.Key.ToString(), prop.Value);
        }
    }
    
    void FixedUpdate()
    {
        UpdateBtnUI();
    }

    private void UpdateBtnUI()
    {
        bombBtText.text = propsCounter[Gloable.PropsType.BOMB].ToString();
        timeExtensionBtText.text = propsCounter[Gloable.PropsType.TIME_INCREASE].ToString();
        powerWaterBtText.text = propsCounter[Gloable.PropsType.POWER_WATER].ToString();
        bonusBtText.text = propsCounter[Gloable.PropsType.SCORE_INCREASE].ToString();
        
        timeExtensionBtn.GetComponent<Image>().sprite = 
            (propsCounter[Gloable.PropsType.TIME_INCREASE] <= 0) ? unableBtnLists.TimeExtensionBtn : enableBtnLists.TimeExtensionBtn;
        bombBtn.GetComponent<Image>().sprite =
            (propsCounter[Gloable.PropsType.BOMB] <= 0 || !GameManager.gm.currentGoal) ? unableBtnLists.BombBtn : enableBtnLists.BombBtn;
        bonusBtn.GetComponent<Image>().sprite = 
            (propsCounter[Gloable.PropsType.SCORE_INCREASE] <= 0) ? unableBtnLists.BonusBtn : enableBtnLists.BonusBtn;
        powerWaterBtn.GetComponent<Image>().sprite = 
            (propsCounter[Gloable.PropsType.POWER_WATER] <= 0 || !GameManager.gm.currentGoal) ? unableBtnLists.PowerWaterBtn : enableBtnLists.PowerWaterBtn;
    }

    public void IncreaseScore(bool isAuto)
    {
        GameManager.gm.AddScore(BOUNS_VALUE);
        if (!isAuto)
        {
            propsCounter[Gloable.PropsType.SCORE_INCREASE]--;
        }
        //play sound effect
        if (addScoreAudio != null && PlayerPrefs.GetInt("sound") == 1)
        {
            AudioSource.PlayClipAtPoint(addScoreAudio, Camera.main.transform.position);
        }
    }

    public void IncreaseTime(bool isAuto)
    {
        GameManager.gm.AddRemainingTime(BONUS_TIME);
        if (!isAuto)
        {
            propsCounter[Gloable.PropsType.TIME_INCREASE]--;
        }
        //play sound effect
        if (extendTimeAudio != null && PlayerPrefs.GetInt("sound") == 1)
        {
            AudioSource.PlayClipAtPoint(extendTimeAudio, Camera.main.transform.position);
        }
    }

    private void DestroyGoal(GameObject goal)
    {
        GoalMove goalMove = goal.GetComponent<GoalMove>();
        goalMove.explosion = explosion;
        goalMove.destroyGoalAudio = destroyGoalAudio;
        goalMove.ExplosionAndHide();
        propsCounter[Gloable.PropsType.BOMB]--;
    }

    private void FastMove(GameObject goal)
    {
        GoalMove _goalMove = goal.GetComponent<GoalMove>();
        _goalMove.SpeedUp(_goalMove.curSpeed * BONUS_SPEED);
        propsCounter[Gloable.PropsType.POWER_WATER]--;
        //play sound effect
        if (powerWaterAudio != null && PlayerPrefs.GetInt("sound") == 1)
        {
            AudioSource.PlayClipAtPoint(powerWaterAudio, Camera.main.transform.position);
        }
    }

    public void UpdatePropsCounter(Gloable.PropsType type)
    {
        propsCounter[type]++;
    }
}
