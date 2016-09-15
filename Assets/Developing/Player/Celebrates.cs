using UnityEngine;
using System.Collections;

public class Celebrates : MonoBehaviour {
	
	private Animator anim;	

	private int shootHash = Animator.StringToHash ("shoot");

	private int celebrate_1Hash = Animator.StringToHash ("celebrate_1");
	private int celebrate_2Hash = Animator.StringToHash ("celebrate_2");

	public GameObject birth;
	public AudioClip winaudio;
	private float celebratecooldown;
	
	public bool winnerSet = false;
	public bool stopWinner = false;

	private bool mainmenu = false;

	private int random_celebration = 0;
	
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


		//Two types of celebration
		random_celebration = Random.Range(0,3);
		Debug.Log ("celebration = " + random_celebration.ToString());



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
				gameObject.RotateAdd (new Vector3 (0, 90, 0), 1.5f, 0);
				if (celebratecooldown > 1.4 && celebratecooldown < 1.6) {
					GetComponent<Rigidbody> ().velocity = new Vector3 (0, 0, 0);


					if (random_celebration == 1)
					{
						anim.SetTrigger (celebrate_1Hash);
					}
					else
					{
						anim.SetTrigger (celebrate_2Hash);
					}




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
