using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepEater : MonoBehaviour {

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
		this.gameObject.SetActive (false);
	}
		
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "SheepEatCollider") {
			
			if (isCollidingSheep) {
				return;
			}

			isCollidingSheep = true;

			// Disable the sheep eating collider
			other.enabled = false;

			// Swap the sprite to a random corpse sprite
			GameObject sheepObj = other.transform.parent.gameObject;
			GameObject spriteObj = sheepObj.transform.Find ("Sprite").gameObject;

			spriteObj.GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];

			// Disable physics collisions on parent
			sheepObj.GetComponent<BoxCollider2D> ().enabled = false;
		}
	}
}
