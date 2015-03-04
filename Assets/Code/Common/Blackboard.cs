using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class Blackboard : MonoBehaviour {

	public Text touches;
	public AudioClip music;
	public static int totalTouches;

	private static Blackboard instance;
	public static Blackboard Instance 
	{
		get
		{
			if(instance == null)
			{
				string resourcesPrefabPath = "Blackboard";
				// Search in resources folder for this GameObject
				Blackboard managerPrefab = Resources.Load<Blackboard>(resourcesPrefabPath);
				
				if(managerPrefab == null)
				{
					Debug.LogError("[ERROR] Prefab "+resourcesPrefabPath+" not found in Resources directory");
					return null;
				}
				
				Instance = Instantiate(managerPrefab) as Blackboard;
			}
			
			return instance;
		}
		
		private set{
			instance = value;
		}
	}

	void Awake()
	{
		DOTween.Init();

		instance = this;

		touches.text = totalTouches.ToString();
		//if(GameObject.Find("MusicManager") == null)
		//	MusicManager.Instance.Play2DSound(music);
	}
	
	public void incrementTouches()
	{
		totalTouches++;
		touches.text = totalTouches.ToString();

	}

	public IEnumerator ChangeLevel()
	{
		float fadeTime = Camera.main.GetComponent<Fading>().BeginFade(1); 
		yield return new WaitForSeconds(fadeTime);
		Application.LoadLevel(Application.loadedLevel + 1);
	}
}
