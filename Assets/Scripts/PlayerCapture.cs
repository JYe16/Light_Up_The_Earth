using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCapture : MonoBehaviour
{
    public float captureRange = 50.0f;
    // public AudioClip shootingAudio;
    public GameObject targetCross;

    private float timer;                // count intervals between two captures
    private Ray ray;
    private RaycastHit hitInfo;
    private LaserLine laserLine;
    
    private static float TIME_BETWEEN_CAPTURE = 1.0f;    // min intervals between two captures
    
    // Start is called before the first frame update
    void Start()
    {
        timer = 0.0f;
        laserLine = GetComponent<LaserLine>();
    }

    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space) && timer > TIME_BETWEEN_CAPTURE)
        {
            timer = 0.0f;
            GameObject.FindGameObjectWithTag("PlayerController").GetComponent<PlayerMove>().canMove = false;
            CaptureGoals();
        }
        else
        {
            // not meet the requirements of capture
            timer += Time.deltaTime;
        }
    }

    void CaptureGoals()
    {
        // AudioSource.PlayClipAtPoint(shootingAudio, transform.position);
        ray.origin = Camera.main.transform.position;
        ray.direction = targetCross.transform.forward;
        if (Physics.Raycast(ray, out hitInfo, captureRange))
        {
            if (hitInfo.collider.gameObject.tag.Equals("Goal"))
            {
                laserLine.SetProps(hitInfo.collider.gameObject,captureRange, targetCross.transform.position);
                laserLine.enabled = true;
            }
        }
        else
        {
            laserLine.SetProps(null,captureRange, targetCross.transform.position);
            laserLine.enabled = true;
        }
    }
}
