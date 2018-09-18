using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

    GameObject[] _allSheep;

	// Use this for initialization
	void Start () {
        _allSheep = GameObject.FindGameObjectsWithTag("Sheep");
	}
	
	// Update is called once per frame
	void Update () {
        bool sheepLeft = false;
		for(int i = 0; i < _allSheep.Length; i++) {
            sheepLeft = sheepLeft || !_allSheep[i].GetComponent<SheepBehavior>().isDead();
        }

        if(sheepLeft == false) {
            print("You Win");
        }
	}
}
