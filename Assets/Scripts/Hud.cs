using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.ComponentModel;

public class Hud : MonoBehaviour
{
	public Level level;

	private Text text;
	private Player player;

	void Start()
	{
		text = transform.Find("Text").GetComponent<Text>();
		player = level.player;

		player.PropertyChanged += updatePlayer;
		updatePlayer(player, null);
	}

	void updatePlayer(object sender, PropertyChangedEventArgs e)
	{
		text.text = string.Format("Level: {0}\nXP: {1}/{2}\nHealth: {3}", player.level, player.xp, player.nextLevelXp, player.health);
	}

	void OnDestroy()
	{
		player.PropertyChanged -= updatePlayer;
	}
}
