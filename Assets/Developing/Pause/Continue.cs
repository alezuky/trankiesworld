using UnityEngine;
using System.Collections;

public class Continue : MonoBehaviour {
	
		void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Projectile" && other.gameObject.GetComponent<Projectile>().owner == "Player_1") {
			GameObject.FindGameObjectWithTag ("Kinect").GetComponent<Pause> ().ContinueGame (1);
			
		} else 	if (other.gameObject.tag == "Projectile" && other.gameObject.GetComponent<Projectile>().owner == "Player_2") {
			GameObject.FindGameObjectWithTag ("Kinect").GetComponent<Pause> ().ContinueGame (2);			
		}
	}
}
