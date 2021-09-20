using System.Collections;
using System.Collections.Generic;
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
    void Update()
    {
        if (goalValue.isCaptured || target == null) return;
        distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance > minDistance)
        {
            transform.LookAt(target.transform);
            transform.eulerAngles = new Vector3(0.0f, transform.eulerAngles.y, 0.0f);
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
        else
        {
            Vector3 endPosition = transform.position;
            goalValue.isCaptured = true;
            // play sound effect and update score
            goalValue.CapturedEffect();
            // hide laser line
            target.GetComponent<LaserLine>().enabled = false;
            // GameObject.FindGameObjectWithTag("PlayerController").GetComponent<PlayerMove>().canMove = true;
        }
    }
}
