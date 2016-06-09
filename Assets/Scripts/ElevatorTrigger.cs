using UnityEngine;
using System.Collections;

public class ElevatorTrigger : MonoBehaviour {
	private ElevatorStarter elevator;

	void Awake() {
		elevator = GetComponentInParent<ElevatorStarter>();
	}

	public void PlayerSighted() {
		elevator.ActivateForceField();
	}
}
