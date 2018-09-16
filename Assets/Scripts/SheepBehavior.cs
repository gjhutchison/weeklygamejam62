using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepBehavior : MonoBehaviour {
	public SpriteRenderer _sr;
	public SpriteBobber _sb;
	public Rigidbody2D _rb;
	private float _pauseTime = 2.2f;
	private float _actionCounter = 0.0f;

	private Vector2 _moveVector;
	private bool _flipped;

	private readonly static float FORCE = 3500.0f;
	private readonly static float DRAG = 3.0f;

	IEnumerator HopAround() {
		_moveVector = Vector2.zero;
		yield return new WaitForSeconds (Random.Range(1.0f,6.0f));
		_moveVector = new Vector2 (Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
		_moveVector.Normalize ();
		yield return new WaitForSeconds (Random.Range(0.7f,2.0f));
		StartCoroutine ("HopAround");
	}

	// Use this for initialization
	void Start () {
		_moveVector = Vector2.zero;
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

		if(delta > 1.0f / 60.0f) {
			delta = 1.0f / 60.0f;
		}
			
		force = _moveVector * totalForce * delta;

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
