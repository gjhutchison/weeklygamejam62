using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShepardController : MonoBehaviour {

    public GameObject _visionCone;
    public Rigidbody2D _hitbox;
    private GameObject _player;
    public SpriteBobber _spriteBobber;

    private ShepardState _state;

    private Vector2 _target;
    private Vector2 _startPosition;

    private float _searchLength = 0;
    private float _currentSearchDelay = 0;
    private float _currentIdleLookDelay = 0;
    private int _currentSearchAction = 0;

    private float _currentIdleLookAngle = 0;
    private int _idleActionsRemaining = 0;
    private int _idleAction = 0;

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

        _player = GameObject.FindGameObjectWithTag("Player");

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
                doIdle();
                break;
        }
	}

    private void doChase() {
        Vector2 playerPos = new Vector2(_player.transform.position.x, _player.transform.position.y);
        moveTowardsPoint(playerPos);
        _visionCone.GetComponent<ShepardVisionCone>().setLookTarget(playerPos);

        float distance = Vector2.Distance(transform.position, _player.transform.position);
        print(distance);
        if (distance < 1.5f) {
            _player.GetComponent<PlayerController>().kill();
            setIdle();
        }

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

    private void doIdle() {
        if(_idleActionsRemaining <= 0) {
            _idleAction = Random.Range(0,2);

            if(_idleAction == 0) {
                _idleActionsRemaining = Random.Range(3, 10);
                _currentIdleLookDelay = 0;
            } else if(_idleAction == 1) {
                _idleActionsRemaining = 1;
                GameObject sheep = getRandomSheep();
                _target = new Vector2(sheep.transform.position.x, sheep.transform.position.y);
                _visionCone.GetComponent<ShepardVisionCone>().setLookTarget(_target);
                _currentIdleLookDelay = 7;
            }
        }

        if(_idleAction == 0) {
            if (_currentIdleLookDelay > 0) {
                _currentIdleLookDelay -= Time.deltaTime;
            }
            else {
                int choice = Random.Range(0, 9);
                _currentIdleLookAngle = choice * 45;
                _currentIdleLookDelay = Random.Range(1, 2.5f);
                _visionCone.GetComponent<ShepardVisionCone>().setLookAngle(_currentIdleLookAngle);
                _idleActionsRemaining--;
                _spriteBobber.deactivate();
            }
        } else if(_idleAction == 1) {
            if (Vector2.Distance(transform.position, _target) < 1) {
                _idleActionsRemaining--;
            }

            if(_currentIdleLookDelay < 0) {
                _idleActionsRemaining--;
            } else {
                _currentIdleLookDelay -= Time.deltaTime;
            }

            moveTowardsPoint(_target);
        }
    }

    private GameObject getRandomSheep() {
        GameObject[] allSheep = GameObject.FindGameObjectsWithTag("Sheep");
        return allSheep[Random.Range(0, allSheep.Length)];
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
        _state = ShepardState.IDLE;
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
