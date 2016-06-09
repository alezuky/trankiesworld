using UnityEngine;
using System.Collections;

public class DestroyProjectile : MonoBehaviour {

	void OnCollisionEnter(Collision other) {
		if (other.collider.gameObject.CompareTag("Projectile"))
		{
			Destroy (other.gameObject , 0f);
		}
	}


}
