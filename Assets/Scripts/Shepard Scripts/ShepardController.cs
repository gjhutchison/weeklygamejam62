using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShepardController : MonoBehaviour {

    public GameObject _visionCone;
    public Rigidbody2D _hitbox;
    public GameObject _player;
    public SpriteBobber _spriteBobber;

    private ShepardState _state;

    private Vector2 _target;

    private readonly static float FORCE = 675;

    public enum ShepardState {
        IDLE,
        CHASE,
        INVESTIGATE
    };

	// Use this for initialization
	void Start () {
        _state = ShepardState.IDLE;
	}
	
	// Update is called once per frame
	void Update () {
        switch (_state) {
            case ShepardState.CHASE:
                doChase();
                break;
            case ShepardState.INVESTIGATE:
                doInvestigate();
                break;
        }
	}

    private void doChase() {
        Vector2 playerPos = new Vector2(_player.transform.position.x, _player.transform.position.y);
        moveTowardsPoint(playerPos);
        _visionCone.GetComponent<ShepardVisionCone>().setLookTarget(playerPos);
    }

    private void doInvestigate() {
        moveTowardsPoint(_target);
    }

    private void moveTowardsPoint(Vector2 target) {
        Vector2 currentPos = new Vector2(transform.position.x, transform.position.y);
        float angle = Mathf.Atan2(target.y - currentPos.y, target.x - currentPos.x);
        float totalForce = FORCE * Time.deltaTime;
        Vector2 force = new Vector2(Mathf.Cos(angle) * totalForce, Mathf.Sin(angle) * totalForce);

        _hitbox.AddForce(force);

        if(_hitbox.velocity.magnitude > 1) {
            _spriteBobber.activate();
        } else {
            _spriteBobber.deactivate();
        }
    }

    public void setChasing() {
        _state = ShepardState.CHASE;
    }

    public void setInvestigate(Vector2 targetPos) {
        _state = ShepardState.INVESTIGATE;
        _target = new Vector2(targetPos.x, targetPos.y);
    }

    public ShepardState getState() {
        return _state;
    }
}
