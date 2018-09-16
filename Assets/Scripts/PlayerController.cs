using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public Sprite _standingSprite;
    public Sprite _disguisedSprite;
	public SpriteRenderer _sr;
    public Rigidbody2D _rb;
    public SpriteBobber _sb;
	public SheepEater _eater;

    private bool _up, _down, _left, _right, _action;

    private bool _flipped;

    private bool _controller;

    private bool _slowed;

    private bool _disguised;

    private readonly static float FORCE = 750.0f;
    private readonly static float DRAG = 3.0f;

	// Use this for initialization
	void Start () {
        _up = false;
        _down = false;
        _left = false;
        _right = false;
        _controller = false;
		_action = false;
    }
	
    void inputCheck() {
        if (Input.GetKeyDown(KeyCode.A)) {
            _left = true;
        }

        if (Input.GetKeyDown(KeyCode.D)) {
            _right = true;
        }

        if (Input.GetKeyDown(KeyCode.W)) {
            _up = true;
        }

        if (Input.GetKeyDown(KeyCode.S)) {
            _down = true;
        }

		if (Input.GetKeyDown(KeyCode.Space)) {
			_action = true;
		}

        if (Input.GetKeyUp(KeyCode.A)) {
            _left = false;
        }

        if (Input.GetKeyUp(KeyCode.D)) {
            _right = false;
        }

        if (Input.GetKeyUp(KeyCode.W)) {
            _up = false;
        }

        if (Input.GetKeyUp(KeyCode.S)) {
            _down = false;
        }

		if (Input.GetKeyUp(KeyCode.Space)) {
			_action = false;
		}

        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            _disguised = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift)) {
            _disguised = false;
        }
    }

	// Update is called once per frame
	void Update () {
        Vector2 force = new Vector2();
        inputCheck();

        float delta = Time.deltaTime;

        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");

        float totalForce = FORCE;

        float totalDrag = DRAG;

		if (_action) {
			_eater.gameObject.SetActive(true);
		}

        if (_slowed) {
            totalForce = totalForce * 0.75f;
            totalDrag = totalDrag + 3;
        }

        if(_disguised) {
            totalForce = totalForce * 0.8f;
            totalDrag += 1;
        }

        if(_left || _right || _up || _down) {
            xAxis = 1;
            yAxis = 1;
        }

        if(delta > 1.0f / 60.0f) {
            delta = 1.0f / 60.0f;
        }

        if (_left) {
            force.x += -totalForce * delta;
        }

        if(_right) {
            force.x += totalForce * delta;
        }

        if(_up) {
            force.y += totalForce * delta;
        }

        if(_down) {
            force.y += -totalForce * delta;
        }

        if(force.x == 0 && force.y == 0) {
            force.x = xAxis * totalForce * delta;
            force.y = yAxis * totalForce * delta;
        } else {
            force.x *= xAxis;
            force.y *= yAxis;
        }

        if(_rb.velocity.magnitude > 1) {
            _sb.activate();
        } else {
            _sb.deactivate();
        }

        _rb.AddForce(force);
        _rb.drag = totalDrag;

        if (_rb.velocity.x > 0) {
            _sr.flipX = false;
        }
        else if (_rb.velocity.x < 0) {
            _sr.flipX = true;
        }

        if (_disguised) {
            _sr.sprite = _disguisedSprite;
        } else {
            _sr.sprite = _standingSprite;
        }

    }

    public void setSlowed(bool slowed) {
        _slowed = slowed;
    }

    public bool isDisguised() {
        return _disguised;
    }
}
