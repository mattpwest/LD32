﻿using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class LoadWhenPlayerInTrigger : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.layer == (int)Layer.GoodGuy) {
			Application.LoadLevel (1);
		}
	}
}
