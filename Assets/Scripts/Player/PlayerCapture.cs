using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class PlayerCapture : MonoBehaviour
{
    public Transform targetCross;
    
    private float timer;                // count intervals between two captures
    private Ray ray;
    private RaycastHit hitInfo;
    private LaserLine laserLine;
    private FirstPersonController playerController;
    
    private static float TIME_BETWEEN_CAPTURE = 1.0f;    // min intervals between two captures

    // Start is called before the first frame update
    void Start()
    {
        timer = 0.0f;
        playerController = GetComponentInParent<FirstPersonController>();
        laserLine = GetComponent<LaserLine>();
    }

    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space) && timer > TIME_BETWEEN_CAPTURE)
        {
            timer = 0.0f;
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
        ray.origin = Camera.main.transform.position;
        ray.direction = targetCross.forward;
        if (Physics.Raycast(ray, out hitInfo, Gloable.MAX_CAPTURE_RADIUS))
        {
            if (hitInfo.collider.gameObject.tag.Equals("Goal"))
            {
                GameObject goal =  hitInfo.collider.gameObject;
                GameManager.gm.currentGoal = goal;
                laserLine.target = goal;
                laserLine.goalMove = goal.GetComponent<GoalMove>();
                laserLine.enabled = true;
                playerController.changeMoveStatus(false);
                return;
            }
        }
        laserLine.boundaryPoint = GetBoundaryPoint();
        laserLine.enabled = true;
        playerController.changeMoveStatus(false);
    }
    
    private Vector3 GetBoundaryPoint()
    {
        return transform.position + (targetCross.position - transform.position).normalized * Gloable.MAX_CAPTURE_RADIUS;
    }
}
