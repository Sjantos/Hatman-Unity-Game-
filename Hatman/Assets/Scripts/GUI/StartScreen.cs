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
		foreach (var item in sceneObjects) {
			item.SetActive (false);
		}
		anim = GetComponent<Animator> ();
		playButton.onClick.AddListener (MyOnClick);
	}

	public void MyOnClick()
	{
		anim.SetTrigger ("PlayClicked");
		new WaitForSecondsRealtime (2f);
		foreach (var item in sceneObjects) {
			item.SetActive (true);
		}
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}
}
