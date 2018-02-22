using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour {

	public int startingHealth = 30;
	public float sinkSpeed = 1f;
	public int enemyScoreWorth = 10;

	int currentHealth;
	public int CurrentHealth {get { return currentHealth;}}
	Animator anim;
	CapsuleCollider capsuleCollider;
	bool isDead = false;
	bool isSinking = false;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		capsuleCollider = GetComponent<CapsuleCollider> ();

		currentHealth = startingHealth;
	}
	
	// Update is called once per frame
	void Update () {
		if (isSinking)
			transform.Translate (Vector3.down * sinkSpeed * Time.deltaTime, Space.World);
	}

	public void TakeDamage(int damage)
	{
		if (isDead)
			return;

		currentHealth -= damage;
		if (currentHealth <= 0)
			Death ();
	}

	void Death()
	{
		ScoreManager.score += enemyScoreWorth;
		GetComponent<EnemyFollowPlayer> ().enabled = false;
		isDead = true;
		capsuleCollider.isTrigger = true;
		//Deactivate colliders which would block player attacks
		capsuleCollider.enabled = false;
		GetComponent<SphereCollider> ().enabled = false;
		anim.SetTrigger ("Die");
	}

	void StartSinking()
	{
		GetComponent<NavMeshAgent> ().enabled = false;
		GetComponent<CapsuleCollider> ().isTrigger = true;
		GetComponent<Rigidbody> ().isKinematic = true;
		isSinking = true;

		Destroy (gameObject, 2f);
	}
}
