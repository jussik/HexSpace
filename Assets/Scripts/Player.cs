using UnityEngine;
using System.Collections;
using System;

public class Player : MonoBehaviour
{
	public int level { get; private set; }
	public int xp { get; private set; }
	public int nextLevelXp { get; private set; }
	public int health { get; private set; }

	public event EventHandler Changed;

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
		}
		NotifyChanged();
	}

	void CheckLevel()
	{
		while (xp >= nextLevelXp) {
			level++;
			nextLevelXp = (int)(Mathf.Pow(level, 2.7f) * 5.0f) + 5;
		}
	}

	void NotifyChanged()
	{
		if (Changed != null) {
			Changed(this, EventArgs.Empty);
		}
	}
}