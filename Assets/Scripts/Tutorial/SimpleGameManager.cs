using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SimpleGameManager : MonoBehaviour
{
    static public SimpleGameManager gm;
    public TutorialManager tm;
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
    
    public GameObject explosion;
    public Slider timeBar;
    public float timeRemaining;
    public Text scoreText;
    [HideInInspector]public int currentScore;
    [HideInInspector]public GameObject currentGoal; 
    public bool pauseGame;
    private int targetScore;
    private GameObject player;
    private int preScore;
    private bool showPropsGuide = true;
    private bool showGoalGuide = true;
    
    private static int BOUNS_VALUE = 10;
    private static int BONUS_TIME = 5;
    private static int BONUS_SPEED = 2;

    void Start()
    {
        if(gm == null) 
            gm = GetComponent<SimpleGameManager>();
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");
        pauseGame = true;
        targetScore = 30;
        InitUI();
    }

    public void PauseGame(bool isPause)
    {
        pauseGame = isPause;
    }
    
    private void InitUI()
    {
        currentScore = 0;   
        scoreText.text = currentScore + "/" + targetScore;
        timeBar.maxValue = timeRemaining;
        timeBar.value = timeRemaining;
        
        bombBtn.onClick.AddListener(OnClickBomb);
        powerWaterBtn.onClick.AddListener(OnClickPowerWater);
        bonusBtn.onClick.AddListener(AddScore);
        timeExtensionBtn.onClick.AddListener(AddRemainingTime);
    }
    
    void FixedUpdate()
    {
        UpdateBtnUI();
    }

    private void UpdateBtnUI()
    {
        timeExtensionBtn.GetComponent<Image>().sprite = enableBtnLists.TimeExtensionBtn;
        bombBtn.GetComponent<Image>().sprite = currentGoal ?  enableBtnLists.BombBtn : unableBtnLists.BombBtn;
        bonusBtn.GetComponent<Image>().sprite = enableBtnLists.BonusBtn;
        powerWaterBtn.GetComponent<Image>().sprite = currentGoal ? enableBtnLists.PowerWaterBtn : unableBtnLists.PowerWaterBtn;
    }

    void Update()
    {
        if (!pauseGame)
        {
            //update score text
            scoreText.text = currentScore + "/" + targetScore;
            //update time remaining
            if(timeRemaining > 0){
                timeRemaining -= Time.deltaTime;
                timeBar.value = timeRemaining;
            }
            if (currentScore > 0 && showPropsGuide)
            {
                preScore = currentScore;
                showPropsGuide = false;
                pauseGame = true;
                tm.DisplayPropsGuide();
            }

            if (currentScore > preScore && showGoalGuide)
            {
                showGoalGuide = false;
                pauseGame = true;
                tm.DisplayGoalGuide();
            }
        }
    }

    public void AddScore()
    {
        //play sound effect
        if (addScoreAudio != null && PlayerPrefs.GetInt("sound") == 1)
        {
            AudioSource.PlayClipAtPoint(addScoreAudio, Camera.main.transform.position);
        }
        currentScore += BOUNS_VALUE;
    }

    public void AddRemainingTime()
    {
        GameObject handle = GameObject.FindGameObjectWithTag("TimeHandle");
        //play sound effect
        if (extendTimeAudio != null && PlayerPrefs.GetInt("sound") == 1)
        {
            AudioSource.PlayClipAtPoint(extendTimeAudio, Camera.main.transform.position);
        }
        if (handle != null)
        {
            Vector3 maxScale = handle.transform.localScale * 1.5f;
            handle.transform.DOScale(maxScale, 0.5f);
            handle.transform.DOScale(Vector3.one, 0.5f);
        }
        timeRemaining += BONUS_TIME;
    }
    
    private void OnClickBomb()
    {
        bool canClick = currentGoal != null;
        if (canClick)
        {
            GoalMove goalMove = currentGoal.GetComponent<GoalMove>();
            goalMove.explosion = explosion;
            goalMove.destroyGoalAudio = destroyGoalAudio;
            goalMove.ExplosionAndHide();
        }
    }
    
    private void OnClickPowerWater()
    {
        bool canClick = currentGoal != null;
        if (canClick)
        {
            currentGoal.GetComponent<GoalMove>().moveSpeed *= BONUS_SPEED;
            //play sound effect
            if (powerWaterAudio != null && PlayerPrefs.GetInt("sound") == 1)
            {
                AudioSource.PlayClipAtPoint(powerWaterAudio, Camera.main.transform.position);
            }
        }
    }
}
