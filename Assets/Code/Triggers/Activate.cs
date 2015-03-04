using UnityEngine;
using System.Collections;

public class Activate : MonoBehaviour {

	public bool activateGravity;
	public bool activateUpAndDown;

	public Transform target;

	void OnTriggerEnter2D(Collider2D other) 
	{
		if(other.tag == "Player")
		{
			if(activateGravity)
			{
				target.GetComponent<Rigidbody2D>().isKinematic = false;
			}
			if(activateUpAndDown)
			{
				target.GetComponent<UpAndDown>().enabled = true;
			}
		}
	}

	void OnCollisionEnter2D(Collision2D other) 
	{
		if(other.transform.tag == "Player")
		{
			if(activateGravity)
			{
				target.GetComponent<Rigidbody2D>().isKinematic = false;
			}
			if(activateUpAndDown)
			{
				target.GetComponent<UpAndDown>().enabled = true;
			}
		}
	}
}
