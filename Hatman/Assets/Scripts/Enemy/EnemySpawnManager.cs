using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour {

	public GameObject enemy;
	public float spawnInterval = 1f;
	public Transform[] spawnTable;

	PlayerHealth playerHealth;

	// Use this for initialization
	void OnEnable () {
		playerHealth = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerHealth> ();
		//Start spawning enemies, repeat after every spawnInterval
		InvokeRepeating ("Spawn", spawnInterval, spawnInterval);
	}

	/// <summary>
	/// Spawn enemy in random spot from spawnTable field
	/// </summary>
	void Spawn()
	{
		//only when player is alive
		if (playerHealth.CurrentHealth <= 0) {
			return;
		}

		//Pick randomly from spawners where new enemy should appear
		int spawner = Random.Range (0, spawnTable.Length);
		Instantiate (enemy, spawnTable [spawner].position, spawnTable [spawner].rotation);
	}
}
