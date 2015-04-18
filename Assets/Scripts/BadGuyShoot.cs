using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class BadGuyShoot : MonoBehaviour {

	private FacingDirection facing = FacingDirection.Right;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerStay2D(Collider2D collision){
		if (collision.gameObject.layer == (int)Layer.GoodGuy) {
			Shoot();
		}
	}

	void Shoot(){
		Debug.Log ("bang!");
	}
}
