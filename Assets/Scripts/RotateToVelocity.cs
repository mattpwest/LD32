using UnityEngine;
using System.Collections;

public class RotateToVelocity : MonoBehaviour {

	Rigidbody2D bulletBody;

	void Start () {
		bulletBody = GetComponent<Rigidbody2D>();
	}
	
	void Update () {
		var direction = ((bulletBody.position + bulletBody.velocity) - bulletBody.position);

		UpdateRotation(bulletBody, direction.normalized);
	}

	public void UpdateRotation(Rigidbody2D body, Vector2 direction) {
		var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		body.rotation = angle;
	}
}
