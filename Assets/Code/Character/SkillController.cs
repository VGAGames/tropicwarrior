using UnityEngine;
using System.Collections;
using DG.Tweening;

public class SkillController : MonoBehaviour {

	public delegate void HandleOnDestroy();
	public static event HandleOnDestroy OnDestroy;

	public Transform particle;
	public AudioClip fxSoundHits, fxSoundActivate, fxSoundApache;

	private int numBullets;
	public float delayBullets, delayApacheApears;

	public Transform[] spawnPoints;


	void Start () 
	{
		GetComponent<SkeletonAnimation>().enabled = false;
		numBullets = spawnPoints.Length;

		GoogleFu.GameController.Instance.pauseSpawnWaves = true;

		MakingCameraZoom();

		foreach(EnemyController enemy in FindObjectsOfType(typeof(EnemyController)))
		{
			enemy.skeletonAnimation.enabled = false;
			enemy.velocity = 0;
		}

		StartCoroutine(ActivateSkill());

		
	}
	
	IEnumerator ActivateSkill()
	{
		SoundManager.Instance.Play2DSound(fxSoundActivate);
		yield return new WaitForSeconds(fxSoundActivate.length);
		GetComponent<SkeletonAnimation>().enabled = true;
		SoundManager.Instance.Play2DSound(fxSoundApache);

		yield return new WaitForSeconds(delayApacheApears);
		for(int i = 0; i < numBullets; i++)
		{
			SoundManager.Instance.Play2DSound(fxSoundHits);
			GameObject instance = CFX_SpawnSystem.GetNextObject(particle.gameObject);
			instance.transform.position = spawnPoints[i].position;

			yield return new WaitForSeconds(delayBullets);
		}

		foreach(EnemyController enemy in FindObjectsOfType(typeof(EnemyController)))
		{
			enemy.skeletonAnimation.enabled = true;
			enemy.velocity = 3;
			enemy.GetComponent<LifeModule>().DoDamage(1);
		}

		GoogleFu.GameController.Instance.pauseSpawnWaves = false;
		if(OnDestroy != null)
			OnDestroy();

		yield return new WaitForSeconds(2);

		Destroy(gameObject.transform.parent.gameObject);
	}

	void MakingCameraZoom()
	{
		CameraFit cameraFit = Camera.main.GetComponent<CameraFit>();
		GameObject UI = GameObject.FindGameObjectWithTag("UI");
		float sizeCamera = cameraFit.UnitsForWidth;

		UI.SetActive(false);

		Sequence mySequence = DOTween.Sequence();

		mySequence.Append(DOTween.To(x => cameraFit.UnitsForWidth = x, sizeCamera, 4f, 1f));
		mySequence.AppendInterval(1);
		mySequence.Append(DOTween.To(x => cameraFit.UnitsForWidth = x, 4, sizeCamera, 1f));
		mySequence.AppendInterval(1).OnComplete( ()=> {UI.SetActive(true); });


	}
}
