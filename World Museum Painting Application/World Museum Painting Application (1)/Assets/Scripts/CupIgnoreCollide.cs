using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupIgnoreCollide : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter (Collision col){

		if (col.gameObject.tag == "MoveableCup" && this.gameObject.GetComponent<Collider>() != null) {
			Physics.IgnoreCollision (this.gameObject.GetComponent<Collider> (), col.gameObject.GetComponent<Collider> ());
		}
	}
}
