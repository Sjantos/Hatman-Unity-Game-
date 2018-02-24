using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour {

	public Button playButton;
	public GameObject[] sceneObjects;

	Animator anim;

	// Use this for initialization
	void Awake () {
		//Deactivate objects (ex. player, sounds, enemies)
		foreach (var item in sceneObjects) {
			item.SetActive (false);
		}
		anim = GetComponent<Animator> ();
		playButton.onClick.AddListener (PlayClick);
	}

	/// <summary>
	/// Event to call after Play button click
	/// </summary>
	public void PlayClick()
	{
		anim.SetTrigger ("PlayClicked");

		//Wait 2s to let animation end, then activate deactivated in Awake() objects
		new WaitForSecondsRealtime (2f);
		foreach (var item in sceneObjects) {
			item.SetActive (true);
		}
		//Lock cursor
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}
}
