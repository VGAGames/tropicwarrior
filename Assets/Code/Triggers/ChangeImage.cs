using UnityEngine;
using System.Collections;

public class ChangeImage : MonoBehaviour {

	public Sprite imageToChange;


	
	void OnTriggerEnter2D(Collider2D other) 
	{
		if(other.tag == "Player")
		{
			GetComponent<SpriteRenderer>().sprite = imageToChange;
		}
	}

	void OnCollisionEnter2D(Collision2D other) 
	{
		if(other.transform.tag == "Player")
		{
			GetComponent<SpriteRenderer>().sprite = imageToChange;
		}
	}
}
