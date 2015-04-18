using UnityEngine;
using System.Collections;
using AssemblyCSharp;
using System;

public class PlayerInput : MonoBehaviour {

	public float jumpForce = 250f;
	public float maxSpeed = 3.0f;
	private FacingDirection facing = FacingDirection.Right;
	private Rigidbody2D body;
	public Animator animator;

	public bool grounded = false;
	public bool shouldJump = false;
	public bool shooting = false;
	public float shootStrength = 0.0f;
	public float shootChargeRate = 1.0f / 60.0f;
	public Transform groundCheck;
	float groundRadius = 0.1f;
	public LayerMask whatIsGround;

	float move;
	
	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody2D> ();
		//body.drag = 0.4f;
	}
	
	// Update is called once per frame
	void Update(){
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);

		HandleInput ();

		animator.SetBool ("Grounded", grounded);
		animator.SetBool ("Walking", Math.Abs (body.velocity.x) > 0.1f);

		UpdateFacing ();
	}

	void HandleInput(){
		move = Input.GetAxis (Inputs.Horizontal);

		shouldJump = Input.GetButtonDown (Inputs.Jump) && grounded;
		
		if ((!shooting) && (Input.GetButtonDown (Inputs.Shoot))) {
			shootStrength += shootChargeRate;
		} else if (shootStrength > 0.0f) {
			Shoot();
		}
	}

	void FixedUpdate () {
		body.velocity = new Vector2 (move * maxSpeed, body.velocity.y);
		
		if (shouldJump) {
			body.AddForce (new Vector2 (0, jumpForce));
		}
	}

	void UpdateFacing(){
		var newFacing = facing;
		if (body.velocity.x > 0.1f) {
			newFacing = FacingDirection.Right;
		} else if (body.velocity.x < -0.1f) {
			newFacing = FacingDirection.Left;
		}

		facing = newFacing;
		var theScale = body.transform.localScale;
		theScale.x = (int) facing;
		body.transform.localScale = theScale;
	}

	void Shoot() {
		shooting = true;

		// TODO: Spawn bullet

		shootStrength = 0.0f;
	}
}
