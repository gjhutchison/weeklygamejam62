using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepBehavior : MonoBehaviour {
	public SpriteRenderer _sr;
	public SpriteBobber _sb;
	public Rigidbody2D _rb;
	private float _pauseTime = 2.2f;
	private float _actionCounter = 0.0f;

	private bool _s_up, _s_down, _s_left, _s_right;
	private bool _flipped;

	private readonly static float FORCE = 3500.0f;
	private readonly static float DRAG = 3.0f;

	IEnumerator HopAround() {
		yield return new WaitForSeconds (Random.Range(1.0f,3.0f));
		_s_left = true;
		yield return new WaitForSeconds (Random.Range(1.0f,3.0f));
		_s_left = false;
		yield return new WaitForSeconds (Random.Range(1.0f,3.0f));
		_s_right = true;
		yield return new WaitForSeconds (Random.Range(1.0f,3.0f));
		_s_right = false;
		StartCoroutine ("HopAround");
	}

	// Use this for initialization
	void Start () {
		StartCoroutine("HopAround");
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 force = new Vector2();

		float delta = Time.deltaTime;

		float xAxis = 0;
		float yAxis = 0;

		float totalForce = FORCE;
		float totalDrag = DRAG;

		if(_s_left || _s_right || _s_up || _s_down) {
			xAxis = 1;
			yAxis = 1;
		}

		if(delta > 1.0f / 60.0f) {
			delta = 1.0f / 60.0f;
		}

		if (_s_left) {
			force.x += -totalForce * delta;
		}

		if(_s_right) {
			force.x += totalForce * delta;
		}

		if(_s_up) {
			force.y += totalForce * delta;
		}

		if(_s_down) {
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
	}
}
