using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineAnimator : MonoBehaviour
{
    public float moveSpeed = 6.0f;
    public GameObject target;
    private LineRenderer lineRenderer;
    private float duration;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AnimateLine(GameObject target)
    {
        float startTime = Time.time;
        Vector3 startPos = lineRenderer.GetPosition(0);
        Vector3 endPos = target.transform.position;
        duration = Vector3.Distance(startPos, endPos) / moveSpeed;
        Vector3 nextPoint = startPos;
        while (nextPoint != endPos)
        {
            float t = (Time.time - startTime) / duration;
            nextPoint = Vector3.Lerp(startPos, endPos, t);
            lineRenderer.SetPosition(1, nextPoint);
        }
    }
}
