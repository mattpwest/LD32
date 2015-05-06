using UnityEngine;
using System.Collections;

public class WeaponShotgun : MonoBehaviour, IWeapon {

	public float reloadDelaySeconds = 3.0f;
	protected bool mustShoot = false;
	protected float reloadTimeRemainingSeconds = 0.0f;
	protected Transform shotSpawn;
	protected AudioSource audioSource;
	protected AudioClip shotSound;

	private void Start () {
		shotSpawn = createShotTransform ();

		AudioHolder audioHolder = GameObject.Find ("AudioHolder").GetComponent<AudioHolder>();
		shotSound = audioHolder.shotgunShot;
		
		audioSource = GetComponent<AudioSource> ();
	}

	protected Transform createShotTransform() {
		var bounds = GetComponent<BoxCollider2D>();
		
		Transform transform = new GameObject("ShotSpawn").transform;
		transform.parent = gameObject.transform;
		var x = bounds.offset.x + bounds.size.x * 1.2f;
		var y = bounds.offset.y;
		transform.localPosition = new Vector3(x, y, 0);
		transform.localRotation = Quaternion.Euler (0, 0, 0);
		
		return transform;
	}

	public void ChargeStart () {
		mustShoot = true;
	}

	public void ChargeStop () {
		mustShoot = false;
	}
	
	private void Update () {
		if (reloadTimeRemainingSeconds > 0.0f) {
			reloadTimeRemainingSeconds -= Time.deltaTime;
		}

		if ((mustShoot) && (reloadTimeRemainingSeconds <= 0.0f)) {
			Shoot ();
		}
	}

	protected void Shoot () {
		var spawnPos = new Vector2(shotSpawn.localPosition.x, shotSpawn.localPosition.y);
		
		// Spawn body
		GameObject spitBall = (GameObject) Instantiate (Resources.Load ("PreFabs/Projectiles/ShotgunBlast"), shotSpawn.position, Quaternion.identity);
		spitBall.transform.localScale = new Vector3(1, transform.localScale.x, 1);
		
		// Apply launch force
		//var force = spawnPos.normalized * (strength + strengthMin);
		//force = Vector2.Scale(force, new Vector2(transform.localScale.x, 1));
		//var body = spitBall.GetComponent<Rigidbody2D>();
		//body.AddForce(force, ForceMode2D.Impulse);
		
		//var direction = ((body.position + body.velocity) - body.position);
		//spitBall.GetComponent<RotateToVelocity> ().UpdateRotation (body, direction);
		
		// Reset shot strength
		//Debug.Log("Shot fired with strength: " + strength);
		//strength = 0.0f;
		
		// Start reload delay
		reloadTimeRemainingSeconds = reloadDelaySeconds;
		
		// Play the spit sound
		audioSource.clip = shotSound;
		audioSource.loop = false;
		audioSource.Play ();
	}
}
