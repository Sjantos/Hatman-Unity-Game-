using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {

	public float cameraSpeed = 1f;
	public float maxVerticalAngle = 45f;

	//Used to adjust floating camera
	public float horizontalFloatDistance = 2f;
	public float verticalFloatDistance = 1f;
	public float horizontalAdd = 4f;
	public float verticalAdd = 2f;

	Camera mainCamera;
	float cameraVerticalAngle = 0f;
	float cameraHorizontalAngle = 0f;
	float floatedVertical = 0f;
	float floatedHorizontal = 0f;

	// Use this for initialization
	void Awake () {
		mainCamera = Camera.main;
		horizontalAdd *= Time.deltaTime;
		verticalAdd *= Time.deltaTime;
	}
	
	// Update is called once per frame
	void Update () {
		float x = Input.GetAxis ("Mouse Y") * cameraSpeed * Time.deltaTime;
		float y = Input.GetAxis ("Mouse X") * cameraSpeed * Time.deltaTime;

		//If mouse move was significant, make normal camera move
		if (Mathf.Abs (x) > 0.01f || Mathf.Abs (y) > 0.01f) {
			//Move camera from place where it was after floating
			cameraVerticalAngle = floatedVertical;
			cameraHorizontalAngle = floatedHorizontal;

			//Increment angles by input values
			cameraVerticalAngle -= x;
			cameraHorizontalAngle += y;
			//Prepare position foe possible float
			floatedHorizontal = cameraHorizontalAngle;
			floatedVertical = cameraVerticalAngle;
			//Limit camera vertical angle
			cameraVerticalAngle = Mathf.Clamp (cameraVerticalAngle, -maxVerticalAngle, maxVerticalAngle);

			//Rotate only camera along only x axis, because camera is child of Player, so Rotating player along y axis, will rotate camera too
			mainCamera.transform.localEulerAngles = new Vector3 (cameraVerticalAngle, 0f, 0f);
			transform.localEulerAngles = new Vector3 (0f, cameraHorizontalAngle, 0f);
		} else {
			FloatGunpoint ();
		}
	}

	/// <summary>
	/// Floats the gunpoint in "horizontal 8" shape
	/// </summary>
	void FloatGunpoint()
	{
		//Make camera "float"
		//Calculate amplitude from original point
		float verticalAmplitude = Mathf.Abs(floatedVertical - cameraVerticalAngle);
		float horizontalAmplitude = Mathf.Abs(floatedHorizontal - cameraHorizontalAngle);

		//This block of code make the gunpoint float in some "horizontal 8" shape
		//Float gunpoint horizontally by slight camera rotations
		if (horizontalAmplitude < horizontalFloatDistance) {
			floatedHorizontal += horizontalAdd;
			transform.Rotate (new Vector3 (0, horizontalAdd, 0f));
		} else {
			horizontalAdd *= -1;
			floatedHorizontal += horizontalAdd;
		}

		//Float gunpoint vertically by slight camera rotations
		if (verticalAmplitude < verticalFloatDistance/2) {
			floatedVertical += verticalAdd;
			mainCamera.transform.Rotate (new Vector3 (verticalAdd, 0f, 0f));
		} else {
			verticalAdd *= -1;
			floatedVertical += verticalAdd;
		}
	}
}
