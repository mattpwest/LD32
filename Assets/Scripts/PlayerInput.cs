using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class PlayerInput : MonoBehaviour {
	
	public float maxSpeed = 10f;
	public float jumpForce = 250f;
	private FacingDirection facingDirection = FacingDirection.Right;
	private Rigidbody2D body;

	public bool grounded = false;
	public bool shouldJump = false;
	public Transform groundCheck;
	float groundRadius = 0.1f;
	public LayerMask whatIsGround;

	float move;
	
	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update(){
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);

		HandleInput ();
	}

	void FixedUpdate () {
		HandleInputPhysics ();
	}

	void HandleInput(){
		move = Input.GetAxis (Inputs.Horizontal);
		
		shouldJump = Input.GetButtonDown (Inputs.Jump) && grounded;
	}

	void HandleInputPhysics(){
		body.velocity = new Vector2(move * maxSpeed, body.velocity.y);

		if (shouldJump) {
			body.AddForce(new Vector2(0, jumpForce));
		}
	}
}
