using UnityEngine;
using UnityEngine.UI;
//using UnityEditor;
using System.Collections;

public class CharController : MonoBehaviour
{
	bool grounded = false;
	private float propulsionForce;
	private float moveSpeed;
	
	public GestureListener kinect;
	private float jumpTimer;
	public float jumpCoolDown;
	private float propulsionTimer;
	public float propulsionCoolDown;
	
	private float totalspeed = 0;
	private Vector3 totalspeedauxiliar = new Vector3(0, 0, 0);
	private float originalmoveSpeed;
	private float groundedmoveSpeed;
	
	//public AudioClip forward;
	public AudioClip jump;
	public AudioClip hitground;
	
	//public AudioClip changedirection;
	
	public static bool playing = true;
	
	public Animator anim;
	
	float speedHash = Animator.StringToHash("speed");
	int jumpHash = Animator.StringToHash("jump");
	
	
	public Vector3 startposition;
	
	public Transform[] respawnPositions;
	
	private Rigidbody body;
	
	//var for controls via keyboard and mouse or joystick
	private string thisplayer;
	public int numplayer = 0;
	public Vector3 mousePos;
	public Vector3 mouseDirection;

    //Gui Text to show the level to player
    public Text playerSelected;


    void Awake()
	{
		
		// Matches with the script for Kinect
		kinect = this.GetComponent<GestureListener>();
		
		
		if (string.Compare(Application.loadedLevelName, "Spaceship_Level") == 0)
		{
			this.enabled = false;
		}
		
		playing = true;
		anim = gameObject.GetComponent<Animator>();
		
		propulsionForce = 950f;
		moveSpeed = 20000f;



        if (Application.loadedLevelName == "Trainning_Level")
        {
            playerSelected = GameObject.Find("PlayerSelected").GetComponent<Text>();
        }



    }

    void Start()
	{
		
		body = GetComponent<Rigidbody>();
		
		//set the jump Timer
		jumpTimer = 0f;
		jumpCoolDown = 1f;
		
		//set the propulsion Timer
		propulsionTimer = 0f;
		propulsionCoolDown = 0.3f;
		
		//set the propulsion Force
		
		originalmoveSpeed = moveSpeed;
		groundedmoveSpeed = moveSpeed * 7;
		
		//setup feel:
		Physics.gravity = new Vector3(0, -60, 0);
		body.angularDrag = 0.9f;
		body.drag = 0.9f;
		
		
		//saves the origin of the character
		startposition = transform.position;
		
		thisplayer = gameObject.name;
		
	}
	
	public void OnGUI()
	{
        if (numplayer != 0 && Application.loadedLevelName == "Training_Level") {

            playerSelected.text = "Player" +numplayer;

        } else if (playerSelected == null) {
            GUI.Label(new Rect(Screen.width / 2.0f, 60, 150, 100), "Player_" + numplayer);
        }
		
	}
	
	void FixedUpdate()
	{
		
		if (playing)
		{
			
			//lock the z position for 2D if not paused:
			Vector3 tempPos = transform.position;
			tempPos.z = 0;
			transform.position = tempPos;
			
			//Because of Drag, improve directional force when grounded
			if (grounded) moveSpeed = groundedmoveSpeed;
			else moveSpeed = originalmoveSpeed;
			
			//Implements the jumpTimer
			jumpTimer += Time.deltaTime;
			propulsionTimer += Time.deltaTime;
			
			ListenDebugKeyboard();
			ListenKinectMotions();


			//updates the speedhash for the animator
			anim.SetFloat("speed", body.velocity.magnitude);
			//speedHash = body.velocity.magnitude;


		}
	}
	
	void OnCollisionStay(Collision collisionInfo)
	{
		if (collisionInfo.gameObject.name == "Ground") grounded = true;
	}
	
	void OnCollisionEnter(Collision collisionInfo)
	{
		if (collisionInfo.gameObject.name == "Ground")
		{
			grounded = true;
			AudioSource.PlayClipAtPoint(hitground, transform.position);
		}
	}
	
	void OnCollisionExit(Collision collisionInfo)
	{
		if (collisionInfo.gameObject.name == "Ground") grounded = false;
	}
	
