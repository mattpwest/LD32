using UnityEngine;
using System.Collections;

public class RotateToVelocity : MonoBehaviour {

	void Start () {
	
	}
	
	void Update () {
		var body = GetComponent<Rigidbody2D>();
		var transform = GetComponent<Transform>();

		var direction = ((body.position + body.velocity) - body.position);
		direction.Normalize();

		var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		body.rotation = angle;
		//Debug.Log("Spit angle: " + angle);
	}
}
