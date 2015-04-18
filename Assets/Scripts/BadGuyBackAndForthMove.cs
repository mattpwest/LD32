using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class BadGuyBackAndForthMove : MonoBehaviour {

	public float maxSpeed = 5f;
	private FacingDirection facing = FacingDirection.Right;
	private Rigidbody2D body;

	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody2D> ();

		ContinueWalking ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
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

	void OnTriggerStay2D(Collider2D collider) {
		if (collider.gameObject.layer == (int)Layer.Ground && body.velocity.x == 0) {
			ContinueWalking();
			flip ();
		} else if (collider.gameObject.layer == (int)Layer.GoodGuy) {
			UpdateSpeed (new Vector2 (0, 0));
		}
	}

	void OnTriggerExit2D(Collider2D collider) {
		if (collider.gameObject.layer == (int)Layer.Ground 
		    || collider.gameObject.layer == (int)Layer.GoodGuy) {
			ContinueWalking ();
		}
	}

	void ContinueWalking(){
		var velocity = new Vector2(maxSpeed * (int)facing, body.velocity.y);
		UpdateSpeed (velocity);
	}

	void UpdateSpeed (Vector2 velocity){
		body.velocity = velocity;
	}
}
