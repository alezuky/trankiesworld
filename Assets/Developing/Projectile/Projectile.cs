using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public float duration = 3f;
	public string owner;
	private float shotcounter;
	private bool shotcounterset = false;
	public GameObject birth;
	public GameObject projexplosion;

	public GameObject explosion;

	public AudioClip die;
	public AudioClip rebate;

	private bool mainmenu = false;

	void Start(){
		Destroy (this.gameObject, duration);
		shotcounter = 0;

		if (GameObject.Find ("GameControlGUI") == null || GameObject.Find ("Kinect").GetComponent<Pause> ().isPaused == true)
			mainmenu = true;

	}


	void FixedUpdate(){

		//lock the z position for 2D:
		Vector3 tempPos = transform.position;
		tempPos.z=0;
		transform.position=tempPos;

		shotcounter += Time.deltaTime;

		Checkcounter ();
	}

	// Allows collision with yourself after 0.5 seconds you fired 
	// Also instantiate a little explosion when the object is destructed
	void Checkcounter(){
				if (shotcounter > 0.5f && shotcounterset == false) {
						Physics.IgnoreCollision (transform.GetComponent<Collider>(), GameObject.Find (owner).GetComponent<Collider>(), false);
						shotcounterset = true;
				}

		/*if (shotcounter > duration - 0.1f) {
			Instantiate (projexplosion, transform.position, transform.rotation);
		}*/
	
	}
	

	void OnCollisionEnter(Collision collisionInfo) {
		if (mainmenu == false)
		{
		if (collisionInfo.collider.tag == "Player") {
			if (owner != collisionInfo.collider.name) {
				switch (owner) {
				case "Player_1": 
					GameControl.GM.p1points = GameControl.GM.p1points + 3;
					if (GameControl.GM.suddenDeath) {
						GameControl.GM.SetWinner (1);
					}
					break;
				case "Player_2": 
					GameControl.GM.p2points = GameControl.GM.p2points + 3;
					if (GameControl.GM.suddenDeath) {
						GameControl.GM.SetWinner (2);
					} 
					break;
				}
			}
			// If you are good enough to kill yourself, you will lose 2 points
			else {
				switch (owner) {
				case "Player_1": 
					if (GameControl.GM.suddenDeath) {
						GameControl.GM.SetWinner (2);
					} else {
						GameControl.GM.p1points = GameControl.GM.p1points - 2;
					}
					break;
				case "Player_2": 
					if (GameControl.GM.suddenDeath) {
						GameControl.GM.SetWinner (1);
					} else {
						GameControl.GM.p2points = GameControl.GM.p2points - 2;
					}
					break;
				}
			}

			//resets player to origin position
			Instantiate (explosion, collisionInfo.collider.transform.position, collisionInfo.collider.transform.rotation);
			AudioSource.PlayClipAtPoint (die, collisionInfo.collider.transform.position);


			collisionInfo.gameObject.SendMessage ("KillPlayer");

			//collisionInfo.rigidbody.Sleep();
			//collisionInfo.rigidbody.velocity=Vector3.zero;
			//collisionInfo.transform.position= collisionInfo.gameObject.GetComponent<CharController>().startposition;
			//collisionInfo.rigidbody.WakeUp();

			Instantiate (birth, collisionInfo.collider.transform.position, collisionInfo.collider.transform.rotation);

			Destroy (gameObject);
		} else {
			AudioSource.PlayClipAtPoint (rebate, collisionInfo.collider.transform.position);
		}
		if (collisionInfo.collider.tag == "Tutorial") {
			Destroy (gameObject);
		}
	}
	}

}
