using UnityEngine;
using System.Collections;

public class LevelSelection : MonoBehaviour {
	public SceneManager scene;

	public GameObject player1;
	public GameObject player2;

	private float timeLoad = 5;

	private float counter;


	public GameObject clock;
	private GameObject instantiatedClock;

	public GameObject clockPosition;


	void Start()
	{
		counter = timeLoad;
	}

	void Update() {
		Animator anim = GetComponent<Animator>();

		if (scene.IsP1Ready() || scene.IsP2Ready()) {
			anim.SetInteger("Players", 1);
		} else {
			anim.SetInteger("Players", 0);
		}
	}


	void OnTriggerEnter(Collider other) {
		
		if (other.tag == "Player") {
			Debug.Log("Clock!");
			instantiatedClock = GameObject.Instantiate (clock, clockPosition.transform.position , clockPosition.transform.rotation) as GameObject;
			instantiatedClock.gameObject.GetComponent<Animator>().SetTrigger("EnemyActive");

		}
		}


	void OnTriggerStay(Collider other) {

		if (other.tag == "Player") {
			counter -= Time.deltaTime;
		}

		if (other.tag == "Player" && counter < 0) {
			scene.SelectLevel (other.gameObject, transform.parent.name);
			other.gameObject.SetActive (false);

			counter = timeLoad;
			if (instantiatedClock) Destroy (instantiatedClock);
			}
	}

	void OnTriggerExit(Collider other) {
		if (other.tag == "Player") {
			counter = timeLoad;
			if (instantiatedClock) Destroy (instantiatedClock);
		}

	}



}
