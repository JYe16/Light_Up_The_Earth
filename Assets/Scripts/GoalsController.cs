using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class GoalsController : MonoBehaviour
{
    public int value = 10;          // earning score after capture this goal
    public float moveSpeed = 8.0f;      // move speed of the goal
    public float minDistance = 2.0f;    // lower than minDistance, stop dragging back

    // TODO: sound effect
    // public AudioClip capturedAudio; 

    // Update is called once per frame
    void Update()
    {
        GameObject spaceship = GameObject.Find("Spaceship");
        float distance = Vector3.Distance(transform.position, spaceship.transform.position);   // calculate the distance between goal and spaceship
        if (distance > minDistance)
        {
            transform.LookAt(spaceship.transform);
            transform.eulerAngles = new Vector3(0.0f, transform.eulerAngles.y, 0.0f);
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
        // TODO: get score after capture
        Destroy(gameObject, 3.0f);
    }
}
