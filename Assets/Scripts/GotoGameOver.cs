using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class GotoGameOver : MonoBehaviour {
	
	void OnTriggerEnter2D(Collider2D collider) {
		var health = GetComponent<Health>();
		if (health.HP == 0) {
			Application.LoadLevel (3);
		}
	}
}
