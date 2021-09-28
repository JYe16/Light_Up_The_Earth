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

    public UnableBtnSprites unableBtnLists;
    public void OnClikeTimeExtension(Image curImage)
    {
        if (PropsManager.manager.propsCounter.timeIncreaseCounter > 0)
        {
            PropsManager.manager.IncreaseTime();
            if (PropsManager.manager.propsCounter.timeIncreaseCounter <= 0)
            {
                curImage.sprite = unableBtnLists.TimeExtensionBtn;
            }
        }
    }

    public void OnClickBomb(Image curImage)
    {
        int count = PropsManager.manager.propsCounter.bombCounter;
        bool canClick = count > 0 && GameManager.gm.currentGoal;
        if (canClick)
        {
            PropsManager.manager.DestoryGoal(GameManager.gm.currentGoal);
            if (PropsManager.manager.propsCounter.bombCounter <= 0 || !GameManager.gm.currentGoal)
            {
                curImage.sprite = unableBtnLists.BombBtn;
            }
        }
    }

    public void OnClickBonus(Image curImage)
    {
        if ( PropsManager.manager.propsCounter.scoreIncreaseCounter > 0)
        {
            PropsManager.manager.IncreaseScore();
            if (PropsManager.manager.propsCounter.scoreIncreaseCounter <= 0)
            {
                curImage.sprite = unableBtnLists.BonusBtn;
            }
        }
    }

    public void OnClickPowerWater(Image curImage)
    {
        int count = PropsManager.manager.propsCounter.powerWaterCounter;
        bool canClick = count > 0 && GameManager.gm.currentGoal;
        if (canClick)
        {
            PropsManager.manager.FastMove(GameManager.gm.currentGoal);
            if (PropsManager.manager.propsCounter.powerWaterCounter <= 0 || !GameManager.gm.currentGoal)
            {
                curImage.sprite = unableBtnLists.PowerWaterBtn;
            }
        }
    }
}