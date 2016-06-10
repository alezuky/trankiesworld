using UnityEngine;
using System.Collections;

public class Credits : MonoBehaviour {

	public GUISkin skin;
	
	private bool levelLoaded = false;
	
	private KinectManager manager;
	
	IEnumerator LoadGame() {
		Debug.Log("Carregando");
		yield return new WaitForSeconds(5.0f);
		levelLoaded = true;
		
	}
	
	void Start () {
		
			StartCoroutine("LoadGame");
		manager = GameObject.FindGameObjectWithTag("Kinect").GetComponent<KinectManager> ();
	}
	
	void OnGUI () {
		if (skin) {
			GUI.skin = skin;
			GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		
		if (levelLoaded == true ) {
			manager.splash = false;
			Application.LoadLevel ("Spaceship_Level");
			
			
		}
		
	}
}
