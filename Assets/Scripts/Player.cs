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

	public GameObject turretPrefab;

	void Start()
	{
		health = 10;
		CheckLevel();
		NotifyChanged();

		var turret = Instantiate(turretPrefab);
		turret.transform.SetParent(transform);
		var relpos = new Vector3(0.0f, -0.25f, -0.1f);
		turret.transform.localPosition = relpos;
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