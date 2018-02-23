using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TP_PlayerAttack : MonoBehaviour {

	public GameObject rifle;
	public GameObject rifleEnd;
	public int damage = 10;
	public float attackInterval = 0.25f;
	public float range = 1000f;
	public float shootEffectTimeProportion = 0.2f;

	Camera mainCamera;
	Animator anim;
	bool isShooting = false;
	TP_PlayerMovement movementScript;
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
		shootableMask = LayerMask.GetMask ("Shootable");

		mainCamera = Camera.main;
		anim = GetComponent<Animator> ();
		movementScript = GetComponent<TP_PlayerMovement> ();
		rifleEndPoint = rifleEnd.transform;
		gunLine = rifleEnd.GetComponent<LineRenderer> ();
		gunLight = rifleEnd.GetComponent<Light> ();
		gunSound = rifleEnd.GetComponent<AudioSource> ();
	}

	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (Input.GetMouseButtonDown (0)) {
			isShooting = true;
		} else if (Input.GetMouseButtonUp (0)) {
			isShooting = false;
		}

		Anim ();

		if (isShooting && timer >= attackInterval) {
			Shoot ();
		}

		if (timer >= attackInterval * shootEffectTimeProportion) {
			DisableEffects ();
		}
	}

	void Shoot()
	{
		timer = 0f;

		gunSound.Play ();
		gunLight.enabled = true;
		gunLine.enabled = true;

		gunLine.SetPosition (0, rifleEndPoint.position);
		shootRay = mainCamera.ScreenPointToRay (new Vector3 (mainCamera.pixelWidth / 2, mainCamera.pixelHeight / 2, 0f));
		if (Physics.Raycast (shootRay, out shootHit, range, shootableMask)) {
			gunLine.SetPosition (1, shootHit.point);
			EnemyHealth enemyHealth = shootHit.transform.gameObject.GetComponent<EnemyHealth> ();
			if (enemyHealth != null)
				enemyHealth.TakeDamage (damage);
		} else {
			gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
		}
	}

	public void DisableEffects()
	{
		gunLine.enabled = false;
		gunLight.enabled = false;
	}

	void Anim()
	{
		anim.SetBool ("IsAttacking", isShooting);
		if (isShooting)
			movementScript.CancelRun ();
	}
}
