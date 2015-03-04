#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections;
using System;

public class AutoSave : EditorWindow 
{
	public bool saveOnPlay				=	false;
	public bool autoSaveScene			=	false;
	public bool autoSaveProject			= 	false;
	public bool showMessage 			=	true;
	
	public int minutesBetweenSaves=1;
	
	private DateTime lastSaveTime = DateTime.Now;
	private bool isStarted = false;
	private string scenePath;
	
	[MenuItem ("Window/AutoSave")]
	static void Init () 
	{
		AutoSave saveWindow = (AutoSave)EditorWindow.GetWindow (typeof (AutoSave));
		saveWindow.Show();
	}
	
	void OnGUI()
	{
		EditorGUILayout.LabelField("Scene path: "+scenePath);
		
		autoSaveScene = EditorGUILayout.BeginToggleGroup ("Auto save scene", autoSaveScene);
		EditorGUILayout.EndToggleGroup();
		showMessage = EditorGUILayout.BeginToggleGroup ("Show Message", showMessage);
		EditorGUILayout.EndToggleGroup ();
		saveOnPlay = EditorGUILayout.BeginToggleGroup ("Save on Play", saveOnPlay);
		EditorGUILayout.EndToggleGroup ();
		autoSaveProject = EditorGUILayout.BeginToggleGroup ("Auto save project", autoSaveProject);
		EditorGUILayout.EndToggleGroup ();
		
		minutesBetweenSaves = EditorGUILayout.IntSlider ("Interval (minutes)", minutesBetweenSaves, 1, 10);
		if(isStarted) {
			EditorGUILayout.LabelField ("Last save:", ""+lastSaveTime);
		}
		
		
		
	}
	
	void Update(){
		scenePath = EditorApplication.currentScene;
		if(autoSaveScene || autoSaveProject) {
			if(DateTime.Now.Minute >= (lastSaveTime.Minute+minutesBetweenSaves)
			   || DateTime.Now.Minute == 59 && DateTime.Now.Second == 59)
			{
				if(autoSaveScene)
					SaveScene();
				if(autoSaveProject)
				{
					bool correctSave = SaveAssets();
					if(showMessage)
					{
						if (correctSave) 
							Debug.Log("Assets saved correctly");
						else
							Debug.Log("Couldn't save assets");
					}
				}
			}
		} else 
		{
			isStarted = false;
		}
		
		if(saveOnPlay && EditorApplication.isPlayingOrWillChangePlaymode && !EditorApplication.isPlaying)
		{
			Debug.Log("Auto-Saving scene before entering Play mode: " + EditorApplication.currentScene);			
			SaveScene();
		}		
	}
	
	
	void SaveScene()
	{
		lastSaveTime = DateTime.Now;
		
		if(showMessage)
		{
			Debug.Log("Scene saved:  "+ scenePath);
		}
		EditorApplication.SaveScene(scenePath);
		AutoSave repaintSaveWindow = (AutoSave)EditorWindow.GetWindow (typeof (AutoSave));
		repaintSaveWindow.Repaint(); 
		isStarted = true;
		
	}
	
	bool SaveAssets()
	{
		lastSaveTime = DateTime.Now;
		
		if (EditorApplication.isPlayingOrWillChangePlaymode || EditorApplication.isCompiling || EditorApplication.isPaused)
		{
			return false;
		}
		EditorApplication.SaveAssets();
		
		AutoSave repaintSaveWindow = (AutoSave)EditorWindow.GetWindow (typeof (AutoSave));
		repaintSaveWindow.Repaint(); 
		
		isStarted = true;
		
		return true;
	}
	
	
}
#endif