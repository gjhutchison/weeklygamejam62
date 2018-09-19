using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

    public string _nextLevel;

    private GameObject[] _allSheep;
    private int _remainingSheep;


    // Use this for initialization
    void Start () {
        _allSheep = GameObject.FindGameObjectsWithTag("Sheep");
        _remainingSheep = _allSheep.Length;
	}
	
	// Update is called once per frame
	void Update () {
        _remainingSheep = 0;
		for(int i = 0; i < _allSheep.Length; i++) {
            if (!_allSheep[i].GetComponent<SheepBehavior>().isDead()) {
                _remainingSheep++;
            }
        }

        

        if(_remainingSheep == 0) {
            print("You Win");
        }
	}

    public int getSheepRemaining() {
        return _remainingSheep;
    }

    public bool wonGame() {
        return _remainingSheep == 0;
    }

    public string getNextLevel() {
        return _nextLevel;
    }
}
