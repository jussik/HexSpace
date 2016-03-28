using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
	public int level { get; private set; }
	public int xp { get; private set; }
	public int nextLevelXp { get; private set; }
	public int health { get; private set; }
	public int[] levelXps;

	public event EventHandler Changed;

	public GameObject laserPrefab;

	private LaserGeometry laser;
	private Plane groundPlane;

	void Start()
	{
		health = 10;
		CheckLevel();
		NotifyChanged();

		groundPlane = new Plane(Vector3.forward, transform.position.z);

		laser = Instantiate(laserPrefab).GetComponent<LaserGeometry>();
		laser.transform.SetParent(transform);
		laser.gameObject.SetActive(false);
	}

	void Update()
	{
		if (Input.GetButton("Fire1")) {
			Vector3 mouse = Input.mousePosition;
			mouse.z = -groundPlane.GetDistanceToPoint(Camera.main.transform.position);
			laser.SetTarget(Camera.main.ScreenToWorldPoint(mouse));
			laser.gameObject.SetActive(true);
		} else {
			laser.gameObject.SetActive(false);
		}
	}

	public void Battle(int enemyLevel)
	{
		int enemyHealth = enemyLevel;
		while (enemyHealth > 0 && health > 0) {
			enemyHealth -= level;
			if (enemyHealth > 0) {
				health -= enemyLevel;
			}
		}
		if (health > 0) {
			xp += enemyLevel;
			CheckLevel();
		} else {
			SceneManager.LoadScene(0);
		}
		NotifyChanged();
	}
		
	public void CheckLevel()
	{
		while (xp >= nextLevelXp) {
			level++;
			if (level <= levelXps.Length)
				nextLevelXp = levelXps[level - 1];
			else
				nextLevelXp = 99999;
		}
	}

	void NotifyChanged()
	{
		if (Changed != null) {
			Changed(this, EventArgs.Empty);
		}
	}
}