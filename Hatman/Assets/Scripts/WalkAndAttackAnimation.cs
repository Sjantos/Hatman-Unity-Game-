using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkAndAttackAnimation : MonoBehaviour {

	public Animation anim;
	public Transform chest;

	// Use this for initialization
	void Start () {
		anim ["Walk"].AddMixingTransform (chest, true);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
