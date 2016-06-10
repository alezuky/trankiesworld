using UnityEngine;
using System.Collections;

public class DestroysTestKinect : MonoBehaviour {

	// Use this for initialization
	void Awake () {

		GameObject realKinect = GameObject.Find ("Kinect"); 

		if (realKinect) {
			GameObject.Destroy(this.gameObject);		
		}

		GameObject[] kinects = GameObject.FindGameObjectsWithTag ("Kinect");

		if (kinects.Length > 1) {

			if (this.gameObject.GetComponent<KinectManager>().player1tracked == true)
			{
			GameObject.Destroy(this.gameObject);			
			}
		}

	}
	

}
