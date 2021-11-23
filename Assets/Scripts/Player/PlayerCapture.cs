using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using StarterAssets;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCapture : MonoBehaviour
{
    //add sound file for shooting
    public bool isTutorial = false;
    public LayerMask goalMask;
    public LongClickButton shootBtn;
    private GameObject player;
    private float timer;                // count intervals between two captures
    [HideInInspector]public bool isShoot = false;
    public GameObject shootAudio;
    private static float TIME_BETWEEN_CAPTURE = 1.0f;    // min intervals between two captures
    
    void Start()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");
        timer = 0.0f;
        shootBtn = GetComponentInChildren<LongClickButton>();
    }

    void LateUpdate()
    {
        bool isPause = isTutorial
            ? SimpleGameManager.gm.pauseGame
            : GameManager.gm.gameState == GameManager.GameState.Pausing;
        if (isPause) return;
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
        if (PlayerPrefs.GetInt("sound") == 1 && !shootAudio.gameObject.GetComponent<AudioSource>().isPlaying)
        {
            shootAudio.gameObject.GetComponent<AudioSource>().Play();
        }
    }

    void CaptureGoals()
    {
        Vector2 middlePos = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray = Camera.main.ScreenPointToRay(middlePos);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, Gloable.MAX_CAPTURE_RADIUS, goalMask))
        {
            if (hitInfo.collider.gameObject.tag.Equals("Goal"))
            {
                GameObject goal =  hitInfo.collider.gameObject;
                if (SimpleGameManager.gm != null)
                {
                    SimpleGameManager.gm.currentGoal = goal;
                }
                else
                {
                    GameManager.gm.currentGoal = goal;
                }

                if (goal.GetComponent<GoalMove>() != null)
                {
                    goal.GetComponent<GoalMove>().shootBtn = shootBtn;
                }
            }
        }
        gameObject.SendMessage("ShootingLaser");
    }

    public void SpeedUpGoal()
    {
        GameObject goal = SimpleGameManager.gm != null ? SimpleGameManager.gm.currentGoal : GameManager.gm.currentGoal;
        GoalMove _goalMove = goal.GetComponent<GoalMove>();
        _goalMove.SpeedUp(_goalMove.curSpeed + 10);
    }

    public void DestroyCurrentGoal()
    {
        StartCoroutine(CoolDownShootButton());
    }
    
    IEnumerator CoolDownShootButton()
    {
        shootBtn.gameObject.GetComponent<Button>().interactable = false;
        yield return new WaitForSeconds(1);
        shootBtn.gameObject.GetComponent<Button>().interactable = true;
    }
}
