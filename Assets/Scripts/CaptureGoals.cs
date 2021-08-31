using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureGoals : MonoBehaviour
{
    public float captureRange = 50.0f;          // max capture range
    public float timeBetweenCaptures = 1.0f;    // min intervals between two captures
    
    // TODO: sound effects & animation when launch hook...
    // public AudioClip captureAudio;
    // private Animator animator;

    private float timer;                // count intervals between two captures
    private Ray ray;
    private RaycastHit hitInfo;
    

    // Start is called before the first frame update
    void Start()
    {
        timer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && timer > timeBetweenCaptures)
        {
            timer = 0.0f;
            
            // TODO: play animation
            // ...
            
            Invoke("LaunchHook", 0.5f);     // the duration of animation is 0.5s
        }
        else
        {
            // not meet the requirements of capture
            timer += Time.deltaTime;
        }
    }

    void LaunchHook()
    {
        // TODO: play sound effect
        // ...

        ray.origin = Camera.main.transform.position;
        ray.direction = Camera.main.transform.forward;
        if (Physics.Raycast(ray, out hitInfo, captureRange))
        {
            // the hook touches a goal
            if (hitInfo.collider.gameObject.tag == "Goals")
            {
                GameObject goal = hitInfo.collider.gameObject;
                // draw the rope route
                DrawHookLine drawHookLine = GameObject.Find("Hook Line").GetComponent<DrawHookLine>();
                drawHookLine.target = goal;
                drawHookLine.enabled = true;
            }
        }
        else
        {
            // TODO: the hook touches the boundary
            
        }
    }
}

