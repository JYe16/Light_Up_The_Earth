using System;
using System.Collections;
using UnityEngine;

public class FireShoot : MonoBehaviour
{
	public int damage;
	public Transform shootPosition;
	public float shootingRange;
	public GameObject fireLaserPrefab;
	
	private LineRenderer lr;
	private GameObject fireLaser;
	private Camera mainCam;

	private void Start()
	{
		mainCam = Camera.main;
		
		fireLaser = Instantiate(fireLaserPrefab, shootPosition);
		lr = fireLaser.GetComponent<LineRenderer>();
		DisableLaser();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Shoot();
		}
	}

	private void Shoot()
	{
		Vector2 middlePos = new Vector2(Screen.width / 2, Screen.height / 2);
		Ray ray = mainCam.ScreenPointToRay(middlePos);
		RaycastHit hitInfo;
		// display fire laser
		Vector3 startPos = shootPosition.transform.position;
		Vector3 endPos = startPos + ray.direction.normalized * shootingRange;
		StartCoroutine(DisplayFireLaser(startPos, endPos));
		if (Physics.Raycast(ray, out hitInfo, shootingRange))
		{
			if (hitInfo.collider.gameObject.tag.Equals("Enemy"))
			{
				EnemyShip enemyShip = hitInfo.collider.gameObject.GetComponent<EnemyShip>();
				if (enemyShip.health > 0)
				{
					enemyShip.TakeDamage(damage);
				}
			}
		}
	}

	IEnumerator DisplayFireLaser(Vector3 startPos, Vector3 endPos)
	{
		EnableLaser();
		lr.SetPosition(0, startPos);
		lr.SetPosition(1, endPos);
		fireLaser.transform.position = startPos;
		yield return new WaitForSeconds(0.5f);
		DisableLaser();
	}

	private void DisableLaser()
	{
		fireLaser.SetActive(false);
	}

	private void EnableLaser()
	{
		fireLaser.SetActive(true);
	}

}