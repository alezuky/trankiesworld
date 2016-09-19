using UnityEngine;
using System.Collections;

public class EnergyField : MonoBehaviour {

	public GameObject player;

	public Vector3 expelingforce = new Vector3(0,200000f,0);

	void OnTriggerExit(Collider other) {
		if (other.gameObject.CompareTag("Projectile"))
		{
			Destroy (other.gameObject);
		}

	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("Projectile") && other.GetComponent<Projectile> ().owner == player.name) {
			Destroy (other.gameObject);
		}

	}

	void OnTriggerStay(Collider other) {
 if (other.gameObject.CompareTag ("Player") && other.name == player.name) {
			Debug.Log("Go away");
			other.GetComponent<Rigidbody>().AddForce(expelingforce);
		}
		
	}


}
