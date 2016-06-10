using UnityEngine;
//using Windows.Kinect;

using System;
using System.Collections;
using System.Collections.Generic;

public class AddsPlayer : MonoBehaviour {


	public KinectManager manager;

	// Calibration gesture data for each player
	private Dictionary<Int64, KinectGestures.GestureData> playerCalibrationData = new Dictionary<Int64, KinectGestures.GestureData>();


	// Kinect body frame data
	private KinectInterop.BodyFrameData bodyFrame;

	// Calibration pose for the player
	public KinectGestures.Gestures calibrationGesture;

	void Start ()
	{
		manager = GameObject.FindGameObjectWithTag ("Kinect").GetComponent<KinectManager> ();
		bodyFrame = manager.bodyFrame;
	}

	void FixedUpdate ()
	{
		manager = GameObject.FindGameObjectWithTag ("Kinect").GetComponent<KinectManager> ();
		bodyFrame = manager.bodyFrame;
	}

	// check if the calibration pose is complete for given user
	public bool CheckForCalibrationPose(Int64 UserId, int bodyIndex)
	{
		if (gameObject.GetComponent<GestureListener> ().tracked == true)
			return false;

		if(calibrationGesture == KinectGestures.Gestures.None)
			return true;
		
		KinectGestures.GestureData gestureData = playerCalibrationData.ContainsKey(UserId) ? 
			playerCalibrationData[UserId] : new KinectGestures.GestureData();
		
		// init gesture data if needed
		if(gestureData.userId != UserId)
		{
			gestureData.userId = UserId;
			gestureData.gesture = calibrationGesture;
			gestureData.state = 0;
			gestureData.timestamp = Time.realtimeSinceStartup;
			gestureData.joint = 0;
			gestureData.progress = 0f;
			gestureData.complete = false;
			gestureData.cancelled = false;
		}
		
		// get joint positions and tracking
		int iAllJointsCount = manager.GetSensorJointCount();
		bool[] playerJointsTracked = new bool[iAllJointsCount];
		Vector3[] playerJointsPos = new Vector3[iAllJointsCount];
		
		int[] aiNeededJointIndexes = KinectGestures.GetNeededJointIndexes();
		int iNeededJointsCount = aiNeededJointIndexes.Length;
		
		for(int i = 0; i < iNeededJointsCount; i++)
		{
			int joint = aiNeededJointIndexes[i];
			
			if(joint >= 0)
			{

				KinectInterop.JointData jointData = manager.bodyFrame.bodyData[bodyIndex].joint[joint];

				playerJointsTracked[joint] = jointData.trackingState != KinectInterop.TrackingState.NotTracked;
				playerJointsPos[joint] = jointData.kinectPos;
			}
		}
		
		// estimate the gesture progess
		KinectGestures.CheckForGesture(UserId, ref gestureData, Time.realtimeSinceStartup, 
		                               ref playerJointsPos, ref playerJointsTracked);
		playerCalibrationData[UserId] = gestureData;
		
		// check if gesture is complete
		if(gestureData.complete)
		{
			gestureData.userId = 0;
			playerCalibrationData[UserId] = gestureData;

			NotifyUser (UserId);
			return true;
		}
		
		return false;
	}


	// notify the gesture listener about the new user
	void NotifyUser (Int64 UserId)	{

		string player = gameObject.GetComponent<GestureListener>().UserDetected(UserId , 0);		
				if (String.Compare (player, "Player_1") == 0)
					{
					manager.player1added = true;
					manager.player1tracked = true;
					manager.p1 = UserId;
					this.gameObject.GetComponent<Shoots>().enabled = true;

					}
				if (String.Compare (player, "Player_2") == 0) {
					manager.player2added = true;
					manager.player2tracked = true;
					manager.p2 = UserId;

					this.gameObject.GetComponent<Shoots>().enabled = true;

					}

		}



}



