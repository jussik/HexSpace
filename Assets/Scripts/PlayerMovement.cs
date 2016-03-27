using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
	public float speed = 250.0f;
	public float rotSpeed = 500.0f;

	private Vector2 movement = Vector2.zero;
	private Rigidbody2D body;

	void Start()
	{
		body = GetComponent<Rigidbody2D>();
		body.freezeRotation = true;	
	}

	void Update()
	{
		movement.x = Input.GetAxis("Horizontal");
		movement.y = Input.GetAxis("Vertical");
		if (movement.sqrMagnitude > 1)
			movement.Normalize();
	}

	void FixedUpdate()
	{
		if (movement.sqrMagnitude > 0.01f) {
			// look smoothly towards requested vector
			transform.rotation = Quaternion.RotateTowards(
				transform.rotation,
				Quaternion.LookRotation(Vector3.forward, movement),
				movement.sqrMagnitude * rotSpeed * Time.deltaTime);
			// movement is half way between direct input and based on rotation
			body.velocity = Time.deltaTime * speed * Vector3.Lerp(movement.sqrMagnitude * transform.up, movement, 0.5f);
		} else {
			body.velocity = Vector2.zero;
		}
	}
}
