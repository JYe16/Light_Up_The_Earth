using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using JetBrains.Annotations;
//using TreeEditor;
using UnityEngine;

public class blackhole : MonoBehaviour
{
    public GameObject fracturedPieces;
    public GameObject originalGoal;
    public float AbsorbSpeed;
    public bool isTutorial = false;
    
    private bool hideCompleteModel = false;
    private float distance;

    private float destroyPiecesDuration = 5.0f;
    private LaserControl laserControl;
    void Start()
    {
      
    }

    public void ReturnGoal(Vector3 distroyPos, LaserControl laserControl)
    {
        //判断当前游戏状态
        bool isPause = isTutorial
            ? SimpleGameManager.gm.pauseGame
            : GameManager.gm.gameState == GameManager.GameState.Pausing;
        //看是不是暂停或者已经抓住了
        if (isPause) return;
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
        gameObject.transform.DOMove(distroyPos, distance / AbsorbSpeed).OnComplete(() =>
        {
            laserControl.ChangeBackStatus(false);
            AfterReturn();
        });
    }

    //在拉回物体会的状态
    private void AfterReturn()
    {
        // hide laser line
        laserControl.HideLaserEnableMove();
        DestroyGoal();
    }

    //销毁物体
    private void DestroyGoal()
    {
        Destroy(gameObject);
    }


}