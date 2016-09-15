using UnityEngine;
using System.Collections;

public class KeyboardListener : MonoBehaviour {

    public int numplayer = 0;
    public GameObject player1;
    public GameObject player2;
    public bool firstpress=false;
    [SerializeField]
    private GameObject kinectfortesting;



    void Start () {
        
    }
	
    void Awake()
    {
        player1 = GameObject.Find("Player_1");
        player2 = GameObject.Find("Player_2");
        
    }

    void Update () {

		kinectfortesting = GameObject.FindGameObjectWithTag("Kinect");

        keyboard();

        if (!player1.GetComponent<CharController>().enabled)
            player1.GetComponent<CharController>().numplayer = numplayer;


        if (!player2.GetComponent<CharController>().enabled)
            player2.GetComponent<CharController>().numplayer = numplayer;

    }

    void keyboard()
    {


        if(!firstpress && Input.anyKey)
        {
            firstpress = false;
            //kinectfortesting = GameObject.FindGameObjectWithTag("Kinect");
        }

        //player to control
        if (Input.GetKeyDown("1") && numplayer != 1)
        {
			//With Keyboard, we dont need the Kinect feedback
			kinectfortesting.GetComponent<KinectManager>().displayUserMap = false;
			kinectfortesting.GetComponent<KinectManager>().displayColorMap = false;
			kinectfortesting.GetComponent<KinectManager>().displaySkeletonLines = false;


            if (!player1.GetComponent<Shoots>().enabled)
            {
                kinectfortesting.GetComponent<KinectManager>().player1added = true;
                player1.GetComponent<Shoots>().enabled = true;
            }
            numplayer = 1;



        }
        else if (Input.GetKeyDown("2") && numplayer != 2)
        {

			//With Keyboard, we dont need the Kinect feedback
			kinectfortesting.GetComponent<KinectManager>().displayUserMap = false;
			kinectfortesting.GetComponent<KinectManager>().displayColorMap = false;
			kinectfortesting.GetComponent<KinectManager>().displaySkeletonLines = false;


            if (!player2.GetComponent<Shoots>().enabled)
            {
                kinectfortesting.GetComponent<KinectManager>().player2added = true;
                player2.GetComponent<Shoots>().enabled = true;
            }
            numplayer = 2;
        }
        else if (Input.GetKeyDown("1") || Input.GetKeyDown("2"))
        {
            numplayer = 0;
        }



        if(numplayer == 1)
        {
            if (Input.GetButton("Fire1_P1"))
                player1.SendMessage("Fire");
        }


        if (numplayer == 2)
        {
            if (Input.GetButton("Fire1_P2"))
                player2.SendMessage("Fire");
        }

    }
}
