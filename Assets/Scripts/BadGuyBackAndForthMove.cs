using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class BadGuyBackAndForthMove : MonoBehaviour {

	public float maxSpeed = 3f;
	public Transform sightStart, sightEnd;
	public LayerMask goodGuyLayerMask;
	public LayerMask turnAroundLayerMask;

	public FacingDirection facing = FacingDirection.Right;
	private Rigidbody2D body;
	public bool spotted;
	public bool turnAround;

	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody2D> ();
	}

	void Update(){
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
		Debug.DrawLine (sightStart.position, sightEnd.position, Color.green);
		spotted = Physics2D.Linecast (sightStart.position, sightEnd.position, goodGuyLayerMask);
		turnAround = Physics2D.Linecast (sightStart.position, sightEnd.position, turnAroundLayerMask);
	}
	
	void Behaviour(){
		if (spotted) {
			StopWalking ();
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
