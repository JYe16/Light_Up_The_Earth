using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserLine : MonoBehaviour
{
    [HideInInspector]public Vector3 boundaryPoint;
    [HideInInspector]public GameObject target;
    [HideInInspector]public GoalMove goalMove;
    
    private float moveSpeed = Gloable.LASER_LINE_MOVE_SPEED;
    private LineRenderer lineRenderer;
    private float distance;
    private float counter;
    private bool backWithNothing;
    private float lastX;   // check if the hook touch the goal(when to drag back)
    private PlayerController playerController;
    
    private static float LINE_RENDERER_START = 0.2F;
    private static float LINE_RENDERER_END = 0.5F;
    
    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponentInParent<PlayerController>();
        lineRenderer = GetComponent<LineRenderer>();
        
        lineRenderer.startWidth = LINE_RENDERER_START;
        lineRenderer.endWidth = LINE_RENDERER_END;
        InitProps();
    }
    
    void Update()
    {
        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
        }
        lineRenderer.SetPosition(0, gameObject.transform.position);
        distance = Vector3.Distance(transform.position, boundaryPoint);
        if (backWithNothing)
        {
            BackWithNothing();
        }
        else
        {
            AnimateShootLine();
        }
    }

    private void AnimateShootLine()
    {
        if (counter < distance)
        {
            counter += 0.1f / moveSpeed;
            float x = Mathf.Lerp(0, distance, counter);
            // laser shoots
            if (Mathf.Abs(x - lastX) > 0.000000001)
            {
                Vector3 point1 = transform.position, point2 = target == null ? boundaryPoint : target.transform.position;
                // get the unit vector in the desired direction, multiply by the desired length and add the starting point
                Vector3 direction = Vector3.Normalize(point2 - point1);
                Vector3 nextPoint = x * direction + point1;
                lineRenderer.SetPosition(1, nextPoint);
                lastX = x;
            }
            // laser returns
            else
            {
                if (target != null)
                {
                    // return with goal
                    goalMove.target = gameObject;
                    goalMove.enabled = true;   // activate GoalsController script to move back the target
                    lineRenderer.SetPosition(1, target.transform.position);
                }
                else
                {
                    counter = 0;
                    backWithNothing = true;
                    lastX = 0;
                }
            }
        }
    }

    void BackWithNothing()
    {
        if (counter < distance)
        {
            counter += 0.1f / moveSpeed;
            float x = Mathf.Lerp(0, distance, counter);
            // laser shoots
            if (Mathf.Abs(x - lastX) > 0.000000001)
            {
                Vector3 point1 = transform.position, point2 = boundaryPoint;
                Vector3 direction = Vector3.Normalize(point1 - point2);
                Vector3 nextPoint = x * direction + point2;
                lineRenderer.SetPosition(1, nextPoint);
                lastX = x;
            }
            else
            {
                this.enabled = false;
            }
        }
    }

    private void OnDisable()
    {
        target = null;
        InitProps();
        lineRenderer.enabled = false;
        playerController.changeMoveStatus(true);
    }

    private void InitProps()
    {
        backWithNothing = false;
        lastX = 0;
    }

}
