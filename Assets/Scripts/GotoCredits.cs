using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class GotoCredits : MonoBehaviour {
	
	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.layer == (int)Layer.GoodGuy) {
			Application.LoadLevel (2);
		}
	}
}
