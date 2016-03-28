using UnityEngine;
using System.Collections;

public class PlayerGeometry : MonoBehaviour
{
	private Player player;

	static readonly Mesh mesh;
	static PlayerGeometry()
	{
		mesh = new Mesh{ subMeshCount = 2 };
		mesh.vertices = new Vector3[] {
			new Vector3(0, 0.5f),
			new Vector3(0.5f, -0.4f),
			new Vector3(0, -0.5f),
			new Vector3(-0.5f, -0.4f)
		};
		mesh.SetIndices(new int[] { 0, 1, 2, 3, 0 }, MeshTopology.LineStrip, 0);
		mesh.SetIndices(new int[] { 1, 2, 0, 2, 3, 0 }, MeshTopology.Triangles, 1);
		mesh.Optimize();
	}

	void Start()
	{
		GetComponent<MeshFilter>().mesh = mesh;
	}
}
