using UnityEngine;
using System.Collections;

public class ForceFIeldSounds : MonoBehaviour {
	public AudioClip activating;
	public AudioClip deactivating;

	public void PlaySound() {
		GetComponent<AudioSource>().Play();
	}

	public void StopSound() {
		GetComponent<AudioSource>().Stop();
	}

	public void PlaySoundActivation() {
		GetComponent<AudioSource>().PlayOneShot(activating);
	}

	public void PlaySoundDeactivation() {
		GetComponent<AudioSource>().PlayOneShot(deactivating);
	}
}
