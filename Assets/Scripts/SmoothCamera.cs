using UnityEngine;
using System.Collections;

public class SmoothCamera : MonoBehaviour
{
	public GameObject target;
	public Vector3 positionOffset = new Vector3(0, 0, -10);

	private Vector3 velocity;

	void Start()
	{
		transform.rotation = Quaternion.LookRotation(-positionOffset);
	}

	void FixedUpdate()
	{
		transform.position = Vector3.SmoothDamp(transform.position, target.transform.position + positionOffset, ref velocity, 0.25f);
		//transform.rotation = Quaternion.LookRotation(target.transform.position-transform.position-positionOffset);
	}
}
