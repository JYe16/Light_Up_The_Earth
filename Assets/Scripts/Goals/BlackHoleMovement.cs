using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class BlackHoleMovement : MonoBehaviour
{

    public GameObject SpaceShip;
    public LaserControl laserControl;
    public float AbsorbSpeed;
    void Start()
    {
        laserControl = GetComponent<LaserControl>();
    }

    public void SpaceshipMovement()
    {
        float distance = Vector3.Distance(SpaceShip.transform.position, transform.position)-100f;
        SpaceShip.transform.DOMove(transform.position, distance/3/ AbsorbSpeed);
        StartCoroutine(Reach(3f));
    }

    IEnumerator Reach(float delay)
    {
        yield return new WaitForSeconds(delay);
        GameManager.gm.gameState = GameManager.GameState.GameOver;
    }

}