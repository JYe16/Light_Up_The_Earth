using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PropsButtons : MonoBehaviour
{
    [System.Serializable]
    public class UnableBtnSprites
    {
        public Sprite TimeExtensionBtn;
        public Sprite BombBtn;
        public Sprite BonusBtn;
        public Sprite PowerWaterBtn;
    }
    
    //audio files for extend time, add score and power water
    public AudioClip extendTimeAudio;
    public AudioClip addScoreAudio;
    public AudioClip powerWaterAudio;

    public UnableBtnSprites unableBtnLists;
    public void OnClikeTimeExtension(Image curImage)
    {
        if (PropsManager.manager.propsCounter[Gloable.PropsType.TIME_INCREASE] > 0)
        {
            PropsManager.manager.IncreaseTime(false);
            //play sound effect
            if (extendTimeAudio != null && PlayerPrefs.GetInt("sound") == 1)
            {
                AudioSource.PlayClipAtPoint(extendTimeAudio, Camera.main.transform.position);
            }
            if (PropsManager.manager.propsCounter[Gloable.PropsType.TIME_INCREASE] <= 0)
            {
                curImage.sprite = unableBtnLists.TimeExtensionBtn;
            }
        }
    }

    public void OnClickBomb(Image curImage)
    {
        int count = PropsManager.manager.propsCounter[Gloable.PropsType.BOMB];
        bool canClick = count > 0 && GameManager.gm.currentGoal;
        if (canClick)
        {
            PropsManager.manager.DestoryGoal(GameManager.gm.currentGoal);
            if (PropsManager.manager.propsCounter[Gloable.PropsType.BOMB] <= 0 || !GameManager.gm.currentGoal)
            {
                curImage.sprite = unableBtnLists.BombBtn;
            }
        }
    }

    public void OnClickBonus(Image curImage)
    {
        if ( PropsManager.manager.propsCounter[Gloable.PropsType.SCORE_INCREASE] > 0)
        {
            PropsManager.manager.IncreaseScore(false);
            //play sound effect
            if (addScoreAudio != null && PlayerPrefs.GetInt("sound") == 1)
            {
                AudioSource.PlayClipAtPoint(addScoreAudio, Camera.main.transform.position);
            }
            if (PropsManager.manager.propsCounter[Gloable.PropsType.SCORE_INCREASE] <= 0)
            {
                curImage.sprite = unableBtnLists.BonusBtn;
            }
        }
    }

    public void OnClickPowerWater(Image curImage)
    {
        int count = PropsManager.manager.propsCounter[Gloable.PropsType.POWER_WATER];
        bool canClick = count > 0 && GameManager.gm.currentGoal;
        if (canClick)
        {
            PropsManager.manager.FastMove(GameManager.gm.currentGoal);
            //play sound effect
            if (powerWaterAudio != null && PlayerPrefs.GetInt("sound") == 1)
            {
                AudioSource.PlayClipAtPoint(powerWaterAudio, Camera.main.transform.position);
            }
            if (PropsManager.manager.propsCounter[Gloable.PropsType.POWER_WATER] <= 0 || !GameManager.gm.currentGoal)
            {
                curImage.sprite = unableBtnLists.PowerWaterBtn;
            }
        }
    }
}