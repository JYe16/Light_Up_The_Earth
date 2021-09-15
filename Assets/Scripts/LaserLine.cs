using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserLine : MonoBehaviour
{
    private GameObject target;
    private float moveSpeed = 6.0f;
    private GoalMove goalMove;
    private LineRenderer lineRenderer;
    private float distance;
    private float counter;
    private float lastX;   // check if the hook touch the goal(when to drag back)
    
    private static float LINE_RENDERER_START = 0.2F;
    private static float LINE_RENDERER_END = 0.5F;
    
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.startWidth = LINE_RENDERER_START;
        lineRenderer.endWidth = LINE_RENDERER_END;

        distance = Vector3.Distance(transform.position, target.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (!lineRenderer.enabled)
            lineRenderer.enabled = true;
        // no end point: hide the line
        if (!target)
        {
            // TODO: ???when to activate
            lineRenderer.enabled = false;
            return;
        }
        if (counter < distance)
        {
            counter += 0.1f / moveSpeed;
            float x = Mathf.Lerp(0, distance, counter);
            // launch the hook
            if (Mathf.Abs(x - lastX) > 0.000000001)
            {
                Vector3 point1 = transform.position, point2 = target.transform.position;
                // get the unit vector in the desired direction, multiply by the desired length and add the starting point
                Vector3 nextPoint = x * Vector3.Normalize(point2 - point1) + point1;
                lineRenderer.SetPosition(1, nextPoint);
                lastX = x;
            }
            // drag the goal back
            else
            {
                goalMove.enabled = true;   // activate GoalsController script to move back the target
                lineRenderer.SetPosition(1, target.transform.position);
            }
        }
    }

    // init props
    public void SetTarget(GameObject goal)
    {
        target = goal;
        goalMove = goal.GetComponent<GoalMove>();
    }
}
