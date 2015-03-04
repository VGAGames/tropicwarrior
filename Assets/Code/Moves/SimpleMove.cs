using UnityEngine;
using System.Collections;

public class SimpleMove : MonoBehaviour {

	public Vector3 dir;
		
	// Update is called once per frame
	void Update () 
	{
		transform.localPosition += dir*Time.deltaTime;
	}
}
