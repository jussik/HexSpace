using UnityEngine;
using System.Collections;

public class PlayerGeometry : MonoBehaviour {
	void Start () {
		var mesh = new Mesh();
		mesh.vertices = new Vector3[] {
			new Vector3(0, 0.5f),
			new Vector3(0.5f, -0.4f),
			new Vector3(0, -0.5f),
			new Vector3(-0.5f, -0.4f)
		};
		mesh.SetIndices(new int[] { 0, 1, 2, 3, 0 }, MeshTopology.LineStrip, 0);

		GetComponent<MeshFilter>().mesh = mesh;
	}
}
