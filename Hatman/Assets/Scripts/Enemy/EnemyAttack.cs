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
	float timer;

	// Use this for initialization
	void Awake () {
		anim = GetComponent<Animator> ();
		player = GameObject.FindGameObjectWithTag ("Player");
		playerHealthScript = player.GetComponent<PlayerHealth> ();
		enemyHealthScript = GetComponent <EnemyHealth> ();
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))//gameObject == player)
			playerInRange = true;

		if (playerInRange)
			anim.SetBool ("IsWalking", false);		
	}

	void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))//gameObject == player)
			playerInRange = false;

		if(!playerInRange)
			anim.SetBool("IsWalking", true);		
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		anim.SetBool ("IsAttacking", false);
		if (playerInRange && timer >= timeBetweenAttacks && enemyHealthScript.CurrentHealth > 0) {
			Attack ();
		}
			
		if (playerHealthScript.CurrentHealth <= 0) {
			GetComponent<EnemyFollowPlayer> ().enabled = false;
			anim.SetBool ("IsWalking", false);
			enabled = false;
		}

	}

	void Attack()
	{
		anim.SetBool ("IsAttacking", true);
		timer = 0f;
	}

	//Event use in attack animation
	void HurtPlayer()
	{
		if(playerHealthScript.CurrentHealth > 0)
			playerHealthScript.TakeDamage(attackDamage);
	}
}
