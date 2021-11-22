using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class BlackHoleMovement : MonoBehaviour
{

    public GameObject SpaceShip;
    public LaserControl laserControl;
    public float AbsorbSpeed;
    public CanvasGroup canvasGroup;
    public bool isCaptured=false;
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
        float distance = Vector3.Distance(SpaceShip.transform.position, transform.position)-100f;
        SpaceShip.transform.DOMove(transform.position, distance/2/ AbsorbSpeed);
        isCaptured = true;
        StartCoroutine(Reach(2.5f));
    }

    IEnumerator Reach(float delay)
    {
        yield return new WaitForSeconds(delay);
        GameManager.gm.gameState = GameManager.GameState.GameOver;
    }

}