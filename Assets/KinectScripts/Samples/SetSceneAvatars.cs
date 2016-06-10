using UnityEngine;
using System.Collections;
using System.Reflection;

public class SetSceneAvatars : MonoBehaviour 
{
	void Awake()
	{


		}

	void Start () 
	{
		KinectManager manager = GameObject.FindGameObjectWithTag ("Kinect").GetComponent<KinectManager> ();
		
		if(manager)
		{

			/*if (Application.loadedLevel.CompareTo("SpaceShip_Level") == 0)
			{
			// remove all users, filters and avatar controllers
				manager.ClearKinectUsers();
				manager.avatarControllers.Clear();
				manager.player1added = false;
				manager.player1tracked = false;
				manager.player2added = false;
				manager.player2tracked = false;
			}*/


			// get the mono scripts. avatar controllers and gesture listeners are among them
			MonoBehaviour[] monoScripts = FindObjectsOfType(typeof(MonoBehaviour)) as MonoBehaviour[];
			
			// add available avatar controllers
			foreach(MonoBehaviour monoScript in monoScripts)
			{
				if(typeof(AvatarController).IsAssignableFrom(monoScript.GetType()))
				{
					AvatarController avatar = (AvatarController)monoScript;
					manager.avatarControllers.Add(avatar);
				}
			}

			// add available gesture listeners
			manager.gestureListeners.Clear();

			Debug.Log("adding gestures");
			manager.gestureListeners.Add(GameObject.Find("Player_1").GetComponent<GestureListener>() );
			Debug.Log(manager.gestureListeners.Count);
			manager.gestureListeners.Add(GameObject.Find("Player_2").GetComponent<GestureListener>() );
			Debug.Log(manager.gestureListeners.Count);
			Debug.Log(manager.gestureListeners.Count);

			/*foreach(MonoBehaviour monoScript in monoScripts)
			{
				if(typeof(KinectGestures.GestureListenerInterface).IsAssignableFrom(monoScript.GetType()))
				{
					//KinectGestures.GestureListenerInterface gl = (KinectGestures.GestureListenerInterface)monoScript;
					manager.gestureListeners.Add(monoScript);

				}
			}*/

		}
	}
	
}
