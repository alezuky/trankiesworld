using UnityEngine;
using System.Collections;

public class InstructionPause : MonoBehaviour {

	private KinectManager manager;

	private Pause pause;

	private int player;

	public GameObject inst;
	public GameObject inst2;
	public GameObject inst3;
	public GameObject inst4;

	private GameObject instruction;

	GameObject position;

	GameObject position2;

	void Awake (){

		manager = GameObject.FindGameObjectWithTag ("Kinect").GetComponent<KinectManager> ();

		pause = GameObject.FindGameObjectWithTag("Kinect").GetComponent<Pause> ();

		}


	// Use this for initialization
	void Start () {
	
				if (pause.player.name.CompareTo ("Player_1") == 0)
						player = 1;
				else if (pause.player.name.CompareTo ("Player_2") == 0)
						player = 2;

				position = GameObject.Find ("Pause_InstructionsPanel");

				position2 = GameObject.Find ("Pause_InstructionsPanel2");

				if (player == 1 && manager.player1tracked == false) {
						instruction = Instantiate (inst, position.transform.position, position.transform.rotation) as GameObject;
						instruction.transform.parent = gameObject.transform;
				} else if (player == 2 && manager.player2tracked == false) {
						instruction = Instantiate (inst2, position2.transform.position, position2.transform.rotation) as GameObject;
						instruction.transform.parent = gameObject.transform;
				} else if (player == 1) {
						instruction = Instantiate (inst3, position.transform.position, position.transform.rotation) as GameObject;
						instruction.transform.parent = gameObject.transform;
				} else if (player == 2) {
						instruction = Instantiate (inst4, position2.transform.position, position2.transform.rotation) as GameObject;
						instruction.transform.parent = gameObject.transform;
				}
		}
	

	void Update (){

		manager = GameObject.FindGameObjectWithTag ("Kinect").GetComponent<KinectManager> ();

		if (manager.player1tracked == true)
		{
			Destroy (instruction);
			GameObject instruction2 = Instantiate (inst3, position.transform.position, position.transform.rotation) as GameObject;
			instruction2.transform.parent = gameObject.transform;
		}
		else if (manager.player2tracked == true)
		{
			Destroy (instruction);
			GameObject instruction2 = Instantiate (inst4, position2.transform.position, position2.transform.rotation) as GameObject;
			instruction2.transform.parent = gameObject.transform;
		}

	}
}
