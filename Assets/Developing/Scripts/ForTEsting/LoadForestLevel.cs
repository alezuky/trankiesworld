using UnityEngine;
using System.Collections;

public class LoadForestLevel : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.name == "Projectile(Clone)" && other.gameObject.GetComponent<Projectile>().owner == "Player_1") {
			Application.LoadLevel("Forest_Level");
			
		}
	}
}
