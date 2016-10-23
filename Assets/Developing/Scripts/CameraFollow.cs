using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class CameraFollow : MonoBehaviour {
	public Vector3 halfwayMultiplier = new Vector3(0.5f, 0.5f, -30);
	private Vector3 halfway;
	private float distance;
	public float zoom = 1;
	public Transform p1, p2;
	public float distanceScale = 1.0f;
	public float smooth = 0.75f;
	private Camera cam;

    //Tranform for CameraShake script
    private Transform tranformShake;


    void Start() {
		// set screen scale to open the field of view
		float screenRatio = (float) Screen.width / (float) Screen.height;
		cam = GetComponent<Camera>();

        switch (screenRatio.ToString("0.00")) {
		case "1.25":	// 5:4
			distanceScale = 1.55f;
			break;
		case "1.33":	// 4:3
			distanceScale = 1.45f;
			break;
		case "1.50":	// 3:2
			distanceScale = 1.35f;
			break;
		case "1.60":	// 16:10
			distanceScale = 1.3f;
			break;
		case "1.78":	// 16:9
			distanceScale = 1.2f;
			break;
		}
	}

	// Update is called once per frame
	void Update() {
		distance = Vector3.Distance(p1.position, p2.position);

		halfway.x = (p1.position.x + p2.position.x) * halfwayMultiplier.x;
		halfway.y = (p1.position.y + p2.position.y) * halfwayMultiplier.y;

		// using field of view
		halfway.z = halfwayMultiplier.z; // maintain z position

        cam.transform.position = Vector3.Lerp(cam.transform.position, halfway, smooth * Time.deltaTime); //halfway;
        tranformShake = cam.transform;
        //calculate and put the value between 20 and 60
        // using FloatUpdate to smoothie the camera movement
        cam.fieldOfView = iTween.FloatUpdate(cam.fieldOfView, Mathf.Clamp((40 * distance * distanceScale + 1020) / 57, 20, 60 * distanceScale) + 5, smooth)*zoom;
	}

	void OnEnable() {

        //When the level is equal "Training_Level" , load one player else load two player
        if (Application.loadedLevelName == "Training_Level")
        {
            p1 = (Transform)GameObject.Find("Player_1").GetComponent("Transform");
            
        }
        else {
            p1 = (Transform)GameObject.Find("Player_1").GetComponent("Transform");
            p2 = (Transform)GameObject.Find("Player_2").GetComponent("Transform");

        }
	}

	void RestartGame() {
		this.enabled = true;
		Debug.Log("Restart CameraFollow");
	}

    public Transform getTransformShake() {
        Debug.Log("Posicao camera follow : "+ tranformShake.position.ToString());
        return tranformShake;
    }
}