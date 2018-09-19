using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushColliderScript : MonoBehaviour {

    public PlayerController _playerController;
    private bool _playerInBush;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        
	}

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Bush") {
            _playerController.setSlowed(true);
            _playerInBush = true;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "Bush") {
            _playerController.setSlowed(false);
            _playerInBush = false;
        }
    }
}
