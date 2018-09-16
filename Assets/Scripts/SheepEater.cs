using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepEater : MonoBehaviour {

	private bool isCollidingSheep = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		isCollidingSheep = false;
		this.gameObject.SetActive (false);
	}
		
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "SheepEatCollider") {
			if (isCollidingSheep) {
				return;
			}

			isCollidingSheep = true;

			// Disable the sheep eating collider
			other.enabled = false;

			SheepBehavior sheepBehavior = other.gameObject.GetComponentInParent<SheepBehavior>();
			sheepBehavior.OnPlayerAction();
		}
	}
}
