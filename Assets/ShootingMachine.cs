﻿using UnityEngine;
using System.Collections;

public class ShootingMachine : MonoBehaviour
{
    public GestureListener kinect;
    //public GameObject projectile = null;
    public GameObject ray = null;

    public float projectileforce = 25000f;
    public int damagePerShot = 1;
    public int numplayer = 0;

    public bool firstShoot = true;
    public bool canFire = true;
    public float firecooldown = 0.5f;
    public float fireRate = 0.25f;

    public Vector3 useDirection;
    public Vector3 firelocal;
    public Transform machineEnd;
    public AudioClip fire;
    

    private float nextFire;
    private float firetimer;
    private float hitForce = 1000f;
    private bool safetyTime = false;
    private Vector3 scale;
    private Vector3 totalforce;
    private WaitForSeconds shotDuration = new WaitForSeconds(0.01f);

    bool rebate = false;
    float size = 0;
    int raycount = 0;
    GameObject[] rays = new GameObject[100];
    RaycastHit hit;
    RaycastHit hitRebate;
    RaycastHit rebateRebate;
    Light machineLight;

    public GameObject machinePlataform;



    IEnumerator SafetyTime()
    {
        float waitTime = 0.1f;
        if (string.Compare(Application.loadedLevelName, "Spaceship_Level") == 0)
            waitTime = 0.5f;
        yield return new WaitForSeconds(waitTime);
        safetyTime = true;
    }


    void Awake()
    {

        // Matches with the script for Kinect
        kinect = this.GetComponent<GestureListener>();

        if (string.Compare(Application.loadedLevelName, "Trainning_Level")==0)
        {
            this.enabled = false;
        }

        firetimer = 0;
        firstShoot = true;
        machineEnd = GetComponent<Transform>();
        


    }

    void Start()
    {
        
        StartCoroutine("SafetyTime");

    }


    void FixedUpdate()
    {
        
        firelocal = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        firetimer = firetimer + Time.deltaTime;
        if (canFire && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Point();
            StartCoroutine("ShotEffect");
        }
       
        

    }

    private IEnumerator ShotEffect()
    {
        // Disable the line renderer and the light.
        Debug.Log("Entrou");       
        canFire = false;
        yield return shotDuration;
        Destroy(rays[raycount]);
        Debug.Log("Saiu");        
        canFire = true;


    }




    public void Point()
    {


        useDirection = new Vector3(1, 0, 0);
        if (Physics.Raycast(transform.position, useDirection, out hit))
        {
            Debug.Log(hit.collider.name);

            //Physics.IgnoreCollision(ray.GetComponent<Collider>(), transform.root.GetComponent<Collider>());
            TrankiesHealth health = hit.collider.GetComponent<TrankiesHealth>();

            if (health != null)
            {
                health.TakeDamage(damagePerShot, hit.point);
                Debug.Log("Take Damage");

            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * hitForce);
            }



            //Physics.IgnoreCollision (instprojectile.collider, transform.root.collider);
            //Debug.Log("Poiting");

            //if (rays[raycount]) Destroy(rays[raycount]);

            rays[raycount] = Instantiate(ray, firelocal, Quaternion.LookRotation(useDirection)) as GameObject;
            //rays[raycount].GetComponent<Renderer>().material.color = kinect.color;
            //rays[raycount].GetComponent<Light>().color = kinect.color;
            rays[raycount].name = "pointer_";
            firstShoot = false;
            //Debug.Log("Instantiating Pointer");




            rays[raycount].GetComponent<Renderer>().enabled = true;

            scale = rays[raycount].transform.localScale;
            scale.z += hit.distance;
            rays[raycount].transform.localScale = scale;

        }
    }

    public void setMachineProp(int dPShot, float fRate, float hForce, float wfseconds, float timeMachine)
    {

       damagePerShot = dPShot;
       fireRate = fRate;
       hitForce = hForce;
       WaitForSeconds shotDuration = new WaitForSeconds(wfseconds);
       machinePlataform.GetComponent<MachineController>().changeTime(timeMachine);


    }

    public void changeMachineStatus()
    {
        machinePlataform.GetComponent<MachineController>().changeStatus();
        



    }
}



