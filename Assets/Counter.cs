using UnityEngine;
using System.Collections;

public class Counter : MonoBehaviour {


	public float initialTime;

	public AudioClip counterSound;

	public AudioClip goSound;

	private float timer;
	private float counter;

	private float soundTimer = 0;

	Animator anim;

	private bool playedGoSound = false;


	// Use this for initialization
	void Start () {

		anim = GetComponent<Animator>();    

	}
	
	// Update is called once per frame
	void Update () {

		if (!(anim.GetCurrentAnimatorStateInfo (0).IsName ("TimerInvisible"))) {

			timer += Time.deltaTime;

			soundTimer += Time.deltaTime;

			if (soundTimer > 0.9f && counter > 0) {
		
				AudioSource.PlayClipAtPoint (counterSound, transform.position);

				soundTimer = 0f;
		
			}

			counter = initialTime - timer;


			if (counter < 0) {

				if (playedGoSound)
				{
				AudioSource.PlayClipAtPoint (goSound, transform.position);
				playedGoSound = true;
				}

			}

		}
	}
}
