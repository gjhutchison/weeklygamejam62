using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public SpriteRenderer sr;

    public Rigidbody2D _rb;

    private bool _up, _down, _left, _right;

    private bool _flipped;

    private bool _controller;

    private readonly static float FORCE = 750.0f;

	// Use this for initialization
	void Start () {
        _up = false;
        _down = false;
        _left = false;
        _right = false;
        _controller = false;

    }
	
    void inputCheck() {
        if (Input.GetKeyDown(KeyCode.A)) {
            _left = true;
        } else if (Input.GetKeyDown(KeyCode.D)) {
            _right = true;
        } else if (Input.GetKeyDown(KeyCode.W)) {
            _up = true;
        } else if (Input.GetKeyDown(KeyCode.S)) {
            _down = true;
        }

        if (Input.GetKeyUp(KeyCode.A)) {
            _left = false;
        } else if (Input.GetKeyUp(KeyCode.D)) {
            _right = false;
        } else if (Input.GetKeyUp(KeyCode.W)) {
            _up = false;
        } else if (Input.GetKeyUp(KeyCode.S)) {
            _down = false;
        }
    }

	// Update is called once per frame
	void Update () {
        Vector2 force = new Vector2();
        inputCheck();


        if (_left) {
            force.x = -FORCE * Time.deltaTime;
        }

        if(_right) {
            force.x = FORCE * Time.deltaTime;
        }

        if(_up) {
            force.y = FORCE * Time.deltaTime;
        }

        if(_down) {
            force.y = -FORCE * Time.deltaTime;
        }

        _rb.AddForce(force);
        sr.flipX = _rb.velocity.x < 0;
    }
}
