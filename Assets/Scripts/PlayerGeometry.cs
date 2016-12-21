using UnityEngine;

public class PlayerGeometry : MonoBehaviour
{
	static Mesh mesh;
	static Mesh GetMesh()
	{
	    if (mesh == null)
	    {
	        mesh = new Mesh
	        {
	            subMeshCount = 2,
	            vertices = new[]
	            {
	                new Vector3(0, 0.5f),
	                new Vector3(0.5f, -0.4f),
	                new Vector3(0, -0.5f),
	                new Vector3(-0.5f, -0.4f)
	            }
	        };
	        mesh.SetIndices(new[] {0, 1, 2, 3, 0}, MeshTopology.LineStrip, 0);
	        mesh.SetIndices(new[] {1, 2, 0, 2, 3, 0}, MeshTopology.Triangles, 1);
	        ;
	    }
	    return mesh;
	}

	void Start()
	{
		GetComponent<MeshFilter>().mesh = GetMesh();
	}
}
