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
    private Vector2 _startPosition;

    private float _searchLength;
    private float _currentSearchDelay;
    private int _currentSearchAction;

    private readonly static float FORCE = 675;

    public enum ShepardState {
        IDLE,
        CHASE,
        INVESTIGATE,
        SEARCH,
        WALK,
    };

	// Use this for initialization
	void Start () {
        _state = ShepardState.IDLE;
        _currentSearchDelay = 0;
        _startPosition = new Vector2(transform.position.x, transform.position.y);

    }
	
	// Update is called once per frame
	void Update () {
        SpriteRenderer coneSprite = _visionCone.transform.Find("Cone Renderer").GetComponent<SpriteRenderer>();
        switch (_state) {
            case ShepardState.CHASE:
                coneSprite.color = new Color(1,0,0,0.3f);
                doChase();
                break;
            case ShepardState.INVESTIGATE:
                coneSprite.color = new Color(1, 0.5f, 0.0f, 0.3f);
                doInvestigate();
                break;
            case ShepardState.SEARCH:
                coneSprite.color = new Color(1, 1, 0, 0.3f);
                doSearch();
                break;
            case ShepardState.IDLE:
                coneSprite.color = new Color(0, 0.5f, 0, 0.3f);
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

        if(Vector2.Distance(transform.position, _target) < 0.5f) {
            setSearching(Random.Range(3,9));
        }
    }

    private void doSearch() {
        if(_searchLength < 0) {
            _state = ShepardState.IDLE;
            _spriteBobber.deactivate();
            return;
        }

        if (_currentSearchDelay >= 0) {
            _currentSearchDelay -= Time.deltaTime;
        }
        else {
            Vector2 point = Random.insideUnitCircle * 4;

            point.x += transform.position.x;
            point.y += transform.position.y;

            _currentSearchAction = Random.Range(0, 2);
            _target = point;

            _currentSearchDelay = Random.Range(0.5f, 1.0f);
        }


        if(_currentSearchAction == 0) {
            _visionCone.GetComponent<ShepardVisionCone>().setLookTarget(_target);
            _spriteBobber.deactivate();
        } else if(_currentSearchAction == 1) {
            moveTowardsPoint(_target);
            _visionCone.GetComponent<ShepardVisionCone>().setLookTarget(_target);
            if(Vector2.Distance(transform.position, _target) < 0.1) {
                _currentSearchDelay = -1;
            }
        }

        _searchLength -= Time.deltaTime;


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

    public void setIdle() {

    }

    public void setChasing() {
        _state = ShepardState.CHASE;
    }

    public void setInvestigate(Vector2 targetPos) {
        _state = ShepardState.INVESTIGATE;
        _target = new Vector2(targetPos.x, targetPos.y);
    }

    public void setSearching(float length) {
        _state = ShepardState.SEARCH;
        _searchLength = length;
        _currentSearchDelay = -1;
    }

    public ShepardState getState() {
        return _state;
    }
}
