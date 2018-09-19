using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SheepCounterUIScript : MonoBehaviour {

    private LevelController _levelController;
    private Text _counter;

	// Use this for initialization
	void Start () {
        _levelController = GameObject.Find("Level Controller").GetComponent<LevelController>();
        _counter = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        _counter.text = "" + _levelController.getSheepRemaining();
	}
}
