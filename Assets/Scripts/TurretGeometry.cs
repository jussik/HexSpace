using UnityEngine;
using System.Collections;

public class TurretGeometry : MonoBehaviour
{
	public int level;
	public LaserGeometry laser;

	public GameObject laserPrefab;

	private Plane groundPlane;

	static Mesh mesh;
	static Mesh GetMesh()
	{
		if (mesh == null)
		{
			mesh = new Mesh
			{
				vertices = new[]
				{
					new Vector3(0.1f, 0.1f),
					new Vector3(0.1f, -0.1f),
					new Vector3(-0.1f, -0.1f),
					new Vector3(-0.1f, 0.1f),
					new Vector3(0.0f, 0.0f),
					new Vector3(0.0f, 0.3f)
				}
			};
			mesh.SetIndices(new[]
			{
				0, 1,
				1, 2,
				2, 3,
				3, 0,
				4, 5
			}, MeshTopology.Lines, 0);
			mesh.Optimize();
		}
		return mesh;
	}

	private float laserZoffset = 0.01f;
	void Start()
	{
		GetComponent<MeshFilter>().mesh = GetMesh();

		laser = Instantiate(laserPrefab).GetComponent<LaserGeometry>();
		laser.gameObject.SetActive(false);
		laser.transform.SetParent(transform);
		laser.transform.localPosition = new Vector3(0.0f, 0.0f, laserZoffset);

		groundPlane = new Plane(Vector3.forward, laser.transform.position.z);
	}

	void Update()
	{
		Vector3 mouse = Input.mousePosition;
		mouse.z = -groundPlane.GetDistanceToPoint(Camera.main.transform.position);
		var relpos = Camera.main.ScreenToWorldPoint(mouse);

		transform.rotation = Quaternion.LookRotation(Vector3.forward, relpos - transform.position);

		if (Input.GetButton("Fire1")) {
			laser.SetTarget(relpos);
			laser.gameObject.SetActive(true);
		} else {
			laser.gameObject.SetActive(false);
		}
	}
}
