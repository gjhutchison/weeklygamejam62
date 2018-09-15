using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepHandler : MonoBehaviour {

	private int sheepEaten = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void ateSheep() {
		sheepEaten += 1;
	}
}
