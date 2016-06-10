using UnityEngine;
using System.Collections;

public class LoadMainLevel : MonoBehaviour 
{
	private bool levelLoaded = false;
	
	
	void Update() 
	{
		KinectManager manager = GameObject.FindGameObjectWithTag ("Kinect").GetComponent<KinectManager> ();
		
		if(!levelLoaded && manager && manager.IsKinectInitialized())
		{
			levelLoaded = true;
			Application.LoadLevel(1);
		}
	}
	
}
