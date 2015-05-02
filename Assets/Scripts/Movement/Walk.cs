using UnityEngine;
using System.Collections;

/**
 * Simple platformer left & right walking movement. Allows air control,
 * since it doesn't check if the body is grounded.
 */
public class Walk : MonoBehaviour {

	public float maxSpeed = 3.0f;
	protected float move;
	protected Rigidbody2D body;

	void Start () {
		body = GetComponent<Rigidbody2D> ();
	}

	public void walkLeft() {
		move = -1.0f;
	}

	public void walkRight() {
		move = 1.0f;
	}

	public void walkAnalog(float xVectorPercentage) {
		if (xVectorPercentage > 1.0f) {
			move = 1.0f;
		} else if (xVectorPercentage < -1.0f) {
			move = -1.0f;
		} else {
			move = xVectorPercentage;
		}
	}

	void Update () {
	}

	void FixedUpdate() {
		body.velocity = new Vector2 (move * maxSpeed, body.velocity.y);
	}
}
