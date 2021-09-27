using UnityEngine;

public class PropsButtons : MonoBehaviour
{
    public void OnClikeTimeExtension()
    {
        if (PropsManager.manager.propsCounter.timeIncreaseCounter > 0)
        {
            PropsManager.manager.IncreaseTime();
        }
    }

    public void OnClickBomb()
    {
        if (PropsManager.manager.propsCounter.bombCounter > 0 && GameManager.gm.currentGoal)
        {
            PropsManager.manager.DestoryGoal(GameManager.gm.currentGoal);
        }
    }

    public void OnClickBonus()
    {
        if (PropsManager.manager.propsCounter.scoreIncreaseCounter > 0)
        {
            PropsManager.manager.IncreaseScore();
        }
    }

    public void OnClickPowerWater()
    {
        if (PropsManager.manager.propsCounter.powerWaterCounter > 0 && GameManager.gm.currentGoal)
        {
            PropsManager.manager.FastMove(GameManager.gm.currentGoal);
        }
    }
}