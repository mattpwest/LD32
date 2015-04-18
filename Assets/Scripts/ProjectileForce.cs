using UnityEngine;
using System.Collections;

public class ProjectileForce : MonoBehaviour {
	public float speed = 0.001f;

	private Rigidbody2D body;

	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		body.velocity = new Vector2 (speed * transform.localScale.x, 0);
	}
}
