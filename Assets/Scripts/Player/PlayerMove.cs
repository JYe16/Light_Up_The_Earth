using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 60.0f;     // player's move speed
    public float rotateSpeed = 40.0f;   // player's rorate speed

    // TODO: animation when move???
    // private Animator _animator; 

    private float h;     // get player's horizontal axis input
    private float v;     // get player's vertical axis inout
    

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal"); 
        float v = Input.GetAxisRaw("Vertical");
        MoveAndRotate(h, v);
    }

    void MoveAndRotate(float h, float v)
    {
        if (v > 0) transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);           // move forward
        else if (v < 0) transform.Translate(-Vector3.forward * moveSpeed * Time.deltaTime);     // move backward
        
        // TODO: play animation
        // ......
        
        transform.Rotate(Vector3.up * h * rotateSpeed * Time.deltaTime);
    }
}
