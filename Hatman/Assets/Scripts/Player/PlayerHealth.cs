using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

	public int playerHealth = 100;
	public Image damageImage;
	public Slider slider;
	public float flashTime = 5f;
	public Color flashColour = new Color (1, 0, 0, 0.1f);

	int currentHealth;
	public int CurrentHealth {get{ return currentHealth; }}
	Animator anim;
	//Delete TP_ if want to use old version (orthogonal)
	TP_PlayerMovement playerMovement;
	TP_PlayerAttack playerAttack;
	bool isDead = false;
	bool damaged = false;

	// Use this for initialization
	void Awake () {
		currentHealth = playerHealth;
		slider.maxValue = currentHealth;
		slider.value = currentHealth;
		anim = GetComponent<Animator> ();
		playerMovement = GetComponent<TP_PlayerMovement> ();
		playerAttack = GetComponent<TP_PlayerAttack> ();
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

	public void TakeDamage(int damage)
	{
		damaged = true;
		currentHealth -= damage;
		slider.value = currentHealth;
		if (currentHealth <= 0 && !isDead)
			Death();
	}

	void Death()
	{
		isDead = true;
		playerAttack.DisableEffects ();
		anim.SetTrigger ("Die");
		playerAttack.enabled = false;
		playerMovement.enabled = false;
	}
}
