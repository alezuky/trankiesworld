using UnityEngine;
using System.Collections;

public class SpaceShipBoundingBox : MonoBehaviour {

	void OnCollisionEnter(Collision collision) {

		GameObject other = collision.collider.gameObject;

		if (other.tag == "Projectile") {
			Destroy(other);
		}
	}
}
