using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class BadGuyBackAndForthMove : MonoBehaviour {

	public float maxSpeed = 5f;
	private FacingDirection facing;
	private Rigidbody2D body;

	// Use this for initialization
	void Start () {
		facing = FacingDirection.Right;
		body = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (body.velocity.x == 0) {
			flip();
		}

		var speed = maxSpeed * (int)facing;
		body.velocity = new Vector2 (speed, body.velocity.y);
	}

	void flip(){
		if (facing == FacingDirection.Right) {
			facing = FacingDirection.Left;
		} else if (facing == FacingDirection.Left) {
			facing = FacingDirection.Right;
		}

		var theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
