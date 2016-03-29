using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class Hud : MonoBehaviour
{
	public Level level;

	private Text text;
	private Player player;

	void Start()
	{
		text = transform.Find("Text").GetComponent<Text>();
		player = level.player;

		player.Changed += UpdatePlayer;
		UpdatePlayer(player, null);
	}

	void UpdatePlayer(object sender, EventArgs e)
	{
		var xpReq = player.nextLevelXp == -1
			? " (max)"
			: "/" + player.nextLevelXp;
		text.text = string.Format("Level: {0}\nXP: {1}{2}\nHealth: {3}", player.level, player.xp, xpReq, player.health);
	}

	void OnDestroy()
	{
		player.Changed -= UpdatePlayer;
	}
}
