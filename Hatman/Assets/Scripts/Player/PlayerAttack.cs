using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

	// Rifle object from Armature
	public GameObject rifle;
	public GameObject rifleEnd;
	public int damage = 10;
	public float attackInterval = 0.25f;
	public float range = 1000f;
	public float shootEffectTimeProportion = 0.2f;

	Camera mainCamera;
	Animator anim;
	bool isShooting = false;
	PlayerMovement movementScript;
	Transform rifleEndPoint;
	Ray shootRay;
	RaycastHit shootHit;
	int shootableMask;
	LineRenderer gunLine;
	Light gunLight;
	AudioSource gunSound;
	float timer = 0f;

	// Use this for initialization
	void Awake () {
		//Everything that player can shoot is on Shootable layer
		shootableMask = LayerMask.GetMask ("Shootable");

		mainCamera = Camera.main;
		anim = GetComponent<Animator> ();
		movementScript = GetComponent<PlayerMovement> ();
		rifleEndPoint = rifleEnd.transform;
		gunLine = rifleEnd.GetComponent<LineRenderer> ();
		gunLight = rifleEnd.GetComponent<Light> ();
		gunSound = rifleEnd.GetComponent<AudioSource> ();
	}

	// Update is called once per frame
	void Update () {
		//Timer controls attack speed
		timer += Time.deltaTime;
		//Manages shooting, also when LMB held down
		if (Input.GetMouseButtonDown (0)) {
			isShooting = true;
		} else if (Input.GetMouseButtonUp (0)) {
			isShooting = false;
		}

		//Animate character
		Anim ();

		if (isShooting && timer >= attackInterval) {
			Shoot ();
		}

		//Disable shoot effects after some part of attackInterval
		if (timer >= attackInterval * shootEffectTimeProportion) {
			DisableEffects ();
		}
	}

	/// <summary>
	/// Let the player shoot once
	/// </summary>
	void Shoot()
	{
		timer = 0f;

		//Sound and visual effects of shooting
		gunSound.Play ();
		gunLight.enabled = true;
		gunLine.enabled = true;

		//gunLine.SetPosition set the position of line to draw
		gunLine.SetPosition (0, rifleEndPoint.position);
		//Create ray
		shootRay = mainCamera.ScreenPointToRay (new Vector3 (mainCamera.pixelWidth / 2, mainCamera.pixelHeight / 2, 0f));
		//Check if ray hit something
		if (Physics.Raycast (shootRay, out shootHit, range, shootableMask)) {
			gunLine.SetPosition (1, shootHit.point);

			//Check if this was a headshot (compare tag used because prefab can't rely on public drag&drop reference)
			if (shootHit.collider.gameObject.CompareTag("EnemyHead")) {
				//HEADSHOT
				EnemyHealth enemyHealth = shootHit.rigidbody.gameObject.GetComponent<EnemyHealth>();
				if (enemyHealth != null) {
					enemyHealth.TakeDamage (enemyHealth.CurrentHealth);
				}
			} else {
				//If we can get enemyHealth from target, it means we shot an enemy
				EnemyHealth enemyHealth = shootHit.transform.gameObject.GetComponent<EnemyHealth> ();
				if (enemyHealth != null) {
					enemyHealth.TakeDamage (damage);
				} else {
					//if player shot nothing, just drow very long line
					gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
				}
			}
		}




	}

	/// <summary>
	/// Disables the effects.
	/// </summary>
	public void DisableEffects()
	{
		gunLine.enabled = false;
		gunLight.enabled = false;
	}

	/// <summary>
	/// Animate shooting
	/// </summary>
	void Anim()
	{
		anim.SetBool ("IsAttacking", isShooting);
		if (isShooting)
			movementScript.CancelRun ();
	}
}
