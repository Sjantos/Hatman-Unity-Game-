using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TP_PlayerMovement : MonoBehaviour {

	public float playerSpeed = 20f;
	public float staminaLossSpeed = 5f;
	public float staminaRegenerationSpeed = 5f;
	public Slider staminaSlider;

	float speed = 20f;
	Vector3 movement;
	Animator anim;
	bool isWalking = false;
	bool isRunning = false;
	Rigidbody playerRigidbody;

	//Initialization
	void Awake()
	{
		anim = GetComponent<Animator> ();
		playerRigidbody = GetComponent<Rigidbody> ();
	}

	// Update is called once per frame
	void FixedUpdate () {
		//Get inputs (x and z as unity axes)
		float x = Input.GetAxis ("Horizontal");
		float z = Input.GetAxis ("Vertical");

		float stamina = staminaSlider.value;
		if (isRunning) {
			stamina -= staminaLossSpeed * Time.deltaTime;
			stamina = Mathf.Clamp (stamina, staminaSlider.minValue, staminaSlider.maxValue);
		} else {
			stamina += staminaRegenerationSpeed * Time.deltaTime;
			stamina = Mathf.Clamp (stamina, staminaSlider.minValue, staminaSlider.maxValue);
		}

		staminaSlider.value = stamina;

		isRunning = Input.GetAxisRaw ("Run") > 0 ? true : false;
		if (stamina <= 0)
			CancelRun ();


		Move (x, z); //TP_PlayerCamera manages rotations
		Anim (x, z);
	}

	void Move(float x, float z)
	{
		movement.Set (x, 0f, z);
		movement = movement.normalized * speed * Time.deltaTime;
		transform.Translate (movement);
	}

	void Anim(float x, float z)
	{
		if (isRunning)
			speed = 3 * playerSpeed;
		else
			speed = playerSpeed;

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

	public void CancelRun()
	{
		isRunning = false;
		speed = playerSpeed;
	}
}
