using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour {

	public Button playAgainButton;
	public Button exitButton;
	public GameObject[] objectsToDeactivate;

	Animator anim;

	// Use this for initialization
	void Awake () {
		foreach (var item in objectsToDeactivate) {
			item.SetActive (false);
		}
		anim = GetComponent<Animator> ();
		playAgainButton.onClick.AddListener (PlayAgainClick);
		exitButton.onClick.AddListener (Exit);
	}

	public void PlayAgainClick()
	{
		anim.SetTrigger ("PlayAgain");
		SceneManager.LoadScene ("FirstScene");
	}

	public void Exit()
	{
		anim.SetTrigger ("Exit");
		Application.Quit ();
	}
}
