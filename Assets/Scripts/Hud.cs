using UnityEngine;
using UnityEngine.UI;
using System;
using System.Diagnostics;

public class Hud : MonoBehaviour
{
	public Level level;
	public Text stats;
	public Text timer;

	private Player player;
	private Stopwatch stopwatch;
	private bool started;

	void Start()
	{
		player = level.player;
		player.Changed += UpdatePlayer;
		UpdatePlayer(player, null);
		stopwatch = new Stopwatch();
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
		if (!started)
		{
			if (Input.GetButton("Fire1")
				|| Input.GetAxisRaw("Horizontal") != 0.0f
				|| Input.GetAxisRaw("Vertical") != 0.0f)
			{
				started = true;
				stopwatch.Start();
			}
		}
		timer.text = stopwatch.Elapsed.ToString();
	}

	void OnDestroy()
	{
		player.Changed -= UpdatePlayer;
	}
}
