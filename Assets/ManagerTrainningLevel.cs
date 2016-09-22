using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ManagerTrainningLevel : MonoBehaviour {

    //Game Object Called BombBall
    public GameObject bombBall;

    //Game Object Maked with BombaBall
    public GameObject makedBombBall;

    //Array String to manipulation of BombBall
    public List<string> listNameBomBall;

    //This variable is used to set the level
    public int numLevel = 0;

    //This variable to controle avaliation about level
    public bool avalLevel = false;

    //Variable Global to postion Area
    public Vector3 positionArea;

    //Tranforms positions limits, very important to Position Area
    public Transform positionLeft, positionRight, positionTop, positionBottom;

    //Waiting controller for levels
    public bool waitLevelSeconds = true;

    //NoWaiting controller for levels
    public bool noWaitLevelSeconds = false;

    //Gui Text to show the level to player
    public Text levelText;


    void Awake () {
        
        numLevel = 1; 
        avalLevel = true;
        listNameBomBall = new List<string>();
        levelText = GameObject.Find("LevelText").GetComponent<Text>();
        levelText.text = "Trankies' Trainning";

        
    }
	
	// Update is called once per frame
	void Update () {

        if (avalLevel && numLevel <= 10)
        {
            if (waitLevelSeconds)
            {
                //Called this function we have a little time to show the number level
                StartCoroutine(waitLevel());
            }
            else if (noWaitLevelSeconds)
            {

                //Gui to show the current level
                levelText.text = "Level: " + numLevel;

                //call the function to instantiate bombs per level         
                BombBallInstantieate(numLevel, positionArea);
            }

        }
        else if (numLevel == 11)
        {
            levelText.text = "EXIT LEVEL";
        }
        else if (!avalLevel)
        {

            //listNameBomBall controls whether all the bombs were destroyed in the previous level
            if (listNameBomBall.Count == 0)
            {
                if (waitLevelSeconds)
                {
                    StartCoroutine(waitLevel());
                }
                else if (noWaitLevelSeconds)
                {
                    //TernaryOperator to control 10 leves
                    numLevel = numLevel < 10 ? ++numLevel : 11;

                    //Check e change the avaliator status
                    avalLevel = true;
                }


            }
        }
             
        
    }

    public void BombBallInstantieate(int numLevel, Vector3 positionArea) {
        
        if (numLevel == 1) {

            for (int i = 0; i <= 1; i++)
            {
                // Variable i is used to set name object instantiate
                bombBallInstantiate(i);
            }           
        }

        else if (numLevel == 2)
        {

            for (int i = 0; i <= 1; i++)
            {
                bombBallInstantiate(i);
            }
        }

        else if (numLevel == 3)
        {

            for (int i = 0; i <= 1; i++)
            {
                bombBallInstantiate(i);
            }
        }

        else if (numLevel == 4)
        {

            for (int i = 0; i <= 1; i++)
            {
                bombBallInstantiate(i);
            }
        }

        else if (numLevel == 5)
        {

            for (int i = 0; i <= 1; i++)
            {
                bombBallInstantiate(i);
            }
        }

        else if (numLevel == 6)
        {

            for (int i = 0; i <= 1; i++)
            {
                bombBallInstantiate(i);
            }
        }

        else if (numLevel == 7)
        {

            for (int i = 0; i <= 1; i++)
            {
                bombBallInstantiate(i);
            }
        }

        else  if (numLevel == 8)
        {

            for (int i = 0; i <= 1; i++)
            {
                bombBallInstantiate(i);
            }
        }

        else if (numLevel == 9)
        {

            for (int i = 0; i <= 1; i++)
            {
                bombBallInstantiate(i);
            }
        }

        else if (numLevel == 10)
        {

            for (int i = 0; i <= 1; i++)
            {
                bombBallInstantiate(i);
            }
        }


        avalLevel = false;

        //Inverte the waiting status to outher turns
        StartCoroutine(nowaitLevel());

    }

    //positionArea provides a random position between the limits
    public Vector3 positionAreaRandom() {
        //return positionArea = new Vector3(Random.Range(positionLeft.position.x + 1f, positionRight.position.x - 1f), Random.Range(positionTop.position.y - 1f, positionBottom.position.y + 1f), 0f);
        return positionArea = new Vector3(Random.Range(-22, 22), Random.Range(12, 28), 0f);

    }

    //Instantiate one BombBall renamed and include to list controller bomb 
    public GameObject bombBallInstantiate(int numberBomb) {
        
        //Instatiation
        GameObject makedBombBall = Instantiate(bombBall, positionAreaRandom(), Quaternion.identity) as GameObject;

        //Rename BombBall
        makedBombBall.name = "BombBall " + (numberBomb + 1);

        //ListBombLevel
        listNameBomBall.Add(makedBombBall.name);

        return makedBombBall;

    }

    public void bombExplode(string name) {

        foreach(string bombToExplode in listNameBomBall)
        {
            if (bombToExplode == name) {
                listNameBomBall.Remove(name);                
                Destroy(GameObject.Find(name));
            }
        }

    }

    IEnumerator nowaitLevel() {
        waitLevelSeconds = true;
        yield return new WaitForSeconds(0f);
        noWaitLevelSeconds = false;
        Debug.Log("BELIVE");
       
    }

    IEnumerator waitLevel()
    {
        waitLevelSeconds = false;
        yield return new WaitForSeconds(5f);
        noWaitLevelSeconds = true;
        Debug.Log("No belive");
        
    }



}
