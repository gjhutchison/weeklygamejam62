using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushColliderScript : MonoBehaviour {

    public PlayerController _playerController;
    private bool _playerInBush;

	// Use this for initialization
	void Start () {
        _playerInBush = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (_playerInBush) {
            float distance = Vector2.Distance(_playerController.gameObject.transform.position, transform.parent.gameObject.transform.position);
            if(distance > 1) {
                _playerInBush = false;
                _playerController.setSlowed(false);
            }
        }
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
