using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

	public Canvas GUICanvas;
	public int playerHealth = 100;
	public Image damageImage;
	public Slider slider;
	public float flashTime = 5f;
	public Color flashColour = new Color (1, 0, 0, 0.1f);

	int currentHealth;
	public int CurrentHealth {get{ return currentHealth; }}
	Animator anim;
	PlayerMovement playerMovement;
	PlayerAttack playerAttack;
	bool isDead = false;
	bool damaged = false;

	// Use this for initialization
	void Awake () {
		currentHealth = playerHealth;
		slider.maxValue = currentHealth;
		slider.value = currentHealth;
		anim = GetComponent<Animator> ();
		playerMovement = GetComponent<PlayerMovement> ();
		playerAttack = GetComponent<PlayerAttack> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (damaged) {
			damageImage.color = flashColour;
		} else {
			damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashTime * Time.deltaTime);
		}
		damaged = false;
	}

	/// <summary>
	/// Decrease health
	/// </summary>
	/// <param name="damage">Ammount of health to subtract</param>
	public void TakeDamage(int damage)
	{
		damaged = true;
		currentHealth -= damage;
		slider.value = currentHealth;
		//Check if player died
		if (currentHealth <= 0 && !isDead)
			Death();
	}

	/// <summary>
	/// Manages all action after player health reach 0
	/// </summary>
	void Death()
	{
		isDead = true;
		playerAttack.DisableEffects ();
		anim.SetTrigger ("Die");
		//Dead player can;t attack or move
		playerAttack.enabled = false;
		playerMovement.enabled = false;
		//Show GameOver screen
		//GUICanvas.GetComponent<GameOverScreen> ().ShowGameOverScreen ();
		Messenger.Broadcast(GameEvent.GameOver);
	}
}
