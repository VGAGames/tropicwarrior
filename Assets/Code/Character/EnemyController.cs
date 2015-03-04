using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public float velocity;
	public Transform Skeleton;
	public Transform hitParticle, bloodParticle;

	[HideInInspector]
	public SkeletonAnimation skeletonAnimation;
	[HideInInspector]
	public Transform mainCharacter;
	

	void Awake()
	{
		skeletonAnimation = Skeleton.GetComponent<SkeletonAnimation>();
		mainCharacter = GameObject.FindGameObjectWithTag("MainCharacter").transform;
	}

	protected void HitParticle()
	{
		//Instancio la particula de HIT
		GameObject instance = CFX_SpawnSystem.GetNextObject(hitParticle.gameObject);
		instance.transform.position = Skeleton.GetComponent<Renderer>().bounds.center;
	}

	protected void BloodParticle()
	{
		//Instancio la particula de HIT
		GameObject instance = CFX_SpawnSystem.GetNextObject(bloodParticle.gameObject);
		instance.transform.position = Skeleton.GetComponent<Renderer>().bounds.center;
	}
}
