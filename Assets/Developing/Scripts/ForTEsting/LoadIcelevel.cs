using UnityEngine;
using System.Collections;

public class LoadIcelevel : MonoBehaviour {
	
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.name == "Projectile(Clone)" && other.gameObject.GetComponent<Projectile>().owner == "Player_1") {
			Application.LoadLevel("Ice_Level");
			
		}
	}
}