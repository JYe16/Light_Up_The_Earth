using System;
using System.Collections;
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
	
	private int total = 0;	// total generated enemy ships

	private void Start()
	{
		StartCoroutine(InstantiateEnemy());
	}

	IEnumerator InstantiateEnemy()
	{
		yield return new WaitForSeconds(Random.Range(minStartWaitTime, maxStartWaitTIme));
		while (total++ < spawnNum)
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

	private Vector3 CircleAreaPos(float radius, Vector3 centerPos)
	{
		return Random.insideUnitSphere * radius + centerPos;
	}
}