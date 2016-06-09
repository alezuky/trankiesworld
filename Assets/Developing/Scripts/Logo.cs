using UnityEngine;
using System.Collections;

public class Logo : MonoBehaviour {

	public GUISkin skin;

	private bool levelLoaded = false;

	private KinectManager manager;

	IEnumerator LoadGame() {
		Debug.Log("Carregando");
		yield return new WaitForSeconds(3.0f);
		levelLoaded = true;

	}

	void Start () {

		manager = GameObject.Find ("Kinect").GetComponent<KinectManager> () ?? KinectManager.Instance;

		StartCoroutine("LoadGame");
	}

	void OnGUI () {
		if (skin) {
			GUI.skin = skin;
			GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");
		}
	}
	
	// Update is called once per frame
	void Update () {
	

		if (levelLoaded == true && manager.IsKinectInitialized ()) {
			Application.LoadLevel ("Credits");


		}

	}
}
