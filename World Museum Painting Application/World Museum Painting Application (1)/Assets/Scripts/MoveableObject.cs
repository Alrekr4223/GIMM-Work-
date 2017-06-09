using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableObject : MonoBehaviour {

	void Start () {
		if (this.gameObject.GetComponent<Animator> () != null) {
			this.gameObject.GetComponent<Animator> ().Stop (); 
		}
	}

	void Update () {
		
	}
}
