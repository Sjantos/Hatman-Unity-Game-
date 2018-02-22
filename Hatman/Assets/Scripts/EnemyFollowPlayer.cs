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
		anim.SetBool ("IsWalking", true);
		playerHealth = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerHealth> ();
	}
	
	// Update is called once per frame
	void Update () {
		nav.SetDestination (player.position);
		if (playerHealth.CurrentHealth <= 0) {
			anim.SetTrigger ("PlayerDie");
			nav.isStopped = true;
		}
	}
}
