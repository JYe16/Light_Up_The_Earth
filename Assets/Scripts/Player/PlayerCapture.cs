using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class PlayerCapture : MonoBehaviour
{
    public Transform targetCross;
    private GameObject player;
    private float timer;                // count intervals between two captures
    private LaserLine laserLine;
    private FirstPersonController playerController;
    public bool isShoot = false;
    private static float TIME_BETWEEN_CAPTURE = 1.0f;    // min intervals between two captures
    //add sound file for shooting
    public AudioClip shootAudio;

    void Start()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");
        timer = 0.0f;
        playerController = player.GetComponentInParent<FirstPersonController>();
        laserLine = player.GetComponentInChildren<LaserLine>();
    }

    void LateUpdate()
    {
        if (isShoot && timer > TIME_BETWEEN_CAPTURE)
        {
            timer = 0.0f;
            CaptureGoals();
        }
        else
        {
            // not meet the requirements of capture
            timer += Time.deltaTime;
            if (isShoot) isShoot = false;
        }
    }

    public void Shoot()
    {
        isShoot = true;
        if (shootAudio != null  && PlayerPrefs.GetInt("sound") == 1)
        {
            AudioSource.PlayClipAtPoint(shootAudio, Camera.main.transform.position);
        }
    }

    void CaptureGoals()
    {
        Vector2 middlePos = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray = Camera.main.ScreenPointToRay(middlePos);
        RaycastHit hitInfo;
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
        laserLine.boundaryPoint = GetBoundaryPoint(ray.direction);
        laserLine.enabled = true;
        isShoot = false;
        playerController.changeMoveStatus(false);
    }
    
    private Vector3 GetBoundaryPoint(Vector3 direction)
    {
        Vector3 initialPos = player.transform.position;
        return initialPos + direction.normalized * Gloable.MAX_CAPTURE_RADIUS;
    }
}
