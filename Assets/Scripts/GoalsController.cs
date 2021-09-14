using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class GoalsController : MonoBehaviour
{
    public int value = 10;          // earning score after capture this goal
    public float moveSpeed = 18.0f;      // move speed of the goal
    public float minDistance = 100.0f;    // lower than minDistance, stop dragging back
    AudioSource goal_audio;

    // Update is called once per frame
    void Update()
    {   
        //此处不知为何minDisdance被设成2.0f，所以导致判定一直失效，所以强制在update内将其设置为100.f
        minDistance = 100.0f;
        GameObject spaceship = GameObject.Find("Spaceship");
        float distance = Vector3.Distance(transform.position, spaceship.transform.position);   // calculate the distance between goal and spaceship

        if (checkReached(distance, minDistance))
        {
            transform.LookAt(spaceship.transform);
            transform.eulerAngles = new Vector3(0.0f, transform.eulerAngles.y, 0.0f);
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }else{
            goal_audio = GetComponent<AudioSource>();
            goal_audio.Play();
            
            //TODO: Add a delay here
            Destroy(gameObject);
            //update score
            UpdateScore.score += value;
            return;
        }
    }

    bool checkReached(float distance, float minDistance){
        return (distance > minDistance);
    }

}
