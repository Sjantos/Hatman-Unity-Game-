using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractiveTrigger : MonoBehaviour {

	public string responseText;
	public Image interactionAnswerUI;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void GunpointOnTrigger()
	{
		interactionAnswerUI.gameObject.SetActive (true);
		interactionAnswerUI.GetComponentInChildren<Text> ().text = responseText;
	}

	public void GunpointOffTrigger()
	{
		interactionAnswerUI.gameObject.SetActive (false);
	}
}
