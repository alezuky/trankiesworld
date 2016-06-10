using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {
	public ElevatorStarter elevatorP1;
	public ElevatorStarter elevatorP2;
	private KinectManager manager;

	private string levelSelected;
	private bool p1Ready;
	private bool p2Ready;

	public GameObject[] platforms;
	public GameObject player1;
	public GameObject player2;

	public Animator timerP1;
	public Animator timerP2;

	public Animator panelP1;
	public Animator panelP2;

	void Awake() {
		manager = GameObject.FindGameObjectWithTag ("Kinect").GetComponent<KinectManager> ();
		panelP1.SetBool("A_P1", true);
		panelP2.SetBool("A_P2", true);
	}

	void Update() {

		manager = GameObject.FindGameObjectWithTag ("Kinect").GetComponent<KinectManager> ();

		if (manager.player1added && !elevatorP1.enabled) {
			timerP1.SetBool("PlayerActive", true);
			panelP1.SetBool("A_P1", false);
			panelP1.SetBool("B", true);
			elevatorP1.enabled = true;
		}

		if (manager.player2added && !elevatorP2.enabled) {
			timerP2.SetBool("PlayerActive", true);
			panelP2.SetBool("A_P2", false);
			panelP2.SetBool("B", true);
			elevatorP2.enabled = true;
		}

		if (p1Ready && p2Ready) Application.LoadLevel(levelSelected);
	}

	public void SelectLevel(GameObject player, string platformName) {
		if (player.name == player1.name) {
			p1Ready = true;
		} else if (player.name == player2.name) {
			p2Ready = true;
		}

		for (int i = 0; i < platforms.Length; i++) {
			if (platforms[i].name != platformName) {
				Transform selection = platforms[i].transform.FindChild("Level Selection");
				selection.gameObject.SetActive(false);
			} else {
				levelSelected = PlatformToLevelName(platformName);
				Debug.Log(levelSelected);
			}
		}
	}

	private string PlatformToLevelName(string platformName) {
		switch(platformName) {
		case "Platform_Forest_Easy":
			return "Forest_Level";
		case "Platform_Desert_Medium":
			return "Desert_Level";
		case "Platform_Ice_Hard":
			return "Ice_Level";
		case "Platform_Sky_Expert":
			return "Sky_Level";
		default:
			return "";
		}
	}

	public bool IsP1Ready() {
		return p1Ready;
	}

	public bool IsP2Ready() {
		return p2Ready;
	}
}
