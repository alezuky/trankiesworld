using UnityEngine;
using System.Collections;

public class Zoom : MonoBehaviour {

	private CameraFollow cam;

	private float distancey , distancez;

	public Pause pause ;

	private Vector3 originalposition = new Vector3(0,0,0);

	private bool follow = false;

	private GameObject target;

	void Awake()
	{
		pause = GameObject.FindGameObjectWithTag ("Kinect").GetComponent<Pause> ();
	}


public void Zoomat (GameObject target)
	{

		this.target = target;

		cam = this.gameObject.GetComponent<CameraFollow> ();

		originalposition = this.transform.position;

		if (pause.isPaused == true) {
			distancey = 1f;
			distancez = -15f;
		} else {
			distancey = 0f;
			distancez = -7f;
			follow = true;
		}

		if (cam != null) {
			cam.enabled = false;
		}

		this.gameObject.MoveTo (new Vector3 (target.transform.position.x, target.transform.position.y  + distancey, target.transform.position.z + distancez), 0, 0);
	}


	public void Zoomout ()
	{
		this.gameObject.MoveTo (originalposition, 0, 0);

		cam = this.gameObject.GetComponent<CameraFollow> ();

		if (cam != null) {
			cam.enabled = true;
		}

		follow = false;
	}

	void Update ()
	{
		if (follow == true) {
			this.gameObject.transform.position = new Vector3 (target.transform.position.x, target.transform.position.y + distancey, target.transform.position.z + distancez);
		}

	}


}