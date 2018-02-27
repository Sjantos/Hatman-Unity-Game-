using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GUIController : MonoBehaviour {

	[SerializeField] private GameObject startScreen;
	[SerializeField] private GameObject gameOverScreen;
	[SerializeField] private GameObject interactionAnswer;

	List<GameObject> objects;
	Animator anim;

	// Use this for initialization
	void Awake () {
		anim = GetComponent<Animator> ();

		Scene loadedScene = SceneManager.GetSceneByName ("FirstScene");
		objects = new List<GameObject>(loadedScene.GetRootGameObjects ());
		objects.Remove (this.gameObject);
		objects.RemoveAll (x => x.name == "EventSystem");

		Messenger.AddListener (GameEvent.GameOver, GameOver);
		Messenger.AddListener (GameEvent.GunpointOffTrigger, GunpointOffTrigger);
	}

	void OnDestroy()
	{
		Messenger.RemoveListener (GameEvent.GameOver, GameOver);
		Messenger.RemoveListener (GameEvent.GunpointOffTrigger, GunpointOffTrigger);
	}

	void Start()
	{
		foreach (var item in objects) {
			item.SetActive (false);
		}

		gameOverScreen.SetActive (false);
	}

	void GameOver()
	{
		gameOverScreen.SetActive (true);
		anim.SetTrigger ("PlayerDied");
	}

	public void AfterGameOverAnimation()
	{
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}

	public void PlayClicked()
	{
		anim.SetTrigger ("PlayClicked");
	}

	public void AfterStartScreenAnimation(){
		foreach (var item in objects)
			item.SetActive (true);
		//Lock cursor
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;

		//Don't need StartScreen anymore
		startScreen.SetActive (false);
	}

	public void PlayAgainClicked()
	{
		anim.SetTrigger ("PlayAgain");
		SceneManager.LoadScene ("FirstScene");
	}

	public void ExitClicked()
	{
		anim.SetTrigger ("Exit");
		Application.Quit ();
	}

	public void GunpointOffTrigger()
	{
		interactionAnswer.gameObject.SetActive (false);
	}
}
