using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableObject : MonoBehaviour {

	private bool startHappened = false;

	void Start () {
		if (this.gameObject.GetComponent<Animator> () != null && startHappened == false) {
			this.gameObject.GetComponent<Animator> ().Stop (); 
			startHappened = true;
		}
	}

	void Update () {
		
	}
}
