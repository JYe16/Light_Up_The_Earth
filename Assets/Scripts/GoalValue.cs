using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalValue : MonoBehaviour
{
    public int value;   // earned score after capture this goal
    public AudioClip captureGoalAudio;
    public bool isCaptured;

    // Start is called before the first frame update
    void Start()
    {
        isCaptured = false;
    }

    public void CapturedEffect()
    {
        if(captureGoalAudio != null) 
            AudioSource.PlayClipAtPoint(captureGoalAudio, Camera.main.transform.position);
        if (GameManager.gm != null)
        {
            GameManager.gm.AddScore(value);
        }
        Destroy(gameObject);
    }
}
