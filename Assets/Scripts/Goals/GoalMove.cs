using System.Collections;
using System.Collections.Generic;
//using TreeEditor;
using UnityEngine;

public class GoalMove : MonoBehaviour
{
    public float moveSpeed;
    public GameObject target;
    
    private float distance;
    private GoalValue goalValue;
    private bool isHide = false;
    private RotateBySelf rotateEffect;
    private float minDistance = 100.0f;
    
    void Start()
    {
        goalValue = GetComponent<GoalValue>();
        rotateEffect = GetComponent<RotateBySelf>();
        if (rotateEffect != null) rotateEffect.enabled = false;
    }

    void FixedUpdate()
    {
        if ( goalValue.isCaptured || target == null) return;
        distance = Vector3.Distance(transform.position, target.transform.position);
        float t = distance / moveSpeed;
        if (distance > minDistance)
        {
            Vector3 goalPosition = transform.position;
            Vector3 spaceshipPosition = target.transform.position;
            transform.position = Vector3.MoveTowards(goalPosition, Vector3.Lerp(goalPosition, spaceshipPosition, t), moveSpeed);
        }
        else
        {
            goalValue.isCaptured = true;
            if(!isHide) goalValue.CapturedEffect();
            // hide laser line
            target.GetComponent<LaserLine>().enabled = false;
        }
    }

    public void HideGoal()
    {
        gameObject.GetComponent<Renderer>().enabled = false;
        moveSpeed = Gloable.LASER_LINE_MOVE_SPEED;
        isHide = true;
    }
}
