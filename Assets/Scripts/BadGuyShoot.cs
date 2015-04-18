using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class BadGuyShoot : MonoBehaviour {

	public GameObject projectilePrefab;

	private Rigidbody2D body;
	public bool canShoot = true;
	private float timer = 0f;

	// Use this for initialization
	void Start () {
		body = GetComponentInParent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;

		if (timer > 3f) {
			timer = 0f;
 
			canShoot = true;
		}
	}

	void OnTriggerEnter2D(Collider2D collision){
		Debug.Log (collision.gameObject.layer);
		if (collision.gameObject.layer == (int)Layer.GoodGuy && canShoot) {
			canShoot = false;
			Shoot();
		}
	}

	void Shoot(){
		GameObject clone;

		clone = (Instantiate (projectilePrefab, transform.position, transform.rotation)) as GameObject;
		clone.transform.localScale = transform.localScale;

	}
}
