using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteBobber : MonoBehaviour {

    //public GameObject _srContainer;

    public Transform _transform;

    private Vector2 _target;

    float _startTime;

    private bool _active;

    public float _speedMod;
    public float _distanceMod;

	// Use this for initialization
	void Start () {
        _startTime = 0;
        _active = false;
        _target = new Vector2();
	}
	
	// Update is called once per frame
	void Update () {
        float currentTime = Time.time;
        float delta = currentTime - _startTime;
        float y = 0;

        if (_active) {
            y = Mathf.Sin(delta*_speedMod) *_distanceMod + (_distanceMod);
        }

        Vector2 currentPos = new Vector2(0, _transform.localPosition.y);
        Vector2 targetPos = new Vector2(0,y);
        Vector2 endPosition = Vector2.Lerp(currentPos, targetPos, 0.35f);
        endPosition.y = endPosition.y - currentPos.y;
        _transform.Translate(0,endPosition.y,0);
    }

    public void activate() {
        if (_active) {
            return;
        }
        _active = true;
        _startTime = Time.time;
    }

    public void deactivate() {
        _active = false;
    }
}
