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
	private IInput input;
	public Player player = Player.PlayerOne;
	
	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody2D> ();
		source = GetComponent<AudioSource> ();
		walk = GetComponent<Walk> ();
		groundSensor = GetComponent<GroundSensor> ();
		jump = GetComponent<Jump> ();
		weapon = GetComponent<WeaponSpit> ();
		//body.drag = 0.4f;
	}
	
	// Update is called once per frame
	void Update(){
		if (input == null) {
			input = GameObject.Find ("InputHolder").GetComponent<InputHolder> ().GetPlayerInput (player);
		}

		HandleInput ();

		UpdateFacing ();
		UpdateAnimations();
	}

	void UpdateAnimations() {
		animator.SetBool ("Grounded", groundSensor.isGrounded());
		animator.SetBool ("Walking", Math.Abs (body.velocity.x) > 0.1f);
	}

	void HandleInput() {
		walk.walkAnalog (input.GetAxis (InputAxis.Horizontal));

		if (input.GetButtonDown (Inputs.Jump)) {
			jump.jump();
		}

		if (input.GetButtonDown (Inputs.Fire)) {
			weapon.ChargeStart ();
		} else if (input.GetButtonUp (Inputs.Fire)) {
			weapon.ChargeStop ();
		}

		if (input.GetButtonDown (Inputs.Start)) {
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
