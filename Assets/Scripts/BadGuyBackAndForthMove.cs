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
	void Update () {
		if (body.velocity.x == 0) {
			flipVelocity();
		}
		body.velocity = new Vector2 (maxSpeed, body.velocity.y);
	}

	void flipVelocity(){
		maxSpeed = maxSpeed * -1;
	}
}
