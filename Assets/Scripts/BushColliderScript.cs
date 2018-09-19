using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushColliderScript : MonoBehaviour {

    public PlayerController _playerController;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Bush") {
            _playerController.setSlowed(true);
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "Bush") {
            _playerController.setSlowed(false);
        }
    }
}
