using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class BadGuyBackAndForthMove : MonoBehaviour {

	public float maxSpeed = 3f;
	public Transform sightStart, shootSightEnd, walkSightEnd, groundCheck;
	public LayerMask goodGuyLayerMask;
	public LayerMask turnAroundLayerMask;
	public LayerMask whatIsGround;

	private FacingDirection facing = FacingDirection.Right;
	private Rigidbody2D body;
	private bool spotted;
	private bool turnAround;
	private bool noGround;
	public bool grounded = false;
	private float groundRadius = 0.1f;


	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody2D> ();
	}

	void Update(){
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);

		Raycasting ();
		Behaviour ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
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
		turnAround = Physics2D.Linecast (sightStart.position, shootSightEnd.position, turnAroundLayerMask);
		noGround = !Physics2D.Linecast (sightStart.position, walkSightEnd.position, whatIsGround);
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
