using UnityEngine;
using System.Collections;

public class MachineProjectile : MonoBehaviour {

	public float duration = 5f;
	public string owner;
	//private float shotcounter;
	//private bool shotcounterset = false;
	//public GameObject birth;
	//public GameObject projexplosion;
	
	public GameObject explosion;
	
	public AudioClip die;
	public AudioClip rebate;

	private Zoom zoom;

	//private bool mainmenu = false;
	
	void Start(){
		Destroy (this.gameObject, duration);
		//shotcounter = 0;
		zoom = GameObject.Find ("Main Camera").GetComponent<Zoom>();

	}
	
	
	void FixedUpdate(){
		
		//lock the z position for 2D:
		Vector3 tempPos = transform.position;
		tempPos.z=0;
		transform.position=tempPos;
		
		//shotcounter += Time.deltaTime;
		
		//Checkcounter ();
	}
	
	// Allows collision with yourself after 0.5 seconds you fired 
	// Also instantiate a little explosion when the object is destructed
	/*void Checkcounter(){
		if (shotcounter > 0.5f && shotcounterset == false) {
			Physics.IgnoreCollision (transform.GetComponent<Collider>(), GameObject.Find (owner).GetComponent<Collider>(), false);
			shotcounterset = true;
		}
		

	}*/
	
	
	void OnCollisionEnter(Collision collisionInfo) {

			if (collisionInfo.collider.tag == "Player") {

				
				//resets player to origin position
				Instantiate (explosion, collisionInfo.collider.transform.position, collisionInfo.collider.transform.rotation);
				AudioSource.PlayClipAtPoint (die, collisionInfo.collider.transform.position);
				
				
				//collisionInfo.gameObject.SendMessage ("KillPlayer");
				
				//collisionInfo.rigidbody.Sleep();
				//collisionInfo.rigidbody.velocity=Vector3.zero;
				//collisionInfo.transform.position= collisionInfo.gameObject.GetComponent<CharController>().startposition;
				//collisionInfo.rigidbody.WakeUp();
				
				//Instantiate (birth, collisionInfo.collider.transform.position, collisionInfo.collider.transform.rotation);
				zoom.Zoomat(collisionInfo.collider.gameObject);
				Destroy (gameObject);
				Destroy (collisionInfo.collider.gameObject);
				


			} else {
				AudioSource.PlayClipAtPoint (rebate, collisionInfo.collider.transform.position);
			}

		}
	}
	
