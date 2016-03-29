using UnityEngine;
using System.Collections;

public class LaserGeometry : MonoBehaviour
{
	private Mesh mesh;
	private Vector3[] verts = { Vector3.zero, Vector3.zero };

	public void SetTarget(Vector3 position)
	{
		verts[1] = transform.parent.InverseTransformPoint(position);
		verts[1].z = 0;
		if (mesh != null)
			mesh.vertices = verts; // trigger upload
	}

	private readonly int[] indices = { 0, 1 };
	void Start()
	{
		mesh = new Mesh();
		mesh.MarkDynamic();
		mesh.vertices = verts;
		mesh.SetIndices(indices, MeshTopology.Lines, 0);

		GetComponent<MeshRenderer>().material.color = Color.red;
		GetComponent<MeshFilter>().mesh = mesh;
	}
}
