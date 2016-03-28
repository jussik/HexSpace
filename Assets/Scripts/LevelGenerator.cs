﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelGenerator : MonoBehaviour
{
	public GameObject cellPrefab;
	public string seed;

	private const int rows = 25;
	private const int columns = 15;
	private int nextId = 0;
	private Level level;

	void Start()
	{
		if (!string.IsNullOrEmpty(seed))
			Random.seed = seed.GetHashCode();
		
		level = GetComponent<Level>();
		Cell[,] cells = new Cell[rows, columns];
		for (var i = 0; i < rows; i++) {
			for (var j = 0; j < columns; j++) {
				cells[i, j] = Spawn(i, j);
			}
		}
		for (var i = 0; i < rows; i++) {
			for (var j = 0; j < columns; j++) {
				var cell = cells[i, j];
				if (j + 1 < columns)
					cell.AddAdjacent(cells[i, j + 1]);
				if (i + 1 < rows) {
					int jx = j + (i % 2);
					if (jx < columns)
						cell.AddAdjacent(cells[i + 1, jx]);
					if (jx > 0)
						cell.AddAdjacent(cells[i + 1, jx - 1]);
				}
			}
		}
		for (var i = 0; i < rows; i++) {
			for (var j = 0; j < columns; j++) {
				var cell = cells[i, j];
				cell.UpdateThreat();
			}
		}
		for (var i = 0; i < rows; i++) {
			cells[i, 0].Reveal(true);
			cells[i, columns - 1].Reveal(true);
		}
		for (var j = 0; j < columns; j++) {
			cells[0, j].Reveal(true);
			cells[rows - 1, j].Reveal(true);
		}
		level.cells = cells;
	}

	Cell Spawn(int i, int j)
	{
		float h = Mathf.Sqrt(3.0f);
		float x = i * 1.5f;
		float y = j * h + ((i % 2) * h / 2.0f);
		var obj = (GameObject)Instantiate(cellPrefab, new Vector3(x, y), Quaternion.identity);
		obj.name = "Cell " + i + "," + j;
		//obj.GetComponentInChildren<Text>().text = i + "," + j;
		var cell = obj.GetComponent<Cell>();
		cell.id = ++nextId;
		cell.level = level;
		cell.x = i;
		cell.y = j;
		if (i > 0 && i < rows - 1 && j > 0 && j < columns - 1) {
			// do not create enemies on edges
			cell.enemy = (int)(Mathf.Pow(Random.Range(0.0f, 1.5f), 4.0f));
		}
		return cell;
	}
}
