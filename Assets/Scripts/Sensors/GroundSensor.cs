using UnityEngine;
using System.Collections;

public class GroundSensor : MonoBehaviour {

	public LayerMask whatIsGround;
	protected Transform groundCheck;
	protected float groundRadius = 0.1f;
	protected bool grounded = false;

	void Start () {
		var bounds = GetComponent<BoxCollider2D>();
		var body = bounds.attachedRigidbody;

		groundCheck = new GameObject("GroundCheck").transform;
		groundCheck.parent = gameObject.transform;
		var x = bounds.offset.x;
		var y = bounds.offset.y + bounds.size.y * 0.9f;
		groundCheck.position = new Vector3(x, y, 0);
	}

	public bool isGrounded() {
		return grounded;
	}

	void Update () {
	}

	void FixedUpdate() {
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
	}
}
