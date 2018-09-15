using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EatSheep : MonoBehaviour {

	private bool isCollidingSheep = false;
	private string corpseSpriteNames = "Sprites/sheepcorpses";
	private Sprite[] sprites;

	// Use this for initialization
	void Start () {
		sprites = Resources.LoadAll<Sprite> (corpseSpriteNames);
	}
	
	// Update is called once per frame
	void Update () {
		isCollidingSheep = false;
	}
		
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Sheep") {
			if (isCollidingSheep) {
				return;
			}

			isCollidingSheep = true;
				
			// Eat the sheep (switch spite, and other handling)
			other.enabled = false;
			other.gameObject.GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];
		}
	}
}
