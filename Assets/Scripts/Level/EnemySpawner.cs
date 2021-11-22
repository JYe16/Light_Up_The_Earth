using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
	public Transform[] spawnerPoints;
	public GameObject[] enemyShipPrefabs;
	public float spawnInterval;
	public float startWaitTime = 15f;
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
		yield return new WaitForSeconds(startWaitTime);
		while (total++ < spawnNum)
		{
			Transform center = spawnerPoints[Random.Range(0, spawnerPoints.Length)];
			float spawnRadius = center.gameObject.GetComponent<SpawnerPoint>().spwanRadius;
			Vector3 randomPos = CircleAreaPos(spawnRadius, center.position);
			randomPos.y = center.position.y;
			GameObject enemy = Instantiate(enemyShipPrefabs[Random.Range(0, enemyShipPrefabs.Length)], randomPos,
				Quaternion.identity);
			// assign properties
			EnemyShip enemyShip = enemy.GetComponent<EnemyShip>();
			enemyShip.player = player;
			enemyShip.centerTip = centerTip;
			enemyShip.alert = alert;
			yield return new WaitForSeconds(spawnInterval);
		}
	}

	private Vector3 CircleAreaPos(float radius, Vector3 centerPos)
	{
		return Random.insideUnitSphere * radius + centerPos;
	}
}