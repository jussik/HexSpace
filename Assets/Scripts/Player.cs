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
		switch (level) {
		case 1:
			SetTurrets(new Vector3(0.0f, -0.25f, -0.1f));
			break;
		case 2:
			SetTurrets(
				new Vector3(-0.2f, -0.15f, -0.1f),
				new Vector3(0.2f, -0.15f, -0.1f));
			break;
		case 3:
			SetTurrets(
				new Vector3(-0.2f, -0.15f, -0.1f),
				new Vector3(0.2f, -0.15f, -0.1f),
				new Vector3(0.0f, 0.2f, -0.1f));
			break;
		case 4:
			SetTurrets(new Vector3(0.0f, -0.25f, -0.1f));
			break;
		case 5:
			SetTurrets(
				new Vector3(0.0f, -0.2f, -0.1f),
				new Vector3(0.0f, 0.0f, -0.1f),
				new Vector3(0.0f, 0.2f, -0.1f));
			break;
		}
	}
	private void SetTurrets(params Vector3[] coords)
	{
		int requiredTurrets = coords.Length - turrets.Count;
		Debug.Log(requiredTurrets);
		if (requiredTurrets > 0) {
			for (var i = 0; i < requiredTurrets; i++) {
				var turret = Instantiate(turretPrefab).GetComponent<TurretGeometry>();
				turret.transform.SetParent(transform);
				turrets.Add(turret);
			}
		} else if (requiredTurrets < 0) {
			for (var i = turrets.Count + requiredTurrets; i < turrets.Count; i++) {
				turrets[i].gameObject.SetActive(false);
			}
		}
		for (var i = 0; i < coords.Length; i++) {
			var turret = turrets[i];
			turret.gameObject.SetActive(true);
			turret.transform.localPosition = coords[i];
			// TODO: something sometimes changes the rotation 90deg
			// ensure we're still pointing where we should
			turret.transform.rotation = Quaternion.identity;
		}
	}

	void NotifyChanged()
	{
		if (Changed != null) {
			Changed(this, EventArgs.Empty);
		}
	}
}