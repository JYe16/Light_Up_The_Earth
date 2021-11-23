using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class BlackHoleMovement : MonoBehaviour
{
    public GameObject SpaceShip;
    public float AbsorbSpeed;
    public CanvasGroup canvasGroup;
    public bool isCaptured=false;
    public float darken = 2.0f;

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
        GameManager.gm.gameState = GameManager.GameState.GameOver;
    }
}
