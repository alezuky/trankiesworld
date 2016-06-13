using UnityEngine;
using System.Collections;
using System;
//using Windows.Kinect;

public class GestureListener : MonoBehaviour, KinectGestures.GestureListenerInterface
{
	// GUI Text to display the gesture messages.
	public GUIText GestureInfo;

	public KinectManager manager;

	public bool tracked = false;

	public Int64 user = 0;

	public int player = 0;

	//distance between shoulder's and hand's joints
	public float distance = 0;
	
	//distance between the current position of the hand and its last one
	public Vector3 auxiliarydistance;
	
	//last position registred for the hand
	public Vector3 lasthandposition;
	int i;
	
	private Vector3 direction;
	
	private bool[] colorassigned = new bool[3];
	public Color color;

	private bool requestforpause = false;


	void Start ()
	{
		manager =  GameObject.FindGameObjectWithTag ("Kinect").GetComponent<KinectManager> ();

		if (String.Compare (gameObject.name, "Player_1") == 0)
		{
			player = 1;
			if ( manager.player1added == true )
			{
				UserDetected ( manager.p1 , 0 );
			}
		}

		else if (String.Compare (gameObject.name, "Player_2") == 0)
		{
			player = 2;
			if ( manager.player2added == true )
			{
				UserDetected ( manager.p2 , 0);
			}
		}
	
	}




	public string UserDetected(Int64 userId, int userIndex)
	{	
		if (tracked == false) {
			if (String.Compare (gameObject.name, "Player_1") == 0 || String.Compare (gameObject.name, "Player_2") == 0) {
				Debug.Log ("Adding " + gameObject.name);

						manager.DetectGesture (userId, KinectGestures.Gestures.Tpose);
				//		manager.DetectGesture (userId, KinectGestures.Gestures.SwipeRight);
				//		manager.DetectGesture(userId, KinectGestures.Gestures.SwipeUp);
				//		manager.DetectGesture(userId, KinectGestures.Gestures.SwipeDown);

				tracked = true;
				user = userId;

				if (GestureInfo != null) {
					GestureInfo.GetComponent<GUIText>().text = "Playing!";
				}

				/*if (colorassigned[player] == false) {
				Renderer[] rendererArray = transform.GetComponentsInChildren<Renderer>();
				foreach (Renderer obj in rendererArray) {
					obj.material.color = color;
				}
				colorassigned [player] = true;
			}*/

				return gameObject.name;

			}
		}
			return "none";
		
	}


	public void UserLost(Int64 userId, int userIndex)
	{

		if (userId.CompareTo(user) == 0 ) 
		{
			Debug.Log ("Lost player: " + player);
			requestforpause = true;		
		}
	}



	


	void FixedUpdate () {

		manager =  GameObject.FindGameObjectWithTag ("Kinect").GetComponent<KinectManager> ();

		if (manager.GetComponent<Pause> ().isPaused == false && requestforpause == true) {
			requestforpause = false;
			manager.GetComponent<Pause> ().PauseGame (player);
		}

		if (player == 1)
		{
			tracked = manager.player1tracked;
		}
		
		else if (player == 2)
		{
			tracked = manager.player2tracked;
		}


		if (tracked == true)
		{
			if ( i == 0 ){
				lasthandposition = manager.GetJointPosition(user , 7 );
			}
			i++;
			if ( i == 12 ){
				i = 0;
				auxiliarydistance = manager.GetJointPosition(user , 7 ) - lasthandposition;
			}

				//updates the direction of the right hand, used to fire
			direction = manager.GetJointPosition(user , 11 ) - manager.GetJointPosition(user , 9 );

			}

		manager.CheckForGestures (user);


		//Pauses
		if (IsTpose())
		{
			Debug.Log("TPose detected");
			requestforpause = true;
		}


		
	}		

	
	//Return the Main Forces
	public string GetMainForces () {
		if (tracked == true) {
		distance = manager.GetJointPosition(user , 7 ).x - manager.GetJointPosition(user , 5 ).x;
			if (Math.Abs(distance) < 0.1){
				return "stay";
			}
			
			if ( distance > 0) {
				return ("right");
			} else {
				return ("left");
			}
		} else
			return ("no active body");
	}
	
	//Jumps with Propulsion
	public Boolean Propulsion () {
		if (tracked == true) {
			if (manager.GetLeftHandState(user) == KinectInterop.HandState.Open){
				return true;
			} else {
				return false;
			}
		} else
			return false;
	}
	
	//Return the direction to fire the projectile
	public Vector3 GetDirection () {
		if (tracked == true) {
			direction.z =0;
			return direction;
		} else {
			return new Vector3 (0, 0, 0);
		}
	}
	
	//Fires
	public Boolean SetFire (){
		if (tracked == true) {
			if (manager.GetRightHandState(user) == KinectInterop.HandState.Open){
				return true;
			} else {
				return false;
			}
		} else
			return false;

	}




	private bool swipeLeft;
	private bool swipeRight;
	private bool swipeUp;
	private bool swipeDown;

	public bool tPose;

	public bool IsTpose()
	{
		if(tPose)
		{
			tPose = false;
			return true;
		}
		
		return false;
	}



	public bool IsSwipeLeft()
	{
		if(swipeLeft)
		{
			swipeLeft = false;
			return true;
		}
		
		return false;
	}
	
	public bool IsSwipeRight()
	{
		if(swipeRight)
		{
			swipeRight = false;
			return true;
		}
		
		return false;
	}
	
	public bool IsSwipeUp()
	{
		if(swipeUp)
		{
			swipeUp = false;
			return true;
		}
		
		return false;
	}
	
	public bool IsSwipeDown()
	{
		if(swipeDown)
		{
			swipeDown = false;
			return true;
		}
		
		return false;
	}

	public void GestureInProgress(Int64 userId, int userIndex, KinectGestures.Gestures gesture, 
	                              float progress, KinectInterop.JointType joint, Vector3 screenPos)
	{
		//Time.timeScale = 0;
		// don't do anything here
	}

	public bool GestureCompleted (Int64 userId, int userIndex, KinectGestures.Gestures gesture, 
	                              KinectInterop.JointType joint, Vector3 screenPos)
	{
		Time.timeScale = 1;
		string sGestureText = gesture + " detected";
		if(GestureInfo != null)
		{
			GestureInfo.GetComponent<GUIText>().text = sGestureText;
		}

		if (gesture == KinectGestures.Gestures.Tpose) {
			Debug.Log ("Gesture Completed!");
			return true;
		}
		else
			return false;
		/*else if(gesture == KinectGestures.Gestures.SwipeLeft)
			swipeLeft = true;
		else if(gesture == KinectGestures.Gestures.SwipeRight)
			swipeRight = true;
		else if(gesture == KinectGestures.Gestures.SwipeUp)
			swipeUp = true;
		else if(gesture == KinectGestures.Gestures.SwipeDown)
			swipeDown = true;*/
		
	}

	public bool GestureCancelled (Int64 userId, int userIndex, KinectGestures.Gestures gesture, 
	                              KinectInterop.JointType joint)
	{
		Time.timeScale = 1;
		// don't do anything here, just reset the gesture state
		return true;
	}

	public string Getname ()
	{
		return gameObject.name;
	}

	public void Requestforpause(Int64 userId , int UserIndex)
	{
		Debug.Log ("Pause Received");
		if (user.CompareTo(userId) == 0 && manager.GetComponent<Pause>().isPaused == false) requestforpause = true;
		Debug.Log ("Request for Pause Done");
	}



}
