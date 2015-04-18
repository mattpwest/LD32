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
	public bool shootCharging = false;
	private float shootStrength = 0.0f;
	private float shootStrengthMax = 4.0f;
	private float shootChargeRate = 2.0f;
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
		UpdateFacing ();
		UpdateShooting();
		UpdateAnimations();
	}

	void UpdateAnimations() {
		animator.SetBool ("Grounded", grounded);
		animator.SetBool ("Walking", Math.Abs (body.velocity.x) > 0.1f);
	}

	void HandleInput() {
		move = Input.GetAxis (Inputs.Horizontal);

		shouldJump = Input.GetButtonDown (Inputs.Jump) && grounded;

		if (Input.GetButtonDown (Inputs.Shoot)) {
			shootCharging = true;
		} else if (Input.GetButtonUp (Inputs.Shoot)) {
			shootCharging = false;
		}
	}

	void UpdateShooting() {
		if ((shootCharging) && (shootStrength < shootStrengthMax)) {
			Debug.Log ("Delta time: " + Time.deltaTime);
			shootStrength += shootChargeRate * Time.deltaTime;
		}
		
		if ((shootCharging == false) && (shootStrength > 0.0f)) {
			Shoot();
		}
	}
	
	void Shoot() {
		Debug.Log("Shot fired with strength: " + shootStrength);
		
		// TODO: Spawn bullet
		
		shootStrength = 0.0f;
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

	void FixedUpdate () {
		body.velocity = new Vector2 (move * maxSpeed, body.velocity.y);
		
		if (shouldJump) {
			body.AddForce (new Vector2 (0, jumpForce));
		}
	}
}
