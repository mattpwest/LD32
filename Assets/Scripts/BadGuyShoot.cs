using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class BadGuyShoot : MonoBehaviour {

	public GameObject projectilePrefab;
	public Transform sightStart, sightEnd;
	public bool canShoot = true;
	public LayerMask layerMask;
	public Transform projectileSpawn;

	private Rigidbody2D body;
	private float timer = 0f;
	public bool spotted;

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
		Raycasting ();
		Behaviour ();
	}

	void Raycasting(){
		spotted = Physics2D.Linecast (sightStart.position, sightEnd.position, layerMask);
	}

	void Behaviour(){
		if (spotted && canShoot) {
			canShoot = false;
			Shoot();
		}
	}

	void Shoot(){
		GameObject clone;

		clone = (Instantiate (projectilePrefab, projectileSpawn.position, projectileSpawn.rotation)) as GameObject;
		clone.transform.localScale = transform.localScale;

	}
}
