using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using StarterAssets;
using UnityEngine;

public class LaserControl : MonoBehaviour
{
    public bool isShoot;
    public GameObject laserBeamPrefab;
    public Transform shootPosition;

    public GameObject laserBeam;
    private GameObject curGoal;
    private LineRenderer _lineRenderer;
    private Vector3 statrtPos, endPos;
    void Start()
    {
        laserBeam = Instantiate(laserBeamPrefab, shootPosition);
        _lineRenderer = laserBeam.GetComponent<LineRenderer>();
        statrtPos = shootPosition.transform.position;
        _lineRenderer.SetPosition(0, statrtPos);
        DisableLaserBeam();
    }

    public void ShootingLaser()
    {
        EnableLaserBeam();
        // launch laser
        curGoal = GameManager.gm.currentGoal;
        endPos = curGoal == null ? GetBoundaryPosition(shootPosition.position, Gloable.MAX_CAPTURE_RADIUS / 4) : curGoal.transform.position;
        _lineRenderer.SetPosition(1, endPos);
        laserBeam.transform.position = statrtPos;
        // return laser
        StartCoroutine(ReturnLaser(!curGoal));
    }

    IEnumerator ReturnLaser(bool withNothing)
    {
        yield return new WaitForSeconds(1);
        if (withNothing)
        {
            float duration = Gloable.MAX_CAPTURE_RADIUS / Gloable.LASER_LINE_MOVE_SPEED / 4;
            DOTween.To(() => _lineRenderer.GetPosition(1), newVal =>
            {
                _lineRenderer.SetPosition(1, newVal);
            }, shootPosition.position, duration).OnComplete(HideLaserEnableMove);
        }
        else
        {
            Vector3 distroyPos = GetBoundaryPosition(shootPosition.position, 180);
            curGoal.GetComponent<GoalMove>().ReturnGoal(distroyPos, this);
            _lineRenderer.SetPosition(1, curGoal.transform.position);
        }
    }

    private Vector3 GetBoundaryPosition(Vector3 initialPos, int distance)
    {
        Vector2 middlePos = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray = Camera.main.ScreenPointToRay(middlePos);
        return initialPos + ray.direction.normalized * distance;
    }

    private void DisableLaserBeam()
    {
        isShoot = false;
        laserBeam.SetActive(false);
    }

    private void EnableLaserBeam()
    {
        isShoot = true;
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
