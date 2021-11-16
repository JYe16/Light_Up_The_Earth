using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using JetBrains.Annotations;
//using TreeEditor;
using UnityEngine;

public class GoalMove : MonoBehaviour
{
    [HideInInspector]public AudioClip destroyGoalAudio;
    [HideInInspector]public GameObject explosion;
    // params about fracture
    [CanBeNull]public GameObject fracturedPieces;
    public GameObject originalGoal;
    public float force;
    public float radius;
    public float moveSpeed;
    public bool isTutorial = false;
    
    private bool hideCompleteModel = false;
    private float distance;
    private GoalValue goalValue;
    private float destroyPiecesDuration = 5.0f;
    private LaserControl laserControl;
    void Start()
    {
        goalValue = GetComponent<GoalValue>();
    }

    public void ReturnGoal(Vector3 distroyPos, LaserControl laserControl)
    {
        //判断当前游戏状态
        bool isPause = isTutorial
            ? SimpleGameManager.gm.pauseGame
            : GameManager.gm.gameState == GameManager.GameState.Pausing;
        //看是不是暂停或者已经抓住了
        if (isPause || goalValue.isCaptured) return;
        //如果在return了，那么停止之前的旋转
        if (GetComponent<RotateBySelf>())
        {
            GetComponent<RotateBySelf>().enabled = false;
        }
        //初始化一个laserControl
        this.laserControl = laserControl;
        
        //计算物体原来的位置和回收后消失的位置
        distance = Vector3.Distance(transform.position, distroyPos);
        
        //激光的状态发生了改变
        laserControl.ChangeBackStatus(true);
       
        gameObject.transform.DOMove(distroyPos, distance / moveSpeed).OnComplete(() =>
        {
            laserControl.ChangeBackStatus(false);
            AfterReturn();
        });
    }

    //在拉回物体会的状态
    private void AfterReturn()
    {
        goalValue.isCaptured = true;
        if(!hideCompleteModel) goalValue.CapturedEffect();
        // hide laser line
        laserControl.HideLaserEnableMove();
        DestroyGoal();
    }

    //销毁物体
    private void DestroyGoal()
    {
        foreach (Transform child in transform) {
            Destroy(child.gameObject);
        }
        Destroy(gameObject);
    }

    public void ExplosionAndHide()
    {
        originalGoal.GetComponent<Renderer>().enabled = false;
        hideCompleteModel = true;
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
                Destroy(rootTrans.GetChild(i).gameObject, destroyPiecesDuration);
            }
        }
        // change speed to lase line speed after explosion
        moveSpeed = Gloable.LASER_LINE_MOVE_SPEED;
    }
}
