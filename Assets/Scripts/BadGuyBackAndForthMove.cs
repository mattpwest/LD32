using UnityEngine;
using System.Collections;
using AssemblyCSharp;
using System;

public class BadGuyBackAndForthMove : MonoBehaviour {

	public float maxSpeed = 3f;
	public Transform sightStart, shootSightEnd, walkEdgeSightEnd, groundCheck;
	public LayerMask goodGuyLayerMask;
	public LayerMask turnAroundLayerMask;
	public LayerMask whatIsGround;

	private FacingDirection facing = FacingDirection.Right;
	private Rigidbody2D body;
	private bool spotted;
	private bool turnAround;
	private bool noGround;
	private bool grounded = false;
	private float groundRadius = 0.1f;
	private Animator animator;


	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator> ();
		animator.SetBool ("Hostile", true);
	}

	void Update(){
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		turnAround = Physics2D.OverlapCircle (sightStart.position, groundRadius, turnAroundLayerMask);

		Raycasting ();
		Behaviour ();
		UpdateAnimations ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	}

	void UpdateAnimations() {
		animator.SetBool ("Grounded", grounded);
		animator.SetBool ("Walking", Math.Abs (body.velocity.x) > 0.1f);
	}

	
	void Flip(){
		if (facing == FacingDirection.Right) {
			facing = FacingDirection.Left;
		} else if (facing == FacingDirection.Left) {
			facing = FacingDirection.Right;
		}

		var theScale = transform.localScale;
		theScale.x = (int)facing;
		transform.localScale = theScale;
	}

	void Raycasting(){
		spotted = Physics2D.Linecast (sightStart.position, shootSightEnd.position, goodGuyLayerMask);
		noGround = !Physics2D.Linecast (sightStart.position, walkEdgeSightEnd.position, whatIsGround);
	}

	void Behaviour(){
		if (spotted || !grounded) {
			StopWalking ();
		} else if (grounded && noGround){
			Flip();
		} else if (turnAround && body.velocity.x == 0) {
			Flip ();
		} else {
			ContinueWalking ();
		}
	}

	void ContinueWalking(){
		UpdateSpeed (maxSpeed * (int)facing);
	}

	void StopWalking(){
		UpdateSpeed (0);
	}

	void UpdateSpeed (float xVelocity){
		body.velocity = new Vector2(xVelocity, body.velocity.y);
	}
}
