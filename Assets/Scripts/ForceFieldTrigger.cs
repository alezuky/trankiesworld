using UnityEngine;
using System.Collections;

public class ForceFieldTrigger : MonoBehaviour {
	public GameObject forceField;
	public GameObject player;
	public GameObject explosion;
	public AudioClip soundFx;
	public bool triggerExplosion;
	public Animator panel;
	
	void Update() {
		if (triggerExplosion) {
			triggerExplosion = !triggerExplosion;
			Explosion();
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag("Projectile") && other.GetComponent<Projectile>().owner == player.name)
		    {
			Debug.Log("Explosion");
			Explosion();
		}
	}

	void Explosion() {
		Instantiate(explosion, transform.position, transform.rotation);
		AudioSource.PlayClipAtPoint(soundFx, transform.position);
		forceField.GetComponent<Animator>().SetBool("Activated", false);
		player.GetComponent<CharController>().enabled = true;

		panel.SetBool("B", false);
		panel.SetBool("C", true);
		Destroy(gameObject);
	}
}
