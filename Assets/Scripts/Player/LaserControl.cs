using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using StarterAssets;
using UnityEngine;

public class LaserControl : MonoBehaviour
{
    public bool backWithGoal = false;
    public GameObject laserBeamPrefab;
    public Transform shootPosition;
    public GameObject laserBeam;
    public bool isTutorial = false;
    
    private GameObject curGoal;
    private LineRenderer lineRenderer;
    private Vector3 startPos, endPos;
    private static float MIN_DISTORY_DISTANCE = 500.0f;
    void Start()
    {
        laserBeam = Instantiate(laserBeamPrefab, shootPosition);
        lineRenderer = laserBeam.GetComponent<LineRenderer>();
        DisableLaserBeam();
    }

    private void Update()
    {
        bool existCurGoal = isTutorial ? SimpleGameManager.gm.currentGoal != null : GameManager.gm.currentGoal != null;
        if (backWithGoal && existCurGoal)
        {
            Vector3 endPos = isTutorial
                ? SimpleGameManager.gm.currentGoal.transform.position
                : GameManager.gm.currentGoal.transform.position;
            lineRenderer.SetPosition(1, endPos);
        }
        if (curGoal.GetComponent<BlackHoleMovement>().isArrived == true)
        {
            GameManager.gm.gameState = GameManager.GameState.GameOver;
        }
    }

    public void ChangeBackStatus(bool backStatus)
    {
        backWithGoal = backStatus;
    }
    
    public void ShootingLaser()
    {
        EnableLaserBeam();
        startPos = shootPosition.transform.position;
        lineRenderer.SetPosition(0, startPos);
        // launch laser
        curGoal = isTutorial ? SimpleGameManager.gm.currentGoal : GameManager.gm.currentGoal;
        endPos = curGoal == null
                ? GetBoundaryPosition(shootPosition.position, Gloable.MAX_CAPTURE_RADIUS / 4)
                : curGoal.transform.position;
            lineRenderer.SetPosition(1, endPos);
            laserBeam.transform.position = startPos;
            // return laser
            if (curGoal.GetComponent<GoalValue>().isBlackHole)
            {
                DisableLaserBeam();
                curGoal.GetComponent<BlackHoleMovement>().SpaceshipMovement();
               
            }
            else
            {
                StartCoroutine(ReturnLaser(!curGoal));
            }
    }

    //光束的返回
    IEnumerator ReturnLaser(bool withNothing)
    {
        yield return new WaitForSeconds(1);
        if (withNothing)
        {
            float duration = Gloable.MAX_CAPTURE_RADIUS / Gloable.LASER_LINE_MOVE_SPEED / 30;
            DragBack(duration);
        }
        else 
        {
            float distance = Vector3.Distance(shootPosition.position, curGoal.transform.position);
            Vector3 distroyPos = distance > MIN_DISTORY_DISTANCE ? GetBoundaryPosition(shootPosition.position, MIN_DISTORY_DISTANCE) : curGoal.transform.position;
            curGoal.GetComponent<GoalMove>().ReturnGoal(distroyPos, this);
            lineRenderer.SetPosition(1, curGoal.transform.position);
        }
    }



    //将物体从远处拖回来
    private void DragBack(float duration)
    {
        DOTween.To(() => lineRenderer.GetPosition(1), newVal =>
        {
            lineRenderer.SetPosition(1, newVal);
        }, shootPosition.position, duration).OnComplete(HideLaserEnableMove);
    }

    private Vector3 GetBoundaryPosition(Vector3 initialPos, float distance)
    {
        Vector2 middlePos = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray = Camera.main.ScreenPointToRay(middlePos);
        return initialPos + ray.direction.normalized * distance;
    }
    
    private void DisableLaserBeam()
    {
        laserBeam.SetActive(false);
    }

    private void EnableLaserBeam()
    {
        laserBeam.SetActive(true);
    }

    public void HideLaserEnableMove()
    {
        DisableLaserBeam();
        PlayerCapture playerCapture = GetComponent<PlayerCapture>();
        playerCapture.isShoot = false;
        playerCapture.ChangeMoveStatus(true);
    }
}
