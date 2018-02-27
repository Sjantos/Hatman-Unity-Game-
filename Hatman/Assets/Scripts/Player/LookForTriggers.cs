using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookForTriggers : MonoBehaviour {

	public float interactionDistance = 20f;

	GameObject player;
	int interactionMask;
	float cameraToPlayerDistance = 0f;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		interactionMask = LayerMask.GetMask ("Interactive");
		cameraToPlayerDistance = (Camera.main.transform.position - player.transform.position).magnitude;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//Create ray to place where gunpoint points
		Ray interactionRay = Camera.main.ScreenPointToRay (new Vector3 (Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2, 0f));
		RaycastHit interactionHit;
		//If ray hit something on interactions layer, send messege to trigger to react
		if(Physics.Raycast (interactionRay, out interactionHit, interactionDistance, interactionMask)) {
			if (interactionHit.distance >= cameraToPlayerDistance) {
				interactionHit.collider.SendMessage ("GunpointOnTrigger", SendMessageOptions.DontRequireReceiver);
			}
		} else {
			//Send message to deactivate what trigger was showing
			Messenger.Broadcast("GunpointOffTrigger", MessengerMode.DONT_REQUIRE_LISTENER);
		}

	}
}
