using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DrawHookLine : MonoBehaviour
{
    public Transform startPoint;  
    public GameObject target;  
    public float drawSpeed = 6.0f;

    private LineRenderer lineRenderer;
    private float distance;
    private float counter;
    private float isStop;   // check if the hook touch the goal(when to drag back)
    
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, startPoint.position);
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;

        distance = Vector3.Distance(startPoint.position, target.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        // no end point: hide the line
        if (!target)
        {
            // TODO: ???when to activate
            lineRenderer.enabled = false;
            return;
        }
        if (counter < distance)
        {
            counter += 0.1f / drawSpeed;
            float x = Mathf.Lerp(0, distance, counter);
            // launch the hook
            if (Mathf.Abs(x - isStop) > 0.000000001)
            {
                Vector3 point1 = startPoint.position, point2 = target.transform.position;
                // get the unit vector in the desired direction, multiply by the desired length and add the starting point
                Vector3 nextPoint = x * Vector3.Normalize(point2 - point1) + point1;
                lineRenderer.SetPosition(1, nextPoint);
                isStop = x;
            }
            // drag the goal back
            else
            {
                target.GetComponent<GoalsController>().enabled = true;   // activate GoalsController script to move the target
                target.GetComponent<RotateBySelf>().enabled = false;     // stop target's self rotation
                // TODO: reverse draw line
                lineRenderer.SetPosition(1, target.transform.position);
            }
            UpdateScore.score = 10;
        }
    }
}
