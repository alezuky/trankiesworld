﻿using UnityEngine;
using System.Collections;

public class TrainingEnded : MonoBehaviour {
	
	public GestureListener kinect;
	
	public bool handsClosed = false;
	
	private float boxWidth = 270f;
	private float boxHeight = 40f;
	
	private float offsetDistance = 30f;
	
	public GUISkin skin;
	
	public bool SetRestart = false;
	
	
	void UpdateBothHands(Color color) {
		skin.customStyles[1].normal.textColor = color;
	}
	
	// Use this for initialization
	void OnEnable () {
		kinect = (GestureListener) GameObject.Find("Player_"+GameControl.GM.winner).GetComponent<GestureListener>();
		handsClosed = false;
		
		Color colorToGo;
		if (GameControl.GM.winner == 1) {
			colorToGo = Color.blue;
		} else {
			colorToGo = Color.red;
		}
		
		iTween.ValueTo(gameObject, iTween.Hash("name", "ChangeColorBothHands", "from", Color.white, "to", colorToGo, "time", 0.4, "looptype", iTween.LoopType.pingPong, "onupdate", "UpdateBothHands"));
		
		StartEffectsNextStep();
	}
	
	void OnGUI () {
		if (skin) {
			GUI.skin = skin;
			if (!handsClosed) {
				GUI.Box(new Rect((Screen.width - boxWidth)/2, Screen.height*3/4, boxWidth, boxHeight), "CLOSE BOTH HANDS TO CONTINUE", skin.customStyles[1]);
			} else {
				GUI.Box(new Rect((Screen.width - boxWidth)/2, Screen.height*3/4 - offsetDistance, boxWidth, boxHeight), "open right hand to REPLAY", skin.customStyles[5]);
				GUI.Box(new Rect((Screen.width - boxWidth)/2, Screen.height*3/4 + offsetDistance, boxWidth, boxHeight), "open left hand to RESET", skin.customStyles[6]);
			}
		}
	}
	
	void UpdateRightHandColor (Color color) {
		skin.customStyles[5].normal.textColor = color;
	}
	
	void UpdateLeftHandColor (Color color) {
		skin.customStyles[6].normal.textColor = color;
	}
	
	void StartEffectsNextStep () {
		iTween.ValueTo(gameObject, iTween.Hash("name", "ChangeColorReplay", "from", Color.blue, "to", Color.red, "time", 0.4, "looptype", iTween.LoopType.pingPong, "OnUpdate", "UpdateRightHandColor"  ));
		UpdateLeftHandColor (Color.white);
		iTween.ValueTo(gameObject, iTween.Hash("name", "ChangeColorReplay", "from", Color.red, "to", Color.blue, "time", 0.4, "looptype", iTween.LoopType.pingPong, "OnUpdate", "UpdateLeftHandColor"  ));
	}
	
	void RestartGame () {
		this.enabled = false;
		handsClosed = false;
		iTween.Stop();
		SetRestart = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (SetRestart) {
			SetRestart = false;
			SendMessageUpwards("RestartGame");
		}
		//validation to avoid crash
		if(kinect != null && kinect.tracked == true) {
			
			if (!handsClosed) {
				if (!kinect.SetFire() && !kinect.Propulsion()) {
					handsClosed = true;
					StartEffectsNextStep();
				}
			} else {
				//right hand -> play again
				if (kinect.SetFire()) {
					//SendMessageUpwards("RestartGame");
					Application.LoadLevel (Application.loadedLevel);
				}
				// left hand -> quit
				if (kinect.Propulsion()) {
					Application.LoadLevel ("Spaceship_Level");
				}
			}
		}
	}
}
