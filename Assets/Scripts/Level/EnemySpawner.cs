using System;
using System.Collections;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
	public Canvas gloabalCamCanvas;
	public Camera globalCamera;
	public GameObject markDotPrefab;
	public Transform[] spawnerPoints;
	public GameObject[] enemyShipPrefabs;
	public float spawnInterval;
	public float maxStartWaitTIme;
	public float minStartWaitTime;
	public int spawnNum;
	public Transform player;
	public GameObject centerTip;
	public Image alert;
	public GameObject enemyInfo;
	public GameObject[] infoList;
	public Sprite[] enemyIcon;
	
	private int total = 0;	// total generated enemy ships

	private void Start()
	{
		StartCoroutine(InstantiateEnemy());
	}

	private void Update()
	{
		if (enemyInfo.transform.childCount == 0)
		{
			DisplayInfoList(false);
		}
	}

	IEnumerator InstantiateEnemy()
	{
		yield return new WaitForSeconds(Random.Range(minStartWaitTime, maxStartWaitTIme));
		while (total < spawnNum)
		{
			Transform center = spawnerPoints[Random.Range(0, spawnerPoints.Length)];
			float spawnRadius = center.gameObject.GetComponent<SpawnerPoint>().spwanRadius;
			Vector3 randomPos = CircleAreaPos(spawnRadius, center.position);
			randomPos.y = center.position.y;
			GameObject enemy = Instantiate(enemyShipPrefabs[Random.Range(0, enemyShipPrefabs.Length)], randomPos,
				Quaternion.identity);
			EnemyShip enemyShip = enemy.GetComponent<EnemyShip>();
			// assign properties
			SetProperties(enemyShip);
			if (total == 0)
			{
				DisplayInfoList(true);
			}

			SetInfoItem(enemyShip);
			total++;
			yield return new WaitForSeconds(spawnInterval);
		}
	}

	private void SetProperties(EnemyShip enemyShip)
	{
		GameObject dot = Instantiate(markDotPrefab, gloabalCamCanvas.transform);
		enemyShip.markDot = dot;
		enemyShip.gloabalCamCanvas = gloabalCamCanvas;
		enemyShip.globalCamera = globalCamera;
		enemyShip.player = player;
		enemyShip.centerTip = centerTip;
		enemyShip.alert = alert;
	}

	private void DisplayInfoList(bool show)
	{
		enemyInfo.GetComponentInParent<CanvasGroup>().DOFade(show ? 1 : 0, 0.2f);
	}

	private void SetInfoItem(EnemyShip enemyShip)
	{
		Image icon = infoList[total].transform.GetChild(0).GetComponent<Image>();
		icon.sprite = enemyIcon[Random.Range(0, enemyIcon.Length)];
		Text no = infoList[total].transform.GetChild(1).GetComponent<Text>();
		Text score = infoList[total].transform.GetChild(2).GetComponent<Text>();
		no.text = (total + 1) + "/";
		score.text = "-" + enemyShip.damage + "\n+" + enemyShip.crystals;
		infoList[total].SetActive(true);
		enemyShip.infoItem = infoList[total];
	}

	private Vector3 CircleAreaPos(float radius, Vector3 centerPos)
	{
		return Random.insideUnitSphere * radius + centerPos;
	}
}