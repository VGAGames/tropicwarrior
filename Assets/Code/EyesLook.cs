using UnityEngine;
using System.Collections;

public class EyesLook : MonoBehaviour {
	
	public Transform reference;
	public Transform target;
	public float factor = 0.25f;
	public float limit = 0.08f;
	
	private Vector3 center;
	
	void Start() {
		center = transform.position;
	}
	
	void Update () {
		Vector3 pos = target.transform.position;
		pos.z = 0.0f;
		Vector3 dir = (pos - reference.position) * factor;
		dir = Vector3.ClampMagnitude(dir, limit);
		Vector3 tt = center + dir;
		transform.position = center + dir;
	}
}
