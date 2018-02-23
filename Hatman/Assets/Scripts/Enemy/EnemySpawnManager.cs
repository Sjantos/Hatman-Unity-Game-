using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour {

	public GameObject enemy;
	public float spawnInterval = 1f;
	public Transform[] spawnTable;

	PlayerHealth playerHealth;

	// Use this for initialization
	void Start () {
		playerHealth = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerHealth> ();
		InvokeRepeating ("Spawn", spawnInterval, spawnInterval);
	}
	
	void Spawn()
	{
		if (playerHealth.CurrentHealth <= 0) {
			return;
		}

		int spawner = Random.Range (0, spawnTable.Length);
		Instantiate (enemy, spawnTable [spawner].position, spawnTable [spawner].rotation);
	}
}