	void KillPlayer()
	{
		body.Sleep();
		body.velocity = Vector3.zero;
		
		int pos = Random.Range(1, respawnPositions.Length);
		int playerToTake;
		
		playerToTake = kinect.player == 1 ? 2 : 1;
		
		Vector3 otherPlayerPosition = GameObject.Find("Player_" + playerToTake).GetComponent<Transform>().position;
		
		while (Vector3.Distance(otherPlayerPosition, respawnPositions[pos].position) < 1)
			pos = Random.Range(1, respawnPositions.Length);
		
		transform.position = respawnPositions[pos].position;
		body.WakeUp();
	}
	
	void ListenDebugKeyboard()
	{
        //take mouse position on plane (0,0,1) (playes's plane)
		mousePos = Camera.main.ScreenPointToRay(Input.mousePosition).direction * (-Camera.main.transform.position.z) + Camera.main.transform.position;
		mousePos.z = 0;
		mouseDirection = (mousePos - transform.position).normalized;
		
		
		//select player to control
		if (Input.GetKeyDown ("1") && numplayer != 1) {

			//With Keyboard, we dont need the Kinect feedback
			kinect.manager.displayUserMap = false;
			kinect.manager.GetComponent<KinectManager>().displayColorMap = false;
			kinect.manager.GetComponent<KinectManager>().displaySkeletonLines = false;

			numplayer = 1;
		} else if (Input.GetKeyDown ("2") && numplayer != 2) {

			//With Keyboard, we dont need the Kinect feedback
			kinect.manager.displayUserMap = false;
			kinect.manager.GetComponent<KinectManager>().displayColorMap = false;
			kinect.manager.GetComponent<KinectManager>().displaySkeletonLines = false;

			numplayer = 2;
		} else if (Input.GetKeyDown ("1") || Input.GetKeyDown ("2")) {
			numplayer = 0;
		}
		
		//verify if this player and numplayer match
		bool aux = (("Player_" + numplayer) == thisplayer);
		
		totalspeed = (moveSpeed / 10) * Time.deltaTime;
		
		if (totalspeed > moveSpeed) totalspeed = moveSpeed;
		
		if (aux && numplayer != 0)
		{
            
			if (mouseDirection.x <= 0 && transform.eulerAngles.y != 180)
			{
				gameObject.RotateTo(new Vector3(transform.eulerAngles.x, 180, transform.eulerAngles.z), 0.5f, 0f);
			}
			else if (mouseDirection.x > 0 && transform.eulerAngles.y != 0)
			{
				gameObject.RotateTo(new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z), 0.5f, 0f);
			}
			
			if (Input.GetKeyDown("right") || Input.GetKeyDown("left"))
			{
				body.angularVelocity = Vector3.zero;
				Debug.Log("Idle");
			}
			
			if (Input.GetAxis("Horizontal_P" + numplayer) > 0.2f)
			{
				if (transform.eulerAngles.y != 0)
					gameObject.RotateTo(new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z), 0.5f, 0f);
				
				body.AddForce(new Vector3(moveSpeed / 10 * Time.deltaTime* Input.GetAxis("Horizontal_P" + numplayer), 0, 0));
				//anim.SetFloat(speedHash, totalspeed);
				//Debug.Log("Forward");
			}
			
			if (Input.GetAxis("Horizontal_P" + numplayer) < -0.2f)
			{
				if (transform.eulerAngles.y != 180)
					gameObject.RotateTo(new Vector3(transform.eulerAngles.x, 180, transform.eulerAngles.z), 0.5f, 0f);
				
				body.AddForce(new Vector3(moveSpeed / 10 * Time.deltaTime* Input.GetAxis("Horizontal_P" + numplayer), 0, 0));
				//anim.SetFloat(speedHash, totalspeed);
				//Debug.Log("Forward");
			}
			
