using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBySelf : MonoBehaviour
{
    public float speed = 40f;

    // Update is called once per frame
    void Update ()
    {
        if (GameManager.gm.gameState == GameManager.GameState.Pausing) return;
        this.transform.RotateAround(this.transform.position, this.transform.up, Time.deltaTime * speed);
    }
}