using UnityEngine;
using System.Collections;

public class EnergyField : MonoBehaviour {

	void OnTriggerExit(Collider other) {
		if (other.gameObject.CompareTag("Projectile"))
		{
			Destroy (other.gameObject);
		}
	}
}
