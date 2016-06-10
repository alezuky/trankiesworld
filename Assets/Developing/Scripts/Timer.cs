using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {
	
	
	public float startTime = 180f;	
	private float timeRemaining = 180f;
	public GUISkin skin;
	
	private float boxWidth = 90f;
	private float boxHeight = 60f;
	private float offsetTop = 10f;


	private Pause pause;

	IEnumerator StartDecreaser() {
		yield return new WaitForSeconds(1.0f);
		if (enabled) {
			Debug.Log("Chamou vez");
			InvokeRepeating ("decreaseTimeRemaining", 1.0f, 1.0f);
		}
	}

	void Start ()
	{
		SetEnable ();
		pause = GameObject.FindGameObjectWithTag ("Kinect").GetComponent<Pause> ();
	}

	
	public void OnEnable () {
	}
	
	public void OnDisable () {
		SetDisable ();
	}
	
	public void SetEnable () {
		enabled = true;
		UpdateAudioPitch(1);
		timeRemaining = startTime;
		StartCoroutine(StartDecreaser());
	}
	
	public void SetDisable () {
		CancelInvoke ();
		enabled = false;
	}
	
	void RestartGame () {
		SetDisable ();
		SetEnable ();
		
		Camera.main.GetComponent<AudioSource>().pitch = 1;
		
		Debug.Log("Restart Timer");
	}
	
	void OnGUI() {
		if (skin) {
			GUI.skin = skin;
			
			if (GameControl.GM.suddenDeath) {
				GUI.Box (new Rect((Screen.width - boxWidth*2.7f)/2, offsetTop, boxWidth*2.7f, boxHeight), "SUDDEN DEATH", skin.customStyles[3]);
			} else if (GameControl.GM.win) {
				if (GameControl.GM.winner == 1) {
					skin.customStyles[4].normal.textColor = Color.yellow;
				} else {
					skin.customStyles[4].normal.textColor = Color.red;
				}
				GUI.Box (new Rect((Screen.width - boxWidth*2.7f)/2, offsetTop, boxWidth*2.7f, boxHeight), "PLAYER " + GameControl.GM.winner + " WINS!", skin.customStyles[4]);
			} else {
				// convert value to a string to put on the box
				string timeStr = Mathf.FloorToInt(timeRemaining/60) + ":" + (timeRemaining % 60).ToString("00");
				skin.customStyles[0].normal.textColor = Color.black;
				GUI.Box (new Rect((Screen.width - boxWidth)/2, offsetTop, boxWidth, boxHeight), ""+timeStr, skin.customStyles[0]);
			}
		}
	}
	
	void decreaseTimeRemaining() {
		if (pause.isPaused != true) {
						timeRemaining--;
						Debug.Log ("decreasing");

				} else {
						Debug.Log ("Not decreasing");
				}

		if (timeRemaining == 0) {
			TimeFinished();
			CancelInvoke();
		}
	}
	
	void UpdateAudioPitch(float value) {
		Camera.main.GetComponent<AudioSource>().pitch = value;
	}
	
	private void UpdateSuddenDeathColor(Color color)
	{
		skin.customStyles[3].normal.textColor = color;
	}
	
	private void UpdateWinnerFont (float fontSize) {
		skin.customStyles[4].fontSize = (int) fontSize;
	}
	
	void TimeFinished () {
		if (GameControl.GM.p1points == GameControl.GM.p2points) {
			//prevent to create more than one iTween
			
			if (!GameControl.GM.suddenDeath) {
				
				Hashtable suddendParam = new Hashtable();
				suddendParam.Add("name", "SuddenDeathColor");
				suddendParam.Add("from", Color.yellow);
				suddendParam.Add("to", Color.red);
				suddendParam.Add("time", 0.25);
				suddendParam.Add("looptype", iTween.LoopType.pingPong);
				suddendParam.Add("onupdate", "UpdateSuddenDeathColor");
				iTween.ValueTo(gameObject, suddendParam);
				
				//change audio pitch
				iTween.ValueTo(gameObject, iTween.Hash("name", "AudioPitch", "from", 1, "to", 2, "time", 30, "onupdate", "UpdateAudioPitch"));
			}
			GameControl.GM.suddenDeath = true;
			
		} else if (GameControl.GM.p1points > GameControl.GM.p2points) {
			GameControl.GM.SetWinner(1);
		} else {
			GameControl.GM.SetWinner(2);
		}
		
	}
	
	void StartWinnerEffect () {
		iTween.ValueTo(gameObject, iTween.Hash("name", "WinnerFontUpdate", "from", 26, "to", 36, "time", 0.2, "looptype", iTween.LoopType.pingPong, "OnUpdate", "UpdateWinnerFont"));
	}
	
}
