using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour {

	public int startingHealth = 30;
	//How fast dead enemies disappear
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
		//if enemy is dead, it sink in ground
		if (isSinking)
			transform.Translate (Vector3.down * sinkSpeed * Time.deltaTime, Space.World);
	}

	/// <summary>
	/// Hurt enemy
	/// </summary>
	/// <param name="damage">Value to subtract from health</param>
	public void TakeDamage(int damage)
	{
		if (isDead)
			return;
		//take away damage from health, if <=0 then die
		currentHealth -= damage;
		if (currentHealth <= 0)
			Death ();
	}

	/// <summary>
	/// Executes when enemi die
	/// </summary>
	void Death()
	{
		//Gain points for kill
		ScoreManager.score += enemyScoreWorth;

		//Disable player follow
		GetComponent<EnemyFollowPlayer> ().enabled = false;
		isDead = true;

		//Deactivate collider, so player can shoot over dead enemy, and dead enemy can't attack
		capsuleCollider.enabled = false;
		GetComponent<SphereCollider> ().enabled = false;
		anim.SetTrigger ("Die");
	}
		
	/// <summary>
	/// Dead enemy sink in ground
	/// </summary>
	void StartSinking()
	{
		GetComponent<NavMeshAgent> ().enabled = false;
		//Kinematic RigidBody is not affected with gravity
		GetComponent<Rigidbody> ().isKinematic = true;
		isSinking = true;

		//Destroy after 2s (enemy will be under ground by this time)
		Destroy (gameObject, 2f);
	}
}
