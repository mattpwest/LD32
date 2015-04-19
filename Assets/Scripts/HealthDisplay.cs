using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour {

	public int halfValue = 1;
	public int fullValue = 2;
	private Health health;
	private Image image;
	public Sprite emptySprite;
	public Sprite halfSprite;
	public Sprite fullSprite;

	void Start () {
		GameObject player = GameObject.Find("Player1");
		health = player.GetComponent<Health>();
		image = GetComponentInParent<Image>();
	}
	
	void Update () {
		if (health.HP >= fullValue) {
			this.image.sprite = fullSprite;
		} else if (health.HP == halfValue) {
			this.image.sprite = halfSprite;
		} else {
			this.image.sprite = emptySprite;
		}
	}
}
