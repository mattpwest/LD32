using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class ProjectileForce : MonoBehaviour {
	public float force = 10f;
	public LayerMask layerMask;
	public float opacityRetainedPercentage = 0.99f;
	public int damage = 1;

	private Rigidbody2D body;
	private Renderer renderer;

	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody2D> ();
		renderer = GetComponent<Renderer> ();
		body.AddForce (new Vector2(force * transform.localScale.x, 0), ForceMode2D.Impulse);
	}
	
	// Update is called once per frame
	void Update () {
		if (Mathf.Abs(transform.localScale.x) <= 2) {
			ScaleProjectile();
		}
		if (renderer.material.color.a > 0) {
			FadeProjectile();
		}
	}

	void ScaleProjectile(){
		var theScale = transform.localScale;
		theScale.x *= 1.1f;
		transform.localScale = theScale;
	}

	void FadeProjectile() {
		Color color = renderer.material.color;
		color.a *= opacityRetainedPercentage;
		renderer.material.color = color;
	}

	void OnTriggerEnter2D(Collider2D collider){
		if (collider.gameObject.tag.Equals(Tag.ProjectileCollidable)) {
			var health = collider.gameObject.GetComponent<Health>();

			if (health != null) {
				health.Damage(damage);
			}

			Destroy (gameObject);
		}
	}
}
