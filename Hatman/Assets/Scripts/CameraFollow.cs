using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
	public Transform target;
	public float smoothing = 10f;

	Vector3 offset;

	// Use this for initialization
	void Start () {
		offset = transform.position - target.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector3 newCameraPosition = target.position + offset;
		transform.position = Vector3.Lerp (transform.position, newCameraPosition, smoothing * Time.deltaTime);
	}
}
