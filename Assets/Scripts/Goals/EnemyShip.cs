using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class EnemyShip : MonoBehaviour
{
    private NavMeshAgent agent;
    public Image alert;
    public GameObject centerTip;
    public Transform player;
    public LayerMask whatIsPlayer;
    public Slider healthBar;
    public float health;
    public int value;
    public int damage;
    //States
    public float alertRange, stealRange;
    private bool playerInAlertRange, playerInStealRange;

    private string successTip = "You got";
    private string failTip = "You were stolen";

    private bool hasAlert = false;

    private Text line1;
    private Text line2;
    private Text line3;

    private void Start()
    {
        Init();
        RotateToPlayer();
    }

    private void Init()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;

        alert.gameObject.SetActive(false);
        
        centerTip.transform.localScale = Vector3.zero;
        centerTip.SetActive(false);
        Text[] lines = centerTip.GetComponentsInChildren<Text>();
        line1 = lines[0];
        line2 = lines[1];
        line3 = lines[2];

        healthBar.maxValue = health;
        healthBar.value = health;
    }

    private void RotateToPlayer()
    {
        Vector3 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
        transform.DORotate(new Vector3(0, -angle, 0), 0.5f);
    }

    private void Update()
    {
        //Check for sight and attack range
        playerInAlertRange = Physics.CheckSphere(transform.position, alertRange, whatIsPlayer);
        playerInStealRange = Physics.CheckSphere(transform.position, stealRange, whatIsPlayer);

        if (playerInAlertRange && !hasAlert)
        {
            hasAlert = true;
            Alert();
        }

        if (playerInStealRange) 
            StealCrystal();
        else 
            ChasePlayer();

        healthBar.value = health;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void Alert()
    {
        alert.gameObject.SetActive(true);
        DOTween.Sequence(alert.DOFade(1.0f, 0.3f)).Append(alert.DOFade(0.4f, 0.3f)).SetLoops(4).OnComplete(() =>
        {
            alert.gameObject.SetActive(false);
        }).Play();
    }

    private void StealCrystal()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);
        
        RotateToPlayer();
        
        int score = GameManager.gm.currentScore >= damage ? damage : GameManager.gm.currentScore;
        GameManager.gm.currentScore -= score;
        ShowCenterTip(failTip, score.ToString(), "crystals");
        DestroyEnemy();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            GameManager.gm.currentScore += value;
            ShowCenterTip(successTip, value.ToString(), "crystals");
            DestroyEnemy();
        }
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject, 0.5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, stealRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, alertRange);
    }

    private void ShowCenterTip(string text1, string text2, string text3)
    {
        centerTip.SetActive(true);
        line1.text = text1;
        line2.text = text2;
        line3.text = text3;
        DOTween.Sequence().Append(centerTip.transform.DOScale(Vector3.one, 0.6f))
            .Append(centerTip.transform.DOShakePosition(1.5f, 10f, 8, 50))
            .Append(centerTip.transform.DOScale(Vector3.zero, 0.6f)).OnComplete(
                () =>
                {
                    centerTip.SetActive(false);
                });
    }
}