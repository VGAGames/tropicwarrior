using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public string path;

	public void InstanceEnemy(string _nameEnemy)
	{
		EnemyController enemyInstantiate =  Instantiate(Resources.Load<EnemyController>(path + _nameEnemy), transform.localPosition, transform.localRotation) as EnemyController;

		if(Random.Range(0,100) >= 95)
			enemyInstantiate.velocity *= 2;

		if(transform.rotation.y == 1)
		{
			enemyInstantiate.velocity = -enemyInstantiate.velocity;
		}
	}
}
