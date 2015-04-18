using UnityEngine;
using System.Collections;

public class BadGuyBackAndForthMove : MonoBehaviour {

	public float maxSpeed = 5f;
	private bool facingRight;
	private Rigidbody2D body;

	// Use this for initialization
	void Start () {
		facingRight = true;
		body = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (body.velocity.x == 0) {
			flip();
		}
		body.velocity = new Vector2 (maxSpeed, body.velocity.y);
	}

	void flip(){
		facingRight = !facingRight;
		flipVelocity ();
	}

	void flipVelocity(){
		maxSpeed = maxSpeed * -1;
	}
}
