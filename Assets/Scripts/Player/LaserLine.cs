using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserLine : MonoBehaviour
{
    private GameObject target;
    private Vector3 boundaryPoint;
    private float moveSpeed = 6.0f;
    private GoalMove goalMove;
    private LineRenderer lineRenderer;
    private float distance;
    private float counter;
    private float boundaryRange;
    private bool isBack;
    private float lastX;   // check if the hook touch the goal(when to drag back)
    private static float LINE_RENDERER_START = 0.2F;
    private static float LINE_RENDERER_END = 0.5F;
    
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = LINE_RENDERER_START;
        lineRenderer.endWidth = LINE_RENDERER_END;

        isBack = false;
        lineRenderer.SetPosition(0, transform.position);
        distance = target == null ? boundaryRange : Vector3.Distance(transform.position, target.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (!lineRenderer.enabled)
            lineRenderer.enabled = true;
        if (counter < distance)
        {
            counter += 0.1f / moveSpeed;
            float x = Mathf.Lerp(0, distance, counter);
            // laser shoots
            if (Mathf.Abs(x - lastX) > 0.000000001)
            {
                Vector3 point1 = transform.position, point2 = target == null ? boundaryPoint : target.transform.position;
                // get the unit vector in the desired direction, multiply by the desired length and add the starting point
                Vector3 direction = Vector3.Normalize(isBack ? (point1 - point2) : (point2 - point1));
                Vector3 nextPoint = x * direction + (isBack ? point2 : point1);
                lineRenderer.SetPosition(1, nextPoint);
                lastX = x;
            }
            // laser returns
            else
            {
                if (target != null)
                {
                    // return with goal
                    goalMove.enabled = true;   // activate GoalsController script to move back the target
                    lineRenderer.SetPosition(1, target.transform.position);
                }
                else if(!isBack)
                {
                    counter = 0;
                    isBack = true;
                }
                else if(isBack)
                {
                    this.enabled = false;
                }
            }
        }
    }

    private void OnDisable()
    {
        target = null;
        lineRenderer.enabled = false;
    }

    // init props
    public void SetProps( GameObject goal, float captureRange, Vector3 crossPoint)
    {
        boundaryRange = captureRange;
        if (goal == null)
        {
            // find no goal
            boundaryPoint = GetBoundaryPoint(captureRange, crossPoint);
        }
        else
        {
            // find a goal
            target = goal;
            goalMove = goal.GetComponent<GoalMove>();
        }
    }

    private Vector3 GetBoundaryPoint(float captureRange, Vector3 crossPoint)
    {
        return transform.position + (crossPoint - transform.position).normalized * captureRange;
    }

}
