using UnityEngine;
using System.Collections;
using AssemblyCSharp;
using System;

public class PlayerInput : MonoBehaviour {

	private FacingDirection facing = FacingDirection.Right;
	private Rigidbody2D body;
	public Animator animator;
	private AudioSource source;
	private Walk walk;
	private GroundSensor groundSensor;
	private Jump jump;
	private IWeapon weapon;
	
	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody2D> ();
		source = GetComponent<AudioSource> ();
		walk = GetComponent<Walk> ();
		groundSensor = GetComponent<GroundSensor> ();
		jump = GetComponent<Jump> ();
		weapon = GetComponent<WeaponSpit> ();
		weapon = GetComponent<WeaponShotgun> ();
		//body.drag = 0.4f;
	}
	
	// Update is called once per frame
	void Update(){
		HandleInput ();

		UpdateFacing ();
		UpdateAnimations();
	}

	void UpdateAnimations() {
		animator.SetBool ("Grounded", groundSensor.isGrounded());
		animator.SetBool ("Walking", Math.Abs (body.velocity.x) > 0.1f);
	}

	void HandleInput() {
		walk.walkAnalog (Input.GetAxis (Inputs.Horizontal));

		if (Input.GetButtonDown (Inputs.Jump)) {
			jump.jump();
		}

		if (Input.GetButtonDown (Inputs.Shoot)) {
			weapon.ChargeStart ();
		} else if (Input.GetButtonUp (Inputs.Shoot)) {
			weapon.ChargeStop ();
		}

		if (Input.GetButtonDown (Inputs.Cancel)) {
			Application.Quit();
		}
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
	}
}
