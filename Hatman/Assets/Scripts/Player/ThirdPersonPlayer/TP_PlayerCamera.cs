using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TP_PlayerCamera : MonoBehaviour {

	public float cameraSpeed = 1f;
	public float maxVerticalAngle = 45f;

	Camera mainCamera;
	float cameraVerticalAngle = 0f;

	// Use this for initialization
	void Awake () {
		mainCamera = Camera.main;
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {
		float y = Input.GetAxis ("Mouse X") * cameraSpeed * Time.deltaTime;
		cameraVerticalAngle -= Input.GetAxis ("Mouse Y") * cameraSpeed * Time.deltaTime;
		cameraVerticalAngle = Mathf.Clamp (cameraVerticalAngle, -maxVerticalAngle, maxVerticalAngle);

		mainCamera.transform.localEulerAngles = new Vector3 (cameraVerticalAngle, 0f, 0f);
		transform.Rotate (new Vector3(0f, y, 0f));
	}
}
