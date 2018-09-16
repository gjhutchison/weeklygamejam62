using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShepardVisionCone : MonoBehaviour {

    public ShepardController _shepardController;

    private float _currentAngle;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            spotPlayer(other.gameObject);
        }
    }

    private void spotPlayer(GameObject player) {
        _shepardController.setChasing();
        print("I see you!");
        
    }

    public void setLookTarget(Vector2 target) {

    }
}
