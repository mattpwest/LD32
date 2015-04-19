using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	public int HP = 2;
	public int HPInitial = 2;

	void Start () {
		HP = HPInitial;
	}
	
	void Update () {
		if (HP <= 0) {
			Destroy (gameObject);
		}
	}

	public void Damage(int damage) {
		HP -= damage;
	}
}
