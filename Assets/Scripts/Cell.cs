using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Cell : MonoBehaviour
{
	public Level level;
	public readonly List<Cell> adjacents = new List<Cell>();
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
		threat = adjacents.Sum(c => c.enemy) + enemy;
		UpdateModel();
	}

	public void Reveal(bool hideIfPossible = false)
	{
		if (isRevealed)
			return;

		isRevealed = true;
		if (hideIfPossible && threat == 0) {
			gameObject.SetActive(false);
		} else {
			UpdateModel();
		}
		if (threat == 0) {
			foreach (var cell in adjacents)
			{
				cell.Reveal(hideIfPossible);
			}
		}

		if (enemy > 0) {
			level.player.Battle(enemy);
		}
	}

	void UpdateModel()
	{
		GetComponent<MeshRenderer>().materials[1].color = isRevealed ? new Color(0.2f, 0.2f, 0.2f) : new Color(0.25f, 0.25f, 0.25f);
		if (!isRevealed)
			return;
		{
			var pos = transform.position;
			pos.z = 0.5f;
			transform.position = pos;
			GetComponent<Collider2D>().enabled = false;

			var texts = GetComponentsInChildren<TextMesh>();
			var text = texts.First(t => t.name == "Number");
			text.text = threat == 0 ? string.Empty : threat.ToString();
			if (enemy > 0)
				texts.First(t => t.name == "Enemy").text = enemy.ToString();
		}
	}

	void OnMouseOver()
	{
		if (Input.GetButton("Fire1"))
			Reveal();
	}
}
