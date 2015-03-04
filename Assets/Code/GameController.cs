using UnityEngine;
using System.Collections;
using DG.Tweening;

namespace GoogleFu
{
	public class GameController : MonoBehaviour {
		
		public Spawner spawnLeft, spawnRight;
		public float timeSpawn;
		public float minSpawnTime, maxSpawnTime;
		public int currentWave;

		public AudioClip music;

		public bool pauseSpawnWaves;

		Waves _statsDb;


		private static GameController instance;
		public static GameController Instance 
		{
			get
			{
				return instance;
			}
			
			private set{
				instance = value;
			}
		}

		void Awake()
		{
			instance = this;

		}

		void Start () 
		{
			pauseSpawnWaves = false;

			GameObject statsdbobj = GameObject.Find("databaseObj");	
			if ( statsdbobj != null )
			{
				// Get the CharacterStats component out of the GameObject.
				// CharacterStats is the name of the worksheet in the google spreadsheet
				//  that we are getting the monster information from
				_statsDb = statsdbobj.GetComponent<Waves>();
			}
			StartCoroutine("SpawnWaves");
		}

		IEnumerator SpawnWaves()
		{
			int num = _statsDb.Rows[currentWave].Length;
			string nameEnemy = _statsDb.GetRow("WAVE_"+currentWave)[0];
			if(nameEnemy != "BOSS")
			{
				for(int i = 0; i < num; i++)
				{
					if(!pauseSpawnWaves)
					{
						nameEnemy = _statsDb.GetRow("WAVE_"+currentWave)[i];
						if( Random.Range(0,100) <= 50)
						{
							spawnLeft.InstanceEnemy(nameEnemy);
						}
						else
						{
							spawnRight.InstanceEnemy(nameEnemy);
						}
						yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
					}
					else
					{
						while(pauseSpawnWaves)
							yield return new WaitForFixedUpdate();
					}
				}
				currentWave++;
				//Espero un segundo hasta que empieza la siguiente oleada
				yield return new WaitForSeconds(1);
				StartCoroutine("SpawnWaves");
			}
			else
			{

			}
		}

	}
}