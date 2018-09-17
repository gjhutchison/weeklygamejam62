using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShepardVisionCone : MonoBehaviour {

    public GameObject _shepard;
    private ShepardController _shepardController;
    private GameObject _player;
    private SpriteRenderer _shepardSpriteRenderer;

    private float _currentAngle;
    private float _lookTargetAngle;
    private Vector2 _lookTarget;

    private bool _playerInView;

    private readonly static string PLAYER_TAG = "Player";

	// Use this for initialization
	void Start () {
        _shepardController = _shepard.GetComponent<ShepardController>();
        _shepardSpriteRenderer = _shepard.transform.Find("Shepard Sprite").GetComponent<SpriteRenderer>();
        _lookTarget = new Vector2(_shepard.transform.position.x + 10.0f, _shepard.transform.position.y);
        calculateLookTargetAngle();
        _playerInView = false;
        _player = GameObject.FindGameObjectWithTag(PLAYER_TAG);

        
    }
	
	// Update is called once per frame
	void Update () {
        bool lineOfSight = true;
        Vector2 origin = new Vector2(_shepard.transform.position.x, _shepard.transform.position.y);
        origin.y += 0.5f;
        Vector2 target = new Vector2(_player.transform.position.x, _player.transform.position.y);
        Vector2 direction = new Vector2(target.x - origin.x, target.y - origin.y);
        direction.Normalize();
        Debug.DrawLine(origin, target);
        RaycastHit2D[] hitTargets = Physics2D.RaycastAll(origin, direction, Vector2.Distance(origin, target));
        for (int i = 0; i < hitTargets.Length; i++) {
            lineOfSight = lineOfSight && !(hitTargets[i].collider.gameObject.tag == "Bush");
        }

        if ((_playerInView && lineOfSight) && !_player.GetComponent<PlayerController>().isDisguised()) {
            _shepardController.setChasing();
        }

        if(!(_playerInView && lineOfSight) && _shepardController.getState() == ShepardController.ShepardState.CHASE) {
            _shepardController.setInvestigate(new Vector2(_player.transform.position.x, _player.transform.position.y));
        }

        _currentAngle = Mathf.LerpAngle(_currentAngle, _lookTargetAngle, 0.2f);
        transform.localEulerAngles = new Vector3(0, 0, _currentAngle);
        
        if(_currentAngle > 360) {
            _currentAngle -= 360;
        }

        if(_currentAngle < 0) {
            _currentAngle += 360;
        }

        if(_currentAngle > 90.0f && _currentAngle < 270.0f) {
            _shepardSpriteRenderer.flipX = true;
        } else {
            _shepardSpriteRenderer.flipX = false;
        }
	}

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == PLAYER_TAG) {
            _playerInView = true;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == PLAYER_TAG) {
            _playerInView = false;
        }
    }

    private void spotPlayer(GameObject player) {
        _shepardController.setChasing();
    }

    public void setLookTarget(Vector2 target) {
        _lookTarget = new Vector2(target.x, target.y);
        calculateLookTargetAngle();
    }

    public void setLookAngle(float angle) {

    }

    private void calculateLookTargetAngle() {
        _lookTargetAngle = Mathf.Atan2(_lookTarget.y - GetComponentInParent<Transform>().position.y,
            _lookTarget.x - GetComponentInParent<Transform>().position.x) * Mathf.Rad2Deg;
    }
}
