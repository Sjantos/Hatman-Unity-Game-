using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {

	public float playerSpeed = 20f;
	public float staminaLossSpeed = 5f;
	public float staminaRegenerationSpeed = 5f;
	public Slider staminaSlider;

	float speed = 20f;
	Vector3 movement;
	Animator anim;
	bool isWalking = false;
	bool isRunning = false;

	//Initialization
	void Awake()
	{
		anim = GetComponent<Animator> ();
	}

	// Update is called once per frame
	void FixedUpdate () {
		//Get inputs (x and z as unity axes)
		float x = Input.GetAxis ("Horizontal");
		float z = Input.GetAxis ("Vertical");

		//Update stamina slider and (value)
		float stamina = staminaSlider.value;
		if (isRunning) {
			stamina -= staminaLossSpeed * Time.deltaTime;
			stamina = Mathf.Clamp (stamina, staminaSlider.minValue, staminaSlider.maxValue);
		} else {
			stamina += staminaRegenerationSpeed * Time.deltaTime;
			stamina = Mathf.Clamp (stamina, staminaSlider.minValue, staminaSlider.maxValue);
		}
		staminaSlider.value = stamina;

		//Check if player hold left shift
		isRunning = Input.GetAxisRaw ("Run") > 0 ? true : false;
		//If stamina is 0, player can't run
		if (stamina <= 0)
			CancelRun ();

		//Move Player, PlayerCamera manages rotations
		Move (x, z); 
		//Animate character
		Anim (x, z);
	}

	/// <summary>
	/// Move the player
	/// </summary>
	/// <param name="x">Input value for x axis</param>
	/// <param name="z">Input value for z axis</param>
	void Move(float x, float z)
	{
		movement.Set (x, 0f, z);
		//Make sure that moving diagonally is the same speed as along axes (normalize)
		movement = movement.normalized * speed * Time.deltaTime;
		transform.Translate (movement);
	}

	/// <summary>
	/// Animation of character
	/// </summary>
	/// <param name="x">Input value for x axis</param>
	/// <param name="z">Input value for z axis</param>
	void Anim(float x, float z)
	{
		if (isRunning)
			speed = 3 * playerSpeed;
		else
			speed = playerSpeed;

		//Set walking animation, based on input values for movement
		if (x != 0f || z != 0f)
			isWalking = true;
		else
			isWalking = false;

		anim.SetBool ("IsWalking", isWalking);
		if (isWalking && isRunning)
			isRunning = true;
		else
			isRunning = false;
		anim.SetBool ("IsRunning", isRunning);
	}

	/// <summary>
	/// Aborts player run
	/// </summary>
	public void CancelRun()
	{
		isRunning = false;
		speed = playerSpeed;
	}
}
