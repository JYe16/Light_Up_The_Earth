using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class GoalMove : MonoBehaviour
{
    public float moveSpeed = 10.0f;
    public float minDistance = 100.0f;
    public GameObject target;

    private float distance;
    private GoalValue goalValue;
    private RotateBySelf rotateEffect;
    
    // Start is called before the first frame update
    void Start()
    {
        goalValue = GetComponent<GoalValue>();
        rotateEffect = GetComponent<RotateBySelf>();
        if (rotateEffect != null) rotateEffect.enabled = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (goalValue.isCaptured || target == null) return;
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
            // play sound effect and update score
            goalValue.CapturedEffect();
            // hide laser line
            target.GetComponent<LaserLine>().enabled = false;
        }
    }
}
