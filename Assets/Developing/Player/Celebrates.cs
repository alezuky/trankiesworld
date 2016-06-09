using UnityEngine;
using System.Collections;

public class Celebrates : MonoBehaviour {
	
	private Animator anim;	
	private int shootHash = Animator.StringToHash ("shoot");
	public GameObject birth;
	public AudioClip winaudio;
	private float celebratecooldown;
	
	public bool winnerSet = false;
	public bool stopWinner = false;

	private bool mainmenu = false;
	
	// Use this for initialization
	void Start () {
		
		anim = gameObject.GetComponent<Animator>();
		celebratecooldown = 0;
		if (GameObject.Find ("GameControlGUI") == null)
			mainmenu = true;
		
	}
	
	void SetWinner() {
		stopWinner = false;
		winnerSet = true;
		GetComponent<Rigidbody>().useGravity = false;
	}
	
	void StopWinner() {
		winnerSet = false;
		stopWinner = true;
		
		GetComponent<Rigidbody>().useGravity = true;
		gameObject.RotateTo (new Vector3 (0, 0, 0), 0f, 0);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (mainmenu == false) {
			if (gameObject.name == "Player_" + GameControl.GM.winner) {
				if (!winnerSet) {
					SetWinner ();
				}
				celebratecooldown += Time.deltaTime;
				gameObject.RotateAdd (new Vector3 (0, 90, 0), 1f, 0);
				if (celebratecooldown > 0.9 && celebratecooldown < 1.1) {
					GetComponent<Rigidbody> ().velocity = new Vector3 (0, 0, 0);
					anim.SetTrigger (shootHash);
					GameObject instbirth = Instantiate (birth, transform.position, transform.rotation) as GameObject;								
					if (GameControl.GM.winner == 1) {
						instbirth.transform.GetComponent<ParticleSystem> ().startColor = Color.blue;
					} else {
						instbirth.transform.GetComponent<ParticleSystem> ().startColor = Color.red;
					}
					AudioSource.PlayClipAtPoint (winaudio, transform.position);
					celebratecooldown = 0;
				}
			} else {
				if (!stopWinner) {
					StopWinner ();
				}
			}
		}
	}
}
