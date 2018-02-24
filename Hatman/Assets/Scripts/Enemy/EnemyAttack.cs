using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

	public float timeBetweenAttacks = 0.5f;
	public int attackDamage = 10;

	Animator anim;
	GameObject player;
	PlayerHealth playerHealthScript;
	EnemyHealth enemyHealthScript;
	bool playerInRange;
	float timer;						//Used to control enemy attack speed

	// Use this for initialization
	void Awake () {
		anim = GetComponent<Animator> ();
		player = GameObject.FindGameObjectWithTag ("Player");
		playerHealthScript = player.GetComponent<PlayerHealth> ();
		enemyHealthScript = GetComponent <EnemyHealth> ();
	}
		
	/// <summary>
	/// Executes when something is in Trigger Collider (sphere colliders in Hatman)
	/// </summary>
	/// <param name="other">Thing that entered trigger</param>
	void OnTriggerEnter(Collider other)
	{
		//If this thing was the player, enemy can attack
		if (other.CompareTag("Player"))
			playerInRange = true;

		//If want to attack player, stop walking
		if (playerInRange)
			anim.SetBool ("IsWalking", false);		
	}
		
	/// <summary>
	/// Executes when something leaves Trigger Collider (sphere colliders in Hatman) 
	/// </summary>
	/// <param name="other">Thing that leaved trigger</param>
	void OnTriggerExit(Collider other)
	{
		//If this thing was the player, enemy ca't attacko anymore
		if (other.CompareTag("Player"))
			playerInRange = false;

		//Enable walking animation (must catch player)
		if(!playerInRange)
			anim.SetBool("IsWalking", true);		
	}
	
	// Update is called once per frame
	void Update () {
		//Control time between attacks
		timer += Time.deltaTime;
		//disable attack animation (have exit time in animator, won't stop immediately)
		anim.SetBool ("IsAttacking", false);
		//If player in range AND attack interval reached AND enemy is alive => attack player
		if (playerInRange && timer >= timeBetweenAttacks && enemyHealthScript.CurrentHealth > 0) {
			Attack ();
		}

		//If player is dead, disable enemy Moves, Attacks
		if (playerHealthScript.CurrentHealth <= 0) {
			GetComponent<EnemyFollowPlayer> ().enabled = false;
			anim.SetBool ("IsWalking", false);
			enabled = false;
		}

	}
		
	/// <summary>
	/// Attack player
	/// </summary>
	void Attack()
	{
		anim.SetBool ("IsAttacking", true);
		timer = 0f;
	}
		
	/// <summary>
	/// Event used in attack animation, to hurt player in proper moment
	/// </summary>
	void HurtPlayer()
	{
		if(playerHealthScript.CurrentHealth > 0)
			playerHealthScript.TakeDamage(attackDamage);
	}
}
