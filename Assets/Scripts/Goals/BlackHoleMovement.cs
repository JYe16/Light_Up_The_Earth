using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEditor;
//using TreeEditor;
using UnityEngine;

public class BlackHoleMovement : MonoBehaviour
{
    public GameObject SpaceShip;
    public LaserControl laserControl;
    public float AbsorbSpeed;
    public CanvasGroup canvasGroup;
    public bool isArrived=false;
    public bool isCaptured=false;
    public bool isBlackHole=true;
    public float alpha = 1.0f;
    public float darken = 2.0f;
    
    void Start()
    {
        laserControl = GetComponent<LaserControl>();
    }

    private void Update()
    {
        if (isCaptured)
        {
            canvasGroup.alpha += darken/6* Time.deltaTime;
        }
    }

    public void SpaceshipMovement()
    {
        float distance = Vector3.Distance(SpaceShip.transform.position, transform.position) / 2;
        transform.DOMove(SpaceShip.transform.position, distance/AbsorbSpeed);
        SpaceShip.transform.DOShakeRotation(5f, 50, 5, 10f);
        isCaptured = true;
        canvasGroup.gameObject.SetActive(true);
        StartCoroutine(Reach(1.5f));
    }

    IEnumerator Reach(float delay)
    {
        yield return new WaitForSeconds(delay);
        isArrived = true;
        GameManager.gm.gameState = GameManager.GameState.GameOver;
    }

}