﻿using UnityEngine;
using System.Collections;

public class ProvTrainingLevel : MonoBehaviour {

	

	private bool levelLoaded = false;
	
	private KinectManager manager;
	
	IEnumerator LoadGame() {
		Debug.Log("Carregando");
		yield return new WaitForSeconds(10.0f);
		levelLoaded = true;
		
	}
	
	void Start () {
		
		StartCoroutine("LoadGame");
		manager = GameObject.Find ("Kinect").GetComponent<KinectManager> () ?? KinectManager.Instance;
		manager.splash = true;
	}
	

	// Update is called once per frame
	void Update () {
				
		if (levelLoaded == true ) {
			manager.splash = false;
			Application.LoadLevel ("Spaceship_Level");
			
			
		}
		
	}
}