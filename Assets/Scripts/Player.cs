using UnityEngine;
using System.Collections;
using System.ComponentModel;

public class Player : MonoBehaviour, INotifyPropertyChanged
{
	int _level;
	public int level {
		get { return _level; }
		set {
			if (_level != value) {
				_level = value;
				NotifyPropertyChanged("level");
			}
		}
	}

	int _xp;
	public int xp {
		get { return _xp; }
		set {
			if (_xp != value) {
				_xp = value;
				CheckLevel();
				NotifyPropertyChanged("xp");
			}
		}
	}
	public int nextLevelXp;

	int _health = 10;
	public int health {
		get { return _health; }
		set {
			if (_health != value) {
				_health = value;
				NotifyPropertyChanged("health");
			}
		}
	}

	void Start()
	{
		CheckLevel();
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
		}
	}

	void CheckLevel()
	{
		while (xp >= nextLevelXp) {
			level++;
			nextLevelXp = (int)(Mathf.Pow(level, 2.7f) * 5.0f) + 5;
			NotifyPropertyChanged("nextLevelXp");
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;
	void NotifyPropertyChanged(string info)
	{
		if (PropertyChanged != null) {
			PropertyChanged(this, new PropertyChangedEventArgs(info));
		}
	}
}