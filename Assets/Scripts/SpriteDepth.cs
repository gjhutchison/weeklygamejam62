using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteDepth : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float posX = transform.position.x;
        float posY = transform.position.y;

        transform.position = new Vector3(posX, posY, posY);
	}
}
