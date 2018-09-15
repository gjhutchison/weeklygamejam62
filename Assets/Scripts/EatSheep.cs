using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EatSheep : MonoBehaviour {

	private string corpseSpriteNames = "Sprites/sheepcorpses";
	private Sprite[] sprites;

	// Use this for initialization
	void Start () {
		sprites = Resources.LoadAll<Sprite> (corpseSpriteNames);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		
	void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log(other.gameObject.tag);
		if (other.gameObject.tag == "Sheep") {
			// Eat the sheep (switch spite, and other handling)
			other.enabled = false;
			other.gameObject.GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];
		}
	}
}
