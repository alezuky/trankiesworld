using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour {

	public GameObject pauseplatform;

	public GameObject player;
	private GameObject otherplayer;
	private Zoom cam;

	private Vector3 originalposition = new Vector3(0,0,0);
	private Vector3 otheroriginalposition = new Vector3(0,0,0);

	public bool isPaused = false;

	private Timer timer;



public void PauseGame ( int p )
	{
		isPaused = true;
		cam = GameObject.Find ("Main Camera").GetComponent<Zoom>();

		Debug.Log ("Game Paused player: " + p);
		if (p == 1) {
			player = GameObject.Find ("Player_1");
			otherplayer = GameObject.Find ("Player_2");

		} else if (p == 2) {
			player = GameObject.Find ("Player_2");
			otherplayer = GameObject.Find ("Player_1");
		}

		originalposition = player.transform.position;
		otheroriginalposition = otherplayer.transform.position;



		otherplayer.GetComponent<Renderer>().enabled = false;
		otherplayer.GetComponent<Collider>().isTrigger = true;
		otherplayer.GetComponent<Rigidbody>().useGravity = false;
		otherplayer.GetComponent<GestureListener>().enabled = false;

		GameObject platform = Instantiate (pauseplatform, GameObject.Find ("PausePosition").transform.position, this.transform.rotation ) as GameObject;
		player.transform.position = platform.transform.position;
		cam.Zoomat (player);
	}

	public void ContinueGame ( int p )
	{
		isPaused = false;
		cam = GameObject.Find ("Main Camera").GetComponent<Zoom>();
		
		Debug.Log ("Game Continued player: " + p);
		if (p == 1) {
			player = GameObject.Find ("Player_1");
			otherplayer = GameObject.Find ("Player_2");
			
		} else if (p == 2) {
			player = GameObject.Find ("Player_2");
			otherplayer = GameObject.Find ("Player_1");
		}
		
		otherplayer.GetComponent<Collider>().isTrigger = false;
		otherplayer.GetComponent<Rigidbody>().useGravity = true;
		otherplayer.GetComponent<GestureListener>().enabled = true;
		
		Destroy(GameObject.Find("PauseScene(Clone)").gameObject);
		otherplayer.transform.position = otheroriginalposition;
		otherplayer.GetComponent<Renderer>().enabled = true;
		player.transform.position = originalposition;
		cam.Zoomout();
	}




}