			if (Input.GetButton("Jump_P" + numplayer) && propulsionTimer > propulsionCoolDown)
			{
                propulsionTimer = 0;
                body.AddForce(new Vector3(0, propulsionForce, 0));
				AudioSource.PlayClipAtPoint(jump, transform.position);
				anim.SetTrigger(jumpHash);
                //Debug.Log("Jump");
            }

            
            if (Input.GetButtonDown("Pause_P" + numplayer))
			{
				if (GameObject.FindGameObjectWithTag("Kinect").GetComponent<Pause>().isPaused && GameObject.FindGameObjectWithTag("Kinect").GetComponent<Pause>().player.name == thisplayer)
					GameObject.FindGameObjectWithTag("Kinect").GetComponent<Pause>().ContinueGame(numplayer);
				
				else if (!GameObject.FindGameObjectWithTag("Kinect").GetComponent<Pause>().isPaused)
					GameObject.FindGameObjectWithTag("Kinect").GetComponent<Pause>().PauseGame(numplayer);
				
				//Debug.Log("Pause_P" + numplayer);
				
			}
		}
		
		
		
		
	}
	
	void ListenKinectMotions()
	{
		if (kinect.tracked)
		{
			//moveSpeed = (moveSpeed / kinect.bodyupdater.transform.parent.transform.parent.transform.localScale.x) * ( Math.Abs (kinect.bodyupdater.GetComponent<BodyUpdater> ().BodyParts [0].joint.transform.position.z)*0.05f );
			
			//Forces from the distance of the arm
			totalspeed = (moveSpeed / 10) * Mathf.Abs(kinect.distance) * Time.deltaTime / (kinect.transform.localScale.x);
			//Debug.Log (kinect.bodyupdater.GetComponent<BodyUpdater> ().BodyParts [0].joint.transform.position.z);
			if (totalspeed > moveSpeed)
			{
				totalspeed = moveSpeed;
			}
			
			// Always point to the target direction
			if (kinect.GetDirection().x < 0 && transform.eulerAngles.y != 180)
			{
				gameObject.RotateTo(new Vector3(transform.eulerAngles.x, 180, transform.eulerAngles.z), 1f, 0f);
				//AudioSource.PlayClipAtPoint(changedirection, transform.position);
			}
			if (kinect.GetDirection().x > 0 && transform.eulerAngles.y != 0)
			{
				gameObject.RotateTo(new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z), 1f, 0f);
				//AudioSource.PlayClipAtPoint(changedirection, transform.position);
			}
			
			if (kinect.GetMainForces() == "right")
			{

				//I multiplied the total speed by 2 here to improve player experience
				body.AddForce(new Vector3(totalspeed*2, 0, 0));

			}
			
			if (kinect.GetMainForces() == "left")
			{
				//I multiplied the total speed by 2 here to improve player experience
				body.AddForce(new Vector3(-totalspeed*2, 0, 0));
			}
			
			//Forces from the velocity of the hand movements
			
			//Propulsion
			if (kinect.Propulsion() && propulsionTimer > propulsionCoolDown)
			{
				propulsionTimer = 0;
				body.AddForce(new Vector3(0, propulsionForce, 0));
				anim.SetTrigger(jumpHash);
				AudioSource.PlayClipAtPoint(jump, transform.position);
			}
			
			totalspeedauxiliar = (moveSpeed / 10) * Time.deltaTime * kinect.auxiliarydistance;
			
			if (totalspeedauxiliar.x > moveSpeed) totalspeedauxiliar.x = moveSpeed;
			if (totalspeedauxiliar.y > moveSpeed) totalspeedauxiliar.y = moveSpeed;
			if (totalspeedauxiliar.x < -moveSpeed) totalspeedauxiliar.x = -moveSpeed;
			if (totalspeedauxiliar.y < -moveSpeed) totalspeedauxiliar.y = -moveSpeed;
			
			body.AddForce(1f * new Vector3(totalspeedauxiliar.x, 0, 0));
			
			if (kinect.auxiliarydistance.y < 0)
				body.AddForce(1f * new Vector3(0, totalspeedauxiliar.y, 0));
			
			//Auxiliary Force to help jumping (propulsion)
			if (jumpTimer < jumpCoolDown)
			{
				body.AddForce(1f * new Vector3(0, totalspeedauxiliar.y, 0));
				jumpTimer = 0;
			}
		}
		
		
	}
	
}
//[System.Serializable]
//[CustomEditor(typeof(CharController))]
//public class CharControllerEditor : Editor
//{
//    public override void OnInspectorGUI()
//    {
//        DrawDefaultInspector();
//        CharController a = (CharController)target;

//        EditorGUILayout.Toggle("Alternative Control", a.altContr);

//    }
//}