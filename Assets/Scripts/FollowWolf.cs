using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWolf : MonoBehaviour {

	public GameObject wolf;
	public float moveSpeed = 5.0f;
	public bool following = false;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (following) {
			transform.position = Vector2.MoveTowards (transform.position, wolf.transform.position, moveSpeed * Time.deltaTime);
		}
	}
}
