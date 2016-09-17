using UnityEngine;
using System.Collections;

public class CharacterController2D : MonoBehaviour {

	// player controls
	[Range(0.0f, 10.0f)] // create a slider in the editor and set limits on moveSpeed
	public float moveSpeed = 3f;

	public float jumpForce = 600f;

	// LayerMask to determine what is considered ground for the player
	public LayerMask whatIsGround;
    public LayerMask deathGround;

	// Transform just below feet for checking if player is grounded
	public Transform groundCheck;

	// player can move?
	// we want this public so other scripts can access it but we don't want to show in editor as it might confuse designer
	[HideInInspector]
	public bool playerCanMove = true;

	// SFXs
	public AudioClip deathSFX;
	public AudioClip jumpSFX;

	// private variables below

	// store references to components on the gameObject
	Transform _transform;
	Rigidbody2D _rigidbody;
	Animator _animator;
	AudioSource _audio;

	// hold player motion in this timestep
	float _vx;
	float _vy;

	// player tracking
	bool facingRight = true;
	bool isGrounded = false;
	bool isRunning = false;
	bool canDoubleJump = false;

	// store the layer the player is on (setup in Awake)
	int _playerLayer;

	void Awake () {
		// get a reference to the components we are going to be changing and store a reference for efficiency purposes
		_transform = GetComponent<Transform> ();
		
		_rigidbody = GetComponent<Rigidbody2D> ();
		if (_rigidbody==null) // if Rigidbody is missing
			Debug.LogError("Rigidbody2D component missing from this gameobject");
		
		_animator = GetComponent<Animator>();
		if (_animator==null) // if Animator is missing
			Debug.LogError("Animator component missing from this gameobject");
		
		_audio = GetComponent<AudioSource> ();
		if (_audio==null) { // if AudioSource is missing
			Debug.LogWarning("AudioSource component missing from this gameobject. Adding one.");
			// let's just add the AudioSource component dynamically
			_audio = gameObject.AddComponent<AudioSource>();
		}

		// determine the player's specified layer
		_playerLayer = gameObject.layer;

	}

	// this is where most of the player controller magic happens each game event loop
	void Update()
	{
		// exit update if player cannot move or game is paused
		if (!playerCanMove || (Time.timeScale == 0f))
			return;

		// determine horizontal velocity change based on the horizontal input
		_vx = Input.GetAxisRaw ("Horizontal");

		// Determine if running based on the horizontal movement
		if (_vx != 0) 
		{
			isRunning = true;
		} else {
			isRunning = false;
		}

		// set the running animation state
		_animator.SetBool("Running", isRunning);

		// get the current vertical velocity from the rigidbody component
		_vy = _rigidbody.velocity.y;

		// Check to see if character is grounded by raycasting from the middle of the player
		// down to the groundCheck position and see if collected with gameobjects on the
		// whatIsGround layer
		isGrounded = Physics2D.Linecast(_transform.position, groundCheck.position, whatIsGround);

		//allow double jump after grounded
		if (isGrounded) {
			canDoubleJump = true;
		}

		// Set the grounded animation states
		_animator.SetBool("Grounded", isGrounded);

		if (isGrounded && Input.GetButtonDown ("Jump")) { // If grounded AND jump button pressed, then allow the player to jump
			//make the player jump
			DoJump ();
		} else if (canDoubleJump && Input.GetButtonDown ("Jump")) { //allow double jump if canDoubleJump and jump button is pressed
			DoJump ();
			//disable double jump after doing the double jump since you are allowed only once
			canDoubleJump = false;
		}
	
		// If the player stops jumping mid jump and player is not yet falling
		// then set the vertical velocity to 0 (he will start to fall from gravity)
		if(Input.GetButtonUp("Jump") && _vy>0f)
		{
			_vy = 0f;
		}

		// Change the actual velocity on the rigidbody
		_rigidbody.velocity = new Vector2(_vx * moveSpeed, _vy);

	}

	// Checking to see if the sprite should be flipped
	// this is done in LateUpdate since the Animator may override the localScale
	// this code will flip the player even if the animator is controlling scale
	void LateUpdate()
	{
		// get the current scale
		Vector3 localScale = _transform.localScale;

		if (_vx > 0) // moving right so face right
		{
			facingRight = true;
		} else if (_vx < 0) { // moving left so face left
			facingRight = false;
		}

		// check to see if scale x is right for the player
		// if not, multiple by -1 which is an easy way to flip a sprite
		if (((facingRight) && (localScale.x<0)) || ((!facingRight) && (localScale.x>0))) {
			localScale.x *= -1;
		}

		// update the scale
		_transform.localScale = localScale;
	}

	//make the player jump
	void DoJump()
	{
		// reset current vertical motion to 0 prior to jump
		_vy = 0f;
		// add a force in the up direction
		_rigidbody.AddForce (new Vector2 (0, jumpForce));
		// play the jump sound
		PlaySound (jumpSFX);
	}

	// do what needs to be done to freeze the player
 	void FreezeMotion() {
		playerCanMove = false;
		_rigidbody.isKinematic = true;
	}

	// do what needs to be done to unfreeze the player
	void UnFreezeMotion() {
		playerCanMove = true;
		_rigidbody.isKinematic = false;
	}

	// play sound through the audiosource on the gameobject
	void PlaySound(AudioClip clip)
	{
		_audio.PlayOneShot(clip);
	}

}
