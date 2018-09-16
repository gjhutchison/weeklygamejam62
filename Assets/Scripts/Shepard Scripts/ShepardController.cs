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

    private enum ShepardState {
        IDLE,
        SEARCH,
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
        }
	}

    private void doChase() {
        Vector2 playerPos = new Vector2(_player.transform.position.x, _player.transform.position.y);
        Vector2 currentPos = new Vector2(transform.position.x, transform.position.y);
        
        float angle = Mathf.Atan2(playerPos.y - currentPos.y, playerPos.x - currentPos.x);
        float totalForce = FORCE * Time.deltaTime;
        Vector2 force = new Vector2(Mathf.Cos(angle) * totalForce, Mathf.Sin(angle) * totalForce);

        _hitbox.AddForce(force);
        _spriteBobber.activate();
    }

    public void setChasing() {
        _state = ShepardState.CHASE;
    }
}
