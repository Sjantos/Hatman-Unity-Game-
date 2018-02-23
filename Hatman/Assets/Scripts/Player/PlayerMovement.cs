using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour 
{
	public float playerSpeed = 20f;

	float speed = 20f;
	Vector3 movement;
	Animator anim;
	bool isWalking = false;
	bool isRunning = false;
	Rigidbody playerRigidbody;
	int floorMask;
	float cameraRayLength = 1000f;

	//Initialization
	void Awake()
	{
		floorMask = LayerMask.GetMask ("Floor");
		anim = GetComponent<Animator> ();
		playerRigidbody = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//Get inputs (x and z as unity axes)
		float x = Input.GetAxis ("Horizontal");
		float z = Input.GetAxis ("Vertical");

		
		isRunning = Input.GetAxisRaw ("Run") > 0 ? true : false;
		
		Move (x, z);
		Turn ();
		Anim (x, z);

		//transform.Translate(new Vector3(horizontalMove, 0, verticalMove));
	}

	void Move(float x, float z)
	{
		movement.Set (x, 0f, z);
		movement = movement.normalized * speed * Time.deltaTime;
		transform.Translate (movement, Space.World);
		//playerRigidbody.MovePosition (playerRigidbody.transform.position + movement);
	}

	void Turn()
	{
		//Ray from camera to mouse on floor layer, player will face this point
		Ray cameraRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit floorHit;
		if (Physics.Raycast (cameraRay, out floorHit, cameraRayLength, floorMask)) {
			Vector3 playerDirection = floorHit.point - transform.position;
			playerDirection.y = 0f;
			Quaternion newRotation = Quaternion.LookRotation (playerDirection);
			playerRigidbody.MoveRotation (newRotation);
			//transform.rotation = newRotation;
		}
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
