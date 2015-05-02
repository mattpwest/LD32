using UnityEngine;
using System.Collections;

public class WeaponSpit : MonoBehaviour, IWeapon {

	public float chargeRatePerSecond = 1.0f;
	public float spitAngle = 35.0f;
	private bool charging = false;
	private float strength = 0.0f;
	private float strengthMin = 1.0f;
	private float strengthMax = 2.0f;
	private Transform shotSpawn;
	private GameObject spitPrefab;
	private AudioSource audioSource;
	private AudioClip snort;
	private AudioClip spit;

	private void Start () {
		spitPrefab = GameObject.Find ("SpitBall");
		shotSpawn = createSpitSpawnTransform ();

		AudioHolder audioHolder = GameObject.Find ("AudioHolder").GetComponent<AudioHolder>();
		snort = audioHolder.snort;
		spit = audioHolder.spit;

		audioSource = GetComponent<AudioSource> ();
		audioSource.clip = snort;
		audioSource.loop = false;
	}

	protected Transform createSpitSpawnTransform() {
		var bounds = GetComponent<BoxCollider2D>();
		
		Transform transform = new GameObject("SpitSpawn").transform;
		transform.parent = gameObject.transform;
		var x = bounds.offset.x + bounds.size.x * 1.2f;
		var y = bounds.offset.y + bounds.size.y * 0.2f;
		transform.localPosition = new Vector3(x, y, 0);
		transform.localRotation = Quaternion.Euler (0, 0, 35);
		
		return transform;
	}

	public void ChargeStart() {
		charging = true;

		audioSource.Play ();
	}

	public void ChargeStop() {
		charging = false;

		audioSource.Stop ();
		audioSource.PlayOneShot(spit);
	}

	private void Update () {
		shotSpawn.localScale = gameObject.transform.localScale;

		ChargeSpit ();
		SpitIfChargingStopped ();
	}

	protected void ChargeSpit() {
		if ((charging) && (strength < strengthMax)) {
			Debug.Log ("Delta time: " + Time.deltaTime);
			strength += chargeRatePerSecond * Time.deltaTime;
		}
	}

	protected void SpitIfChargingStopped() {
		if ((charging == false) && (strength > 0.0f)) {
			Shoot ();
		}
	}

	protected void Shoot() {
		var spawnPos = new Vector2(shotSpawn.localPosition.x, shotSpawn.localPosition.y);
				
		// Spawn body
		GameObject spitBall = (GameObject) Instantiate (Resources.Load ("PreFabs/Projectiles/SpitBall"), shotSpawn.position, Quaternion.identity);
		spitBall.transform.localScale = new Vector3(1, transform.localScale.x, 1);

		// Apply launch force
		var force = spawnPos.normalized * (strength + strengthMin);
		force = Vector2.Scale(force, new Vector2(transform.localScale.x, 1));
		var body = spitBall.GetComponent<Rigidbody2D>();
		body.AddForce(force, ForceMode2D.Impulse);

		var direction = ((body.position + body.velocity) - body.position);
		spitBall.GetComponent<RotateToVelocity> ().UpdateRotation (body, direction);

		// Reset shot strength
		Debug.Log("Shot fired with strength: " + strength);
		strength = 0.0f;
	}

}
