using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

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

    //BombBallAnimator to control differents animations
    //public Animator animBombBall;

    //To controll de machinegun
    public GameObject shootingMachine;

    //Capture the player loaded
    public TrankiesHealth health;

    //Bool to controll the spawn to another scene
    bool canLoad = false;



    void Awake () {
        
        numLevel = 1; 
        avalLevel = true;
        listNameBomBall = new List<string>();
        levelText = GameObject.Find("LevelText").GetComponent<Text>();
        levelText.text = "Trankies' Trainning";
        TrankiesHealth health = new TrankiesHealth();

    }
	
	// Update is called once per frame
	void Update () {

        if (playerDead()) {
            StartCoroutine(waitDieRestart());
            levelText.text = "GAME OVER";

            if (canLoad) {
                          
                Application.LoadLevel("Training_Level");
            }
        }

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
            StartCoroutine(waitFinishiExit());


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
                    //animBombBall.SetInteger("levelBombBall", numLevel);

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

            for (int i = 0; i <= 2; i++)
            {
                bombBallInstantiate(i);
                
            }
        }

        else if (numLevel == 3)
        {
            shootingMachine.GetComponent<ShootingMachine>().changeMachineStatus();
            shootingMachine.GetComponent<ShootingMachine>().setMachineProp(3, 1.4f, 200f, 0.6f, 10f);

            for (int i = 0; i <= 4; i++)
            {
                bombBallInstantiate(i);
            }

        }

        else if (numLevel == 4)
        {
            shootingMachine.GetComponent<ShootingMachine>().setMachineProp(5, 1.2f, 1100f, 0.5f, 9f);
            for (int i = 0; i <= 8; i++)
            {
                bombBallInstantiate(i);
            }
        }

        else if (numLevel == 5)
        {
            shootingMachine.GetComponent<ShootingMachine>().setMachineProp(10, 1f, 1200f, 0.4f, 9f);
            for (int i = 0; i <= 10; i++)
            {
                bombBallInstantiate(i);
            }
        }

        else if (numLevel == 6)
        {
            shootingMachine.GetComponent<ShootingMachine>().setMachineProp(12, 0.9f, 1400f, 0.4f, 8f);
            for (int i = 0; i <= 12; i++)
            {
                bombBallInstantiate(i);
            }
        }

        else if (numLevel == 7)
        {
            shootingMachine.GetComponent<ShootingMachine>().setMachineProp(14, 0.9f, 1400f, 0.3f, 7f);
            for (int i = 0; i <= 14; i++)
            {
                bombBallInstantiate(i);
            }
        }

        else  if (numLevel == 8)
        {
            shootingMachine.GetComponent<ShootingMachine>().setMachineProp(17, 0.8f, 1500f, 0.3f, 6f);
            for (int i = 0; i <= 16; i++)
            {
                bombBallInstantiate(i);
            }
        }

        else if (numLevel == 9)
        {
            shootingMachine.GetComponent<ShootingMachine>().setMachineProp(20, 0.6f, 1500f, 0.25f, 4f);
            for (int i = 0; i <= 18; i++)
            {
                bombBallInstantiate(i);
            }
        }

        else if (numLevel == 10)
        {
            shootingMachine.GetComponent<ShootingMachine>().setMachineProp(22, 0.5f, 1900f, 0.1f, 5f);
            for (int i = 0; i <= 22; i++)
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

      
        listNameBomBall.Remove(name);
        Destroy(GameObject.Find(name));

    }

    IEnumerator nowaitLevel() {
        waitLevelSeconds = true;
        yield return new WaitForSeconds(0f);
        noWaitLevelSeconds = false;
        
       
    }

    IEnumerator waitLevel()
    {
        waitLevelSeconds = false;
        yield return new WaitForSeconds(5f);
        noWaitLevelSeconds = true;
        
    }

    IEnumerator waitDieRestart()
    {
        
        yield return new WaitForSeconds(5f);
        canLoad = true;

    }

    IEnumerator waitFinishiExit()
    {

        yield return new WaitForSeconds(5f);
        Application.LoadLevel("Spaceship_Level");

    }

    public bool playerDead() {
        return health.m_Dead;
    }



}
