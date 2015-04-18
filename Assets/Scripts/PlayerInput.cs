using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {
	
	public float maxSpeed = 10f;
	private bool facingRight = true;
	private Rigidbody2D body;
	
	float move;
	
	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		move = Input.GetAxis ("Horizontal");
		
		body.velocity = new Vector2(move * maxSpeed, body.velocity.y);
	}
}
