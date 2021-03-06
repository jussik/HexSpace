﻿using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
	public GameObject cellPrefab;
	public string seed;

	private const int rows = 25;
	private const int columns = 15;
	private Level level;

	void Start()
	{
		if (!string.IsNullOrEmpty(seed))
			Random.InitState(seed.GetHashCode());

		level = GetComponent<Level>();

		int[] enemyCounts = {
			33,
			27,
			20,
			13,
			6
		};
		int[] levelXps = {
			15,
			60,
			160,
			271,
			367
		};
		level.player.levelXps = levelXps;
		level.player.CheckLevel();

		// spawn cells
		Cell[,] cells = new Cell[rows, columns];
		Cell[] innerCells = new Cell[(rows - 2) * (columns - 2)];
		int ic = 0;
		int enemyLvl = 0;
		for (var i = 0; i < rows; i++) {
			for (var j = 0; j < columns; j++) {
				var cell = Spawn(i, j);
				cells[i, j] = cell;
				// store non-border cells in another array, used in shuffling of enemies
				if (i > 0 && i < rows - 1 && j > 0 && j < columns - 1) {
					innerCells[ic++] = cell;
					if (enemyLvl < enemyCounts.Length) {
						// add enemies iteratively
						var enemiesLeftInLevel = --enemyCounts[enemyLvl];
						cell.enemy = enemyLvl + 1;
						if (enemiesLeftInLevel == 0) {
							enemyLvl++;
						}
					}
				}
			}
		}
		// Fisher-Yates shuffle to randomise enemies
		for (int n = innerCells.Length - 1; n >= 1; n--) {
			int k = Random.Range(0, n + 1);
			int swp = innerCells[k].enemy;
			innerCells[k].enemy = innerCells[n].enemy;
			innerCells[n].enemy = swp;
		}
		// link adjacents
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
		// update threat values
		for (var i = 0; i < rows; i++) {
			for (var j = 0; j < columns; j++) {
				var cell = cells[i, j];
				cell.UpdateThreat();
			}
		}
		// reveal edges
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
		obj.transform.SetParent(transform);
		obj.name = "Cell " + i + "," + j;
		//obj.GetComponentInChildren<Text>().text = i + "," + j;
		var cell = obj.GetComponent<Cell>();
		cell.level = level;
	    return cell;
	}
}
