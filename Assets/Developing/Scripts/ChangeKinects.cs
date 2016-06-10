using UnityEngine;
using System.Collections;

public class ChangeKinects : MonoBehaviour {

	public GameObject newKinect;

	private bool levelChanged;

	private string currentLevel;

	// Use this for initialization
	void Awake () {

		GameObject[] kinects = GameObject.FindGameObjectsWithTag ("Kinect");

		if (kinects.Length > 0)
		{
		foreach (GameObject k in kinects) {
		GameObject.Destroy (k);
		}
		
		}
			Debug.Log ("Changing Kinects");
			
			GameObject instantiedKinect = GameObject.Instantiate (newKinect);
			instantiedKinect.name = "Kinect";
			instantiedKinect.GetComponent<KinectManager> ().splash = false;


	}

}
