using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelGenerator : MonoBehaviour
{
	public GameObject cellPrefab;

	private int rows = 25;
	private int columns = 15;

	void Start()
	{
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
			cells[i, 0].Reveal();
			cells[i, columns - 1].Reveal();
		}
		for (var j = 0; j < columns; j++) {
			cells[0, j].Reveal();
			cells[rows - 1, j].Reveal();
		}
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
		cell.x = i;
		cell.y = j;
		if (i > 0 && i < rows - 1 && j > 0 && j < columns - 1) {
			// do not create enemies on edges
			cell.enemy = (int)(Mathf.Pow(Random.Range(0.0f, 1.5f), 4.0f));
		}
		return cell;
	}
}
