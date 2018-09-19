using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepBehavior : MonoBehaviour, PlayerActionReceiver {
	private bool _dead = false;

	public SpriteRenderer _sr;
	public SpriteBobber _sb;
	public Rigidbody2D _rb;

	//private float _pauseTime = 2.2f;
	//private float _actionCounter = 0.0f;

	private string _deathSoundPath = "Audio/Baa";
	private string _corpseSpritePath = "Sprites/sheepcorpses";

	private Sprite[] _deathSprites;
	private AudioClip[] _deathSounds;

	private Vector2 _moveVector;
	private bool _flipped;

	private readonly static float FORCE = 3500.0f;
	private readonly static float DRAG = 3.0f;

	IEnumerator HopAround() {
		if (_dead) {
			yield break;
		}

		_moveVector = Vector2.zero;
		yield return new WaitForSeconds (Random.Range(1.0f,6.0f));
		_moveVector = new Vector2 (Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
		_moveVector.Normalize ();
		yield return new WaitForSeconds (Random.Range(0.7f,2.0f));
		StartCoroutine ("HopAround");
	}

	// Use this for initialization
	void Start () {
		_deathSprites = Resources.LoadAll<Sprite> (_corpseSpritePath);
		_deathSounds = Resources.LoadAll<AudioClip> (_deathSoundPath);
		_moveVector = Vector2.zero;
		StartCoroutine("HopAround");
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 force = new Vector2();

		float delta = Time.deltaTime;
		float totalForce = FORCE;
		float totalDrag = DRAG;

		if(delta > 1.0f / 60.0f) {
			delta = 1.0f / 60.0f;
		}

		force = _moveVector * totalForce * delta;

		if(_rb.velocity.magnitude > 1) {
			if (!_dead) {
				_sb.activate ();
			}
		} else {
			_sb.deactivate ();
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

	// AKA totally getting eaten
	public void OnPlayerAction() {
		_dead = true;

		// Swap the sprite to a random corpse sprite
		GameObject sheepObj = this.gameObject;
		GameObject spriteObj = sheepObj.transform.Find ("Sprite").gameObject;

		spriteObj.GetComponent<SpriteRenderer>().sprite = _deathSprites[Random.Range(0, _deathSprites.Length)];

		// Disable physics collisions on parent
		sheepObj.GetComponent<CapsuleCollider2D> ().enabled = false;

		// Death bleat
		Debug.Log(_deathSounds);
		sheepObj.GetComponent<AudioSource>().PlayOneShot(_deathSounds[Random.Range(0, _deathSounds.Length)]);

		_sb.deactivate();
		_rb.simulated = false;
	}

    public bool isDead() {
        return _dead;
    }
}
