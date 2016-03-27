using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class Cell : MonoBehaviour
{
	public int x;
	public int y;
	public List<Cell> adjacents = new List<Cell>();
	public bool isRemoved;
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

	public void Reveal(bool remove = false)
	{
		if (isRevealed)
			return;

		isRevealed = true;
		if (remove && threat == 0) {
			GetComponent<MeshRenderer>().enabled = false;
			GetComponent<Collider2D>().enabled = false;
		} else {
			UpdateUI();
		}
		if (threat == 0) {
			for (var i = 0; i < adjacents.Count; i++) {
				adjacents[i].Reveal(remove);
			}
		}
	}

	void UpdateUI()
	{
		var text = GetComponentInChildren<Text>();
		if (enemy > 0)
			text.color = Color.red;
		if (isRevealed) {
			var pos = transform.position;
			pos.z = 1.0f;
			transform.position = pos;
			GetComponent<Collider2D>().enabled = false;
			text.text = threat == 0 ? string.Empty : threat.ToString();
		}
	}

	void OnMouseDown()
	{
		if (enemy > 0) {
			enemy = 0;
		}
		Reveal();
	}
}
