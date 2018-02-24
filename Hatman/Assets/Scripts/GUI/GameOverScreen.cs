using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour {

	public GameObject player;
	public Button playAgainButton;
	public Button exitButton;
	public GameObject[] objectsToDeactivate;

	Animator anim;
	PlayerHealth playerHealth;

	// Use this for initialization
	void Awake () {
		//Deactivate objects (ex. player, sounds, enemies)
		foreach (var item in objectsToDeactivate) {
			item.SetActive (false);
		}
		anim = GetComponent<Animator> ();
		//Add listeners to buttons
		playAgainButton.onClick.AddListener (PlayAgainClick);
		exitButton.onClick.AddListener (Exit);
		playerHealth = player.GetComponent<PlayerHealth> ();
	}

	/// <summary>
	/// Shows the game over screen when player is dead
	/// </summary>
	public void ShowGameOverScreen()
	{
		if (playerHealth.CurrentHealth <= 0) {
			//Move game over screen in hierarchy just to make sure it will be rendered on top
			transform.SetSiblingIndex (transform.GetSiblingIndex() + 1);
			anim.SetTrigger ("PlayerDied");
			//Wait for 1.5s after unlocking cursor
			new WaitForSecondsRealtime (1.5f);
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
	}

	/// <summary>
	/// Event for Play Again button
	/// </summary>
	public void PlayAgainClick()
	{
		anim.SetTrigger ("PlayAgain");
		SceneManager.LoadScene ("FirstScene");
	}

	/// <summary>
	/// Event for Exit button
	/// </summary>
	public void Exit()
	{
		anim.SetTrigger ("Exit");
		Application.Quit ();
	}
}
