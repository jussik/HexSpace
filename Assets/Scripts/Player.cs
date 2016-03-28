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

	private List<TurretGeometry> turrets = new List<TurretGeometry>();

	void Start()
	{
		health = 10;
		CheckLevel();
		NotifyChanged();
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
			UpdateTurrets();
			if (level <= levelXps.Length)
				nextLevelXp = levelXps[level - 1];
			else
				nextLevelXp = 99999;
		}
	}

	private void UpdateTurrets()
	{
		// NOTE: only works if called after each level increment, level must never be decremented
		switch (level) {
		case 1:
			var turret = AddTurret();
			turret.transform.localPosition = new Vector3(0.0f, -0.25f, -0.1f);
			break;
		case 2:
			turrets[0].transform.localPosition = new Vector3(-0.2f, -0.15f, -0.1f);
			var turret2 = AddTurret();
			turret2.transform.localPosition = new Vector3(0.2f, -0.15f, -0.1f);
			break;
		case 3:
			var turret3 = AddTurret();
			turret3.transform.localPosition = new Vector3(0.0f, 0.2f, -0.1f);
			break;
		}
	}
	private TurretGeometry AddTurret()
	{
		var turret = Instantiate(turretPrefab).GetComponent<TurretGeometry>();
		turret.transform.SetParent(transform);
		turrets.Add(turret);
		return turret;
	}

	void NotifyChanged()
	{
		if (Changed != null) {
			Changed(this, EventArgs.Empty);
		}
	}
}