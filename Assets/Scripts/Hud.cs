using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Diagnostics;

public class Hud : MonoBehaviour
{
	public Level level;
	public Text stats;
	public Text timer;

	private Player player;
	private Stopwatch stopwatch;

	void Start()
	{
		player = level.player;
		player.Changed += UpdatePlayer;
		UpdatePlayer(player, null);
		stopwatch = Stopwatch.StartNew();
	}

	void UpdatePlayer(object sender, EventArgs e)
	{
		var xpReq = player.nextLevelXp == -1
			? " (max)"
			: "/" + player.nextLevelXp;
		stats.text = string.Format("Level: {0}\nXP: {1}{2}\nHealth: {3}", player.level, player.xp, xpReq, player.health);
		if(player.nextLevelXp == -1)
			stopwatch.Stop();
	}

	void Update()
	{
		timer.text = stopwatch.Elapsed.ToString();
	}

	void OnDestroy()
	{
		player.Changed -= UpdatePlayer;
	}
}
