using UnityEngine;
using System.Collections;
using AssemblyCSharp;
using System;

public class PlayerInput : MonoBehaviour {

	public float jumpForce = 250f;
	private FacingDirection facing = FacingDirection.Right;
	private Rigidbody2D body;
	public Animator animator;

	public bool grounded = false;

	public bool shouldJump = false;
	public bool shootCharging = false;
	private float shootStrength = 0.0f;
	private float shootStrengthMax = 3.0f;
	private float shootChargeRate = 1.0f;
	public GameObject shootPrefab;
	public Transform shootSpawn;

	public AudioClip spit;

	public Transform groundCheck;
	float groundRadius = 0.1f;
	public LayerMask whatIsGround;

	private AudioSource source;
	private Walk walk;
	
	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody2D> ();
		source = GetComponent<AudioSource> ();
		walk = GetComponent<Walk> ();
		//body.drag = 0.4f;
	}
	
	// Update is called once per frame
	void Update(){
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);

		HandleInput ();

		UpdateFacing ();
		UpdateShooting();
		UpdateAnimations();

		PlaySounds();

		if ((shootCharging == false) && (shootStrength > 0.0f)) {
			Shoot();
		}
	}

	void PlaySounds() {
		if (!source.isPlaying && shootCharging && shootStrength <= 3) {
			source.Play ();
		} else if (!shootCharging&& shootStrength > 0) {
			source.Stop ();
			source.PlayOneShot(spit);
		}
	}

	void UpdateAnimations() {
		animator.SetBool ("Grounded", grounded);
		animator.SetBool ("Walking", Math.Abs (body.velocity.x) > 0.1f);
	}

	void HandleInput() {
		walk.walkAnalog (Input.GetAxis (Inputs.Horizontal));

		if (Input.GetButtonDown (Inputs.Jump) && grounded){
			shouldJump = true;
		}

		if (Input.GetButtonDown (Inputs.Shoot)) {
			shootStrength = 1.0f;
			shootCharging = true;
		} else if (Input.GetButtonUp (Inputs.Shoot)) {
			shootCharging = false;
		}

		if (Input.GetButtonDown (Inputs.Cancel)) {
			Application.Quit();
		}
	}

	void UpdateShooting() {
		if ((shootCharging) && (shootStrength < shootStrengthMax)) {
			Debug.Log ("Delta time: " + Time.deltaTime);
			shootStrength += shootChargeRate * Time.deltaTime;
		}
	}
	
	void Shoot() {
		Debug.Log("Shot fired with strength: " + shootStrength);
		
		GameObject clone = (Instantiate (shootPrefab, shootSpawn.position, shootSpawn.rotation)) as GameObject;
		var body = clone.GetComponent<Rigidbody2D>();

		var spawnPos = new Vector2(shootSpawn.right.x, shootSpawn.right.y);

		// Apply launch force
		var force = spawnPos.normalized * shootStrength;
		force = Vector2.Scale(force, new Vector2(transform.localScale.x, 1));

		body.AddForce(force, ForceMode2D.Impulse);

		// Set initial angle
		var direction = ((body.position + force) - body.position);
		direction.Normalize();
		var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		body.rotation = angle;

		// Reset shot strength
		shootStrength = 0.0f;
	}

	void UpdateFacing(){
		var newFacing = facing;
		if (body.velocity.x > 0.1f) {
			newFacing = FacingDirection.Right;
		} else if (body.velocity.x < -0.1f) {
			newFacing = FacingDirection.Left;
		}

		facing = newFacing;
		var theScale = body.transform.localScale;
		theScale.x = (int) facing;
		body.transform.localScale = theScale;
	}

	void FixedUpdate () {
		if (shouldJump) {
			body.AddForce (new Vector2 (0, jumpForce));
			shouldJump = false;
		}
	}
}
