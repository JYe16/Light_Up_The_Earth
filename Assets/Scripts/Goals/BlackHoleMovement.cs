using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using JetBrains.Annotations;
//using TreeEditor;
using UnityEngine;

public class BlackHoleMovement : MonoBehaviour
{

    public GameObject SpaceShip;
    public LaserControl laserControl;
    public float AbsorbSpeed;
    public bool isArrived=false;
    public bool isBlackHole=true;
    void Start()
    {
      
    }

    public void SpaceshipMovement()
    {
        float distance = Vector3.Distance(SpaceShip.transform.position, transform.position)-100f;
        SpaceShip.transform.DOMove(transform.position, distance / AbsorbSpeed);
        StartCoroutine(Reach(10f));
    }

    IEnumerator Reach(float delay)
    {
        yield return new WaitForSeconds(delay);
        isArrived = true;
    }

}