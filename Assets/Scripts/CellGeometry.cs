using UnityEngine;
using System.Collections;
using System.Linq;

public class CellGeometry : MonoBehaviour
{
	static readonly Mesh mesh;

	static CellGeometry()
	{
		Vector3[] verts = new Vector3[12];
		int[] lineIndices = new int[36];
		int[] faceIndices = new int[12];

		for (int i = 0; i < 6; i++) {
			float angle = Mathf.PI / 3.0f * i;
			var vert = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), -0.5f);
			// bottom
			verts[i] = vert;
			lineIndices[2 * i] = i;
			lineIndices[2 * i + 1] = (i + 1) % 6;
			// top
			verts[i + 6] = new Vector3(vert.x, vert.y, 0.0f);
			lineIndices[2 * i + 12] = i + 6;
			lineIndices[2 * i + 13] = (i + 1) % 6 + 6;
			// verticals
			lineIndices[2 * i + 24] = i;
			lineIndices[2 * i + 25] = i + 6;
		}
		faceIndices = new int[] {
			// top
			2, 1, 0,
			3, 2, 0,
			3, 0, 5,
			4, 3, 5,
			// sides
			1, 6, 0,
			7, 6, 1,

			2, 7, 1,
			8, 7, 2,

			3, 8, 2,
			9, 8, 3,

			4, 9, 3,
			10, 9, 4,

			5, 10, 4,
			11, 10, 5,

			0, 11, 5,
			6, 11, 0
		};

		mesh = new Mesh { vertices = verts, subMeshCount = 2 };
		mesh.SetIndices(lineIndices, MeshTopology.Lines, 0);
		mesh.SetIndices(faceIndices, MeshTopology.Triangles, 1);
		mesh.Optimize();
	}

	void Start()
	{
		GetComponent<MeshFilter>().mesh = mesh;
	}
}
