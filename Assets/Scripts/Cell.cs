using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class Cell : MonoBehaviour
{
	public Level level;
	public int id;
	public int x;
	public int y;
	public List<Cell> adjacents = new List<Cell>();
	public bool isRevealed;
	public int enemy;
	public int threat;

	public void AddAdjacent(Cell adj)
	{
		adjacents.Add(adj);
		adj.adjacents.Add(this);
	}

	public void UpdateThreat()
	{
		if (enemy > 0) {
			threat = enemy;
		} else {
			threat = adjacents.Sum(c => c.enemy);
		}
		UpdateUI();
	}

	public void Reveal(bool hideIfPossible = false)
	{
		if (isRevealed)
			return;

		isRevealed = true;
		if (hideIfPossible && threat == 0) {
			gameObject.SetActive(false);
		} else {
			UpdateUI();
		}
		if (threat == 0) {
			for (var i = 0; i < adjacents.Count; i++) {
				adjacents[i].Reveal(hideIfPossible);
			}
		}

		if (enemy > 0) {
			level.player.Battle(enemy);
		}
	}

	void UpdateUI()
	{
		var text = GetComponentInChildren<Text>();
		if (enemy > 0)
			text.color = Color.red;
		if (isRevealed) {
			var pos = transform.position;
			pos.z = 0.5f;
			transform.position = pos;
			GetComponent<Collider2D>().enabled = false;
			text.text = threat == 0 ? string.Empty : threat.ToString();
		}
		GetComponent<MeshRenderer>().materials[1].color = isRevealed ? new Color(0.2f, 0.2f, 0.2f) : new Color(0.25f, 0.25f, 0.25f);
	}

	void OnMouseOver()
	{
		if (Input.GetButton("Fire1"))
			Reveal();
	}
}
