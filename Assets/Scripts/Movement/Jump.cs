using UnityEngine;
using System.Collections;

public class Jump : MonoBehaviour {

	public float jumpForce = 250f;
	protected Rigidbody2D body;
	protected GroundSensor groundSensor;
	protected bool shouldJump = false;

	void Start () {
		body = GetComponent<Rigidbody2D> ();
		groundSensor = GetComponent<GroundSensor> ();
	}
	
	public void jump() {
		if (groundSensor.isGrounded()) {
			shouldJump = true;
		}
	}

	void Update () {
	}

	void FixedUpdate() {
		if (shouldJump) {
			body.AddForce (new Vector2 (0, jumpForce));
			shouldJump = false;
		}
	}
}
