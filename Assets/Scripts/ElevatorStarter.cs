using UnityEngine;
using System.Collections;

public class ElevatorStarter : MonoBehaviour {
	private Transform elevator;
	private bool playerSighted = false;

	public GameObject playerAvatar;
	public Vector3 playerPosition;
	public Animator forceField;
	public Animator bombBall;

	public Animator enemyTimer;
	public float soundDelay = 0.9f;

	void Awake() {
		elevator = transform.FindChild("ElevatorB");
	}
		
	void Start() {
		elevator.GetComponent<Animator>().SetTrigger("PlayerAdded");
		playerAvatar.transform.parent = elevator;
		GetComponent<AudioSource>().PlayDelayed(soundDelay);
	}

	public void ActivateForceField() {
		enemyTimer.SetBool("EnemyActive", true);

		playerAvatar.transform.parent = null;
		forceField.SetBool("Activated", true);
		bombBall.SetTrigger("Start");
	}
}
