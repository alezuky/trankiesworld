using UnityEngine;
using System.Collections;

public class DeathZone : MonoBehaviour {
	public Texture2D deathFlash;

	public GameObject explosion;
	public GameObject birth;

	public AudioClip hitdeath;

	void Start() {
		iTween.CameraFadeAdd(deathFlash,100);	
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			iTween.CameraFadeTo(iTween.Hash("amount", .6, "time", .05));
			iTween.CameraFadeTo(iTween.Hash("amount", 0, "time", 1.6, "delay", .05, "easetype", "linear"));

			Instantiate (explosion, other.transform.position, other.transform.rotation);		

			//resets player to origin position
			other.gameObject.SendMessage("KillPlayer");
			//other.rigidbody.Sleep ();
			//other.rigidbody.velocity = Vector3.zero;
			//other.transform.position = other.GetComponent<CharController> ().startposition;
			//other.rigidbody.WakeUp ();
			Instantiate(birth, other.transform.position, other.transform.rotation);

			// If you are good enough to hit the death zone, you will lose 1 point
			switch (other.name) {
			case "Player_1": 
				GameControl.GM.p1points = GameControl.GM.p1points - 1;
				if (GameControl.GM.suddenDeath) GameControl.GM.SetWinner(2);
				break;
			case "Player_2": 
				GameControl.GM.p2points = GameControl.GM.p2points - 1;
				if (GameControl.GM.suddenDeath) GameControl.GM.SetWinner(1);
				break;
			}

			AudioSource.PlayClipAtPoint(hitdeath, other.transform.position);
		} else {
			Instantiate(explosion, other.transform.position, other.transform.rotation);
			Destroy(other.gameObject);
		}
	}
}
