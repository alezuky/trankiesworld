using UnityEngine;
using System.Collections;

public class TrainingControl : MonoBehaviour {
	
	// explanation about this class here:
	// https://www.youtube.com/watch?v=J6FfcJpbPXE#t=805
	
	public static TrainingControl GM;
	
	public int p1points;
	public int p2points;
	

	public bool win = false;
	
	public int winner = 0;
	
	public GUISkin skin;
	
	
	public float playerWidthBox = 70;
	public float playerHeightBox = 70;
	public float playerOffsetBox = 10;
	private float fontSize = 36f;
	
	private Zoom zoom;

	private KinectManager manager;

	private int added = 0;
	
	public AudioClip victory;
	
	void Awake () {
		if (GM == null) {
			//DontDestroyOnLoad(gameObject);
			GM = this;
		} else if (GM != this) {
			Destroy(gameObject);
		}

		manager = GameObject.FindGameObjectWithTag ("Kinect").GetComponent<KinectManager> ();

		if (manager.player1tracked != true)
		{
			Destroy (GameObject.Find ("Player_1").gameObject);
			added = 2;
		}
		else if (manager.player2tracked != true)
		{
			Destroy(GameObject.Find("Player_2").gameObject);
			added  = 1;
		}

	}
	
	void OnGUI () {
		if (skin) {
			GUI.skin = skin;
			if (added == 1)
			{
			GUI.color = Color.blue;
			GUI.Box(new Rect(playerOffsetBox, playerOffsetBox, playerWidthBox, playerHeightBox), "PLAYER 1\n<size=" + fontSize + ">" + p1points + "</size>", skin.customStyles[0]);
			}else if (added == 2)
			{
			GUI.color = Color.red;
			GUI.Box(new Rect(Screen.width - playerOffsetBox - playerWidthBox, playerOffsetBox, playerWidthBox, playerHeightBox), "PLAYER 2\n<size=" + fontSize + ">" + p2points + "</size>", skin.customStyles[0]);
			}
			//	GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "TRANKIES WORLD", skin.customStyles[1]);
		}
	}
	

	void SetTitleFont(float fontSize) {
		skin.customStyles[1].fontSize = (int) fontSize;
	}
	
	void Start () {
		SetTitleFont(1);
		#if UNITY_EDITOR
		iTween.ValueTo(gameObject, iTween.Hash("from", 1, "to", 78, "time", 0.2, "delay", 0.2, "onupdate", "SetTitleFont"));
		iTween.ValueTo(gameObject, iTween.Hash("from", 78, "to", 1, "time", 0.2, "delay", 0.5, "onupdate", "SetTitleFont", "oncomplete", "StartTutorial"));
		#else
		iTween.ValueTo(gameObject, iTween.Hash("from", 1, "to", 78, "time", 1, "delay", 1, "onupdate", "SetTitleFont"));
		iTween.ValueTo(gameObject, iTween.Hash("from", 78, "to", 1, "time", 1, "delay", 3.5, "onupdate", "SetTitleFont", "oncomplete", "StartTutorial"));
		#endif
		
		zoom = (Zoom) Camera.main.GetComponent("Zoom");
	}

	void RestartGame () {
		GameControl.GM.p1points = 0;
		GameControl.GM.p2points = 0;
		GameControl.GM.win = false;
		GameControl.GM.winner = 0;
		GameControl.GM.suddenDeath = false;
		Debug.Log("Restart Game Control");
		
		// reset control of players
		CharController.playing = true;
		//Shoots.playing = true;
		
		GameObject.Find("Player_1").gameObject.SendMessage("KillPlayer");
		GameObject.Find("Player_2").gameObject.SendMessage("KillPlayer");
		
		zoom.Zoomout ();
		
		// reset player on camera follow
		Camera.main.SendMessage("RestartGame");
		
	}

	
	IEnumerator EnableEnded() {
		GameEnded ge = (GameEnded) this.GetComponent("GameEnded");
		yield return new WaitForSeconds(5.0f);
		ge.enabled = true;
	}
	
	public void SetWinner (int player) {
		win = true;
		winner = player;
		
		// disable sudden death message
		//suddenDeath = false;
		
		/*cf = (CameraFollow) Camera.main.GetComponent("CameraFollow");
		
		cf.smooth = 5;
		
		switch (winner) {
		case 1 :
			cf.p2 = cf.p1;
			break;
		case 2 :
			cf.p1 = cf.p2;
			break;
		}*/
		
		
		zoom.Zoomat(GameObject.Find ("Player_"+player));
		
		
		
		// change audio pitch of game
		
		
		//if (suddenDeath == true) iTween.StopByName("AudioPitch"); // if it is sudden death pitch is changing, so stop it
		//Camera.main.GetComponent<AudioSource>().pitch = 2.5f;
		Camera.main.GetComponent<AudioSource> ().enabled = false;
		AudioSource.PlayClipAtPoint (victory, transform.position);
		
		
		// start text winner effect
		SendMessageUpwards("StartWinnerEffect");
		
		// disable control of players
		CharController.playing = false;
		//Shoots.playing = false;
		
		StartCoroutine(EnableEnded());
		
		
	}
	
}
