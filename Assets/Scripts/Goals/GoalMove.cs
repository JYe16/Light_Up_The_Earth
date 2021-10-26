using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
//using TreeEditor;
using UnityEngine;

public class GoalMove : MonoBehaviour
{
    [HideInInspector]public GameObject target;
    [HideInInspector]public AudioClip destroyGoalAudio;
    [HideInInspector]public GameObject explosion;
    public GameObject fracturedPieces;
    public GameObject originalGoal;
    public float force;
    public float radius;
    public float moveSpeed;
    
    private float distance;
    private GoalValue goalValue;
    private bool isHide = false;
    private RotateBySelf rotateEffect;
    private float minDistance = 180.0f;
    private float destroyPiecesDuration = 5.0f;
    
    void Start()
    {
        goalValue = GetComponent<GoalValue>();
        rotateEffect = GetComponent<RotateBySelf>();
        if (rotateEffect != null) rotateEffect.enabled = false;
    }

    void FixedUpdate()
    {
        if ( GameManager.gm.gameState == GameManager.GameState.Pausing || goalValue.isCaptured || target == null) return;
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
            DestroyGoal();
        }
    }

    private void DestroyGoal()
    {
        foreach (Transform child in transform) {
            Destroy(child.gameObject);
        }
        Destroy(gameObject);
    }

    public void ExplosionAndHide()
    {
        isHide = true;
        originalGoal.GetComponent<Renderer>().enabled = false;
        Explode();
    }

    private void Explode()
    {
        fracturedPieces.SetActive(true);
        Vector3 screenCenter = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        // play explosion fire effect
        GameObject bombObj = Instantiate(explosion, screenCenter, Quaternion.identity);
        Destroy(bombObj, destroyPiecesDuration / 2);
        // play explosion sound effect
        if(destroyGoalAudio != null && PlayerPrefs.GetInt("sound") == 1) 
            AudioSource.PlayClipAtPoint(destroyGoalAudio, Camera.main.transform.position, 0.3f);
        // add force to pieces
        Vector3 boundCenter = originalGoal.GetComponent<MeshFilter>().sharedMesh.bounds.center;
        Vector3 center = fracturedPieces.transform.localToWorldMatrix.MultiplyPoint(boundCenter);
        Transform rootTrans = fracturedPieces.transform;
        for (int i = 0; i < rootTrans.childCount; i++)
        {
            Transform pieceTrans = rootTrans.GetChild(i);
            Rigidbody body = pieceTrans.GetComponent<Rigidbody>();
            if (body != null)
            {
                body.AddExplosionForce(force, center, radius);
                Destroy(rootTrans.GetChild(i), destroyPiecesDuration);
            }
        }
        // change speed to lase line speed after explosion
        moveSpeed = Gloable.LASER_LINE_MOVE_SPEED;
    }
}
