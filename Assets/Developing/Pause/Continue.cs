using UnityEngine;
using System.Collections;

public class Continue : MonoBehaviour {
	
		void OnTriggerEnter(Collider other) {
		if (other.gameObject.name == "Projectile(Clone)" && other.gameObject.GetComponent<Projectile>().owner == "Player_1") {
			GameObject.Find ("Kinect").GetComponent<Pause> ().ContinueGame (1);
			
		} else 	if (other.gameObject.name == "Projectile(Clone)" && other.gameObject.GetComponent<Projectile>().owner == "Player_2") {
			GameObject.Find ("Kinect").GetComponent<Pause> ().ContinueGame (2);			
		}
	}
}
