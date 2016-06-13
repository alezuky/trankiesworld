using UnityEngine;
using System.Collections;

public class LevelSelection : MonoBehaviour {
	public SceneManager scene;

	public GameObject player1;
	public GameObject player2;

	void Update() {
		Animator anim = GetComponent<Animator>();

		if (scene.IsP1Ready() || scene.IsP2Ready()) {
			anim.SetInteger("Players", 1);
		} else {
			anim.SetInteger("Players", 0);
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player")
		{
			scene.SelectLevel (other.gameObject, transform.parent.name);
			other.gameObject.SetActive (false);
		}
	}
}
