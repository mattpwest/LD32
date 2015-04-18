using UnityEngine;
using System.Collections;

public class ProjectileForce : MonoBehaviour {
	public float force = 10f;

	private Rigidbody2D body;

	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody2D> ();
		body.AddForce (new Vector2(force * transform.localScale.x, 0), ForceMode2D.Impulse);
	}
	
	// Update is called once per frame
	void Update () {
	}
}
