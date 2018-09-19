using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public Sprite _standingSprite;
    public Sprite _disguisedSprite;
    public Sprite _wolfDead;
	public SpriteRenderer _sr;
    public Rigidbody2D _rb;
    public SpriteBobber _sb;
	public SheepEater _eater;
    public GameObject _bloodSprayer;

    private bool _up, _down, _left, _right, _action;

    private bool _flipped;

    //private bool _controller;

    private bool _slowed;

    private bool _disguised;

    private bool _dead;

    private readonly static float FORCE = 750.0f;
    private readonly static float DRAG = 3.0f;

	private readonly static float SLOWED_MULTIPLIER = 0.7f;
	private readonly static float SLOWED_DRAG = 6.0f;

	private readonly static float DISGUISE_MULTIPLIER = 0.6f;
	private readonly static float DISGUISE_DRAG = 4.0f;

	// Use this for initialization
	void Start () {
        _up = false;
        _down = false;
        _left = false;
        _right = false;
        //_controller = false;
		_action = false;
        _dead = false;
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

        float delta = Time.fixedDeltaTime;

        float totalForce = FORCE;

        float totalDrag = DRAG;

		if (_action && !_disguised) {
			_eater.gameObject.SetActive(true);
		}

        if (_slowed) {
			totalForce = totalForce * SLOWED_MULTIPLIER;
			totalDrag = SLOWED_DRAG;
        }

        if(_disguised) {
			totalForce = totalForce * DISGUISE_MULTIPLIER;
            totalDrag += DISGUISE_DRAG;
        }

        if (!_dead) {
            force = calcMovement(totalForce, delta);
        }

        _rb.AddForce(force);
        _rb.drag = totalDrag;

        if (_rb.velocity.x > 0) {
            _sr.flipX = false;
        }
        else if (_rb.velocity.x < 0) {
            _sr.flipX = true;
        }

        if (_dead) {
            _sr.sprite = _wolfDead;
            _sb.deactivate();
        } else if (_disguised) {
            _sr.sprite = _disguisedSprite;
        } else {
            _sr.sprite = _standingSprite;
        }

    }

    private Vector2 calcMovement(float totalForce, float delta) {
        Vector2 force = new Vector2();
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");

        if (_left || _right || _up || _down) {
            xAxis = 1;
            yAxis = 1;
        }

        if (_left) {
            force.x += -totalForce * delta;
        }

        if (_right) {
            force.x += totalForce * delta;
        }

        if (_up) {
            force.y += totalForce * delta;
        }

        if (_down) {
            force.y += -totalForce * delta;
        }

        if (force.x == 0 && force.y == 0) {
            force.x = xAxis * totalForce * delta;
            force.y = yAxis * totalForce * delta;
        }
        else {
            force.x *= xAxis;
            force.y *= yAxis;
        }

        if (_rb.velocity.magnitude > 1) {
            _sb.activate();
        }
        else {
            _sb.deactivate();
        }

        if (force.magnitude > totalForce * delta) {
            force.Normalize();
            force = force * (totalForce * delta);
        }

        return force;
    }

    public void kill() {
        if (_dead) {
            return;
        }
        _dead = true;
        _bloodSprayer.SetActive(true);
    }

    public void setSlowed(bool slowed) {
        _slowed = slowed;
    }

    public bool isDisguised() {
        return _disguised;
    }

    public bool isDead() {
        return _dead;
    }
}
