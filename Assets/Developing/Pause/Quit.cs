using UnityEngine;
using System.Collections;

public class Quit : MonoBehaviour {

	public GestureListener kinect;
	
	public bool handsClosed = false;
	
	private float boxWidth = 270f;
	private float boxHeight = 40f;
	
	private float offsetDistance = 30f;
	
	public GUISkin skin;
	
	private bool hit = false;
	
	void UpdateBothHands(Color color) {
		//skin.customStyles[1].normal.textColor = color;
	}
	
	// Use this for initialization
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Projectile") {
			kinect = (GestureListener)GameObject.Find (other.gameObject.GetComponent<Projectile> ().owner).GetComponent<GestureListener> ();
			handsClosed = false;
			hit = true;
			
			Color colorToGo;
			if (kinect.player == 1) {
				colorToGo = Color.blue;
			} else {
				colorToGo = Color.red;
			}
			
			iTween.ValueTo (gameObject, iTween.Hash ("name", "ChangeColorBothHands", "from", Color.white, "to", colorToGo, "time", 0.4, "looptype", iTween.LoopType.pingPong, "onupdate", "UpdateBothHands"));
			
			StartEffectsNextStep ();
			Destroy (other.gameObject);
		}

	}
	
	void OnGUI () {
		if (skin && hit == true) {
			GUI.skin = skin;
			if (!handsClosed) {
				GUI.Box(new Rect((Screen.width - boxWidth)/2, Screen.height*3/4, boxWidth, boxHeight), "CLOSE BOTH HANDS TO CONTINUE", skin.customStyles[1]);
			} else {
				GUI.Box(new Rect((Screen.width - boxWidth)/2, Screen.height*3/4 - offsetDistance, boxWidth, boxHeight), "open right hand to CONTINUE", skin.customStyles[5]);
				GUI.Box(new Rect((Screen.width - boxWidth)/2, Screen.height*3/4 + offsetDistance, boxWidth, boxHeight), "open left hand to QUIT", skin.customStyles[6]);
			}
		}
	}
	
	void UpdateRightHandColor (Color color) {
		//skin.customStyles[5].normal.textColor = color;
	}
	
	void UpdateLeftHandColor (Color color) {
		//skin.customStyles[6].normal.textColor = color;
	}
	
	void StartEffectsNextStep () {
		iTween.ValueTo(gameObject, iTween.Hash("name", "ChangeColorReplay", "from", Color.magenta, "to", Color.red, "time", 0.4, "looptype", iTween.LoopType.pingPong, "OnUpdate", "UpdateRightHandColor"  ));
		UpdateLeftHandColor (Color.white);
		iTween.ValueTo(gameObject, iTween.Hash("name", "ChangeColorReplay", "from", Color.magenta, "to", Color.red, "time", 0.4, "looptype", iTween.LoopType.pingPong, "OnUpdate", "UpdateLeftHandColor"  ));
	}
	

	// Update is called once per frame
	void Update () {
		if (hit) {
			//validation to avoid crash
			if (kinect != null && kinect.tracked == true) {
			
				if (!handsClosed) {
					if (!kinect.SetFire () && !kinect.Propulsion ()) {
						handsClosed = true;
						StartEffectsNextStep ();
					}
				} else {
					//right hand -> CONTINUE
					if (kinect.SetFire ()) {
						//SendMessageUpwards("RestartGame");
						GameObject.Find ("Kinect").GetComponent<Pause> ().ContinueGame (kinect.player);
					}
					// left hand -> Quit
					if (kinect.Propulsion ()) {
						Application.Quit ();
					}
				}
			}
		}
	}
}