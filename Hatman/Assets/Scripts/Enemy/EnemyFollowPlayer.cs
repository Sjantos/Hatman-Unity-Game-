using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollowPlayer : MonoBehaviour {

	Transform player;
	NavMeshAgent nav;
	Animator anim;
	PlayerHealth playerHealth;

	// Use this for initialization
	void Awake () {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		nav = GetComponent<NavMeshAgent> ();
		anim = GetComponent<Animator> ();
		//By default enemies walk towards player
		anim.SetBool ("IsWalking", true);
		playerHealth = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerHealth> ();
	}
	
	// Update is called once per frame
	void Update () {
		//Update player position to follow by NavMeshAgent
		nav.SetDestination (player.position);
		//If player is dead, stop chasing him
		if (playerHealth.CurrentHealth <= 0) {
			anim.SetTrigger ("PlayerDie");
			nav.isStopped = true;
		}
	}
}
