using System;
using System.Collections;
using System.Collections.Generic;
//using TreeEditor;
using UnityEngine;

public class GoalMove : MonoBehaviour
{
    [HideInInspector]public GameObject target;
    [HideInInspector]public AudioClip destroyGoalAudio;
    [HideInInspector]public GameObject explosion;
    
    public float moveSpeed;
    
    private float distance;
    private GoalValue goalValue;
    private bool isHide = false;
    private RotateBySelf rotateEffect;
    private float minDistance = 180.0f;
    
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

    public void ExplosionAndHide()
    {
        StartCoroutine(GoalExplosion());
    }

    IEnumerator GoalExplosion()
    {
        Vector3 screenCenter = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        isHide = true;
        Destroy(Instantiate(explosion, screenCenter, Quaternion.identity), 3f);
        if(destroyGoalAudio != null && PlayerPrefs.GetInt("sound") == 1) 
            AudioSource.PlayClipAtPoint(destroyGoalAudio, Camera.main.transform.position);
        gameObject.GetComponent<Renderer>().enabled = false;
        yield return new WaitForSeconds(1);
        moveSpeed = Gloable.LASER_LINE_MOVE_SPEED;
    }
}
