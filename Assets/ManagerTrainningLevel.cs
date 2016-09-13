using UnityEngine;
using System.Collections;

public class ManagerTrainningLevel : MonoBehaviour {

    //Game Object Called BombBall
    public GameObject bombBall;

    //This variable is used to set the level
    public int numLevel = 1;

    //RateOfSpwan Balls
    public float RateOfSpawn = 5;

    //next Spawn Ball
    private float nextSpawn = 0;

    //Variable Global to postion Area
    public Vector3 positionArea;

    //Tranforms positions limits, very important to Position Area
    public Transform positionLeft, positionRight, positionTop, positionBottom;

    int a = 0;
    // Use this for initialization
    void Start () {

        numLevel = 1;
    }
	
	// Update is called once per frame
	void Update () {

        // if (Time.time > nextSpawn) for spawn with time
        if (numLevel == 1)
        {
            
            nextSpawn = Time.time + RateOfSpawn;
            BombBallInstantieate(numLevel, positionArea);
            numLevel = 0;
        }

       

    }

    public void BombBallInstantieate(int numLevel, Vector3 positionArea) {
        
        if (numLevel == 1) {

            for (int i = 0; i <= 3; i++)
            {                
                positionArea = new Vector3(Random.Range(positionLeft.position.x+5f, positionRight.position.x-5f), Random.Range(positionTop.position.y-5, positionBottom.position.y+5f), 0f); 
                Instantiate(bombBall, positionArea, Quaternion.identity);
                bombBall.name = "BombBall "+(i+1);
                Debug.Log(positionArea);
                
            }            

        }
    }
}
