using UnityEngine;
using System.Collections;

public class Shoots : MonoBehaviour
{
    private bool safetyTime = false;

    public GameObject projectile = null;
    public float projectileforce = 25000f;
    public GameObject ray = null;
    public GestureListener kinect;
    private Vector3 totalforce;
    public float firecooldown = 0.5f;
    private float firetimer;
    private Vector3 scale;
    public Vector3 firelocal;

    float size = 0;
    int raycount = 0;
    GameObject[] rays = new GameObject[100];
    RaycastHit hit;
    RaycastHit hitRebate;
    RaycastHit rebateRebate;
    public bool firstShoot = true;
    bool rebate = false;

    public AudioClip fire;

    //public ParticleSystem shootEffect;
    public Animator anim;
    int shootHash = Animator.StringToHash("shoot");

    public Vector3 useDirection;
    public int numplayer = 0;

    IEnumerator SafetyTime()
    {
        float waitTime = 0.1f;
        if (string.Compare(Application.loadedLevelName, "Spaceship_Level") == 0)
            waitTime = 3.0f;
        yield return new WaitForSeconds(waitTime);
        safetyTime = true;
    }


    void Awake()
    {

        // Matches with the script for Kinect
        kinect = this.GetComponent<GestureListener>();
        //shootEffect = GetComponent<ParticleSystem>();

        if (string.Compare(Application.loadedLevelName, "Spaceship_Level") == 0 && kinect.tracked == false)
        {
            this.enabled = false;
        }

        anim = gameObject.GetComponent<Animator>();
        firetimer = 0;
        firstShoot = true;

    }

    void Start()
    {

        StartCoroutine("SafetyTime");

    }


    void FixedUpdate()
    {
		//Trying to make the ponter appear a little in front of Player - not working yet
		/*
		float firelocaldirection = 0.5f;
		if (transform.rotation.y > 90) {
			firelocaldirection = - 3f;	
			firstShoot = false;
			Debug.Log ("changing_direction");
		}
		*/


		firelocal = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        firetimer = firetimer + Time.deltaTime;
        
        if(!gameObject.GetComponent<CharController>().enabled)
            numplayer = GameObject.Find("KeyboardListener").GetComponent<KeyboardListener>().numplayer;
        else
            numplayer = gameObject.GetComponent<CharController>().numplayer;

        if (kinect.tracked == true  || "Player_"+numplayer == gameObject.name)
        {
            Point();
        }

        if (kinect.SetFire() || ((Input.GetButton("Fire1_P1") || Input.GetButton("Fire1_P2")) && gameObject.name == "Player_" + numplayer))
        {
            gameObject.SendMessageUpwards("Fire");
            //shootEffect.Play();
            //anim.SetTrigger(shootHash);
        }

    }


    public void Point()
    {
        if (numplayer != 0)
        {
            if(gameObject.GetComponent<CharController>().enabled == true)
            {
                useDirection = gameObject.GetComponent<CharController>().mouseDirection;
            }
            else
            {
                Vector3 mousePos = Camera.main.ScreenPointToRay(Input.mousePosition).direction * (-Camera.main.transform.position.z) + Camera.main.transform.position;
                mousePos.z = 0;
                useDirection = (mousePos - transform.position).normalized;
            }

        }
        else
        {
            useDirection = kinect.GetDirection();
        }

        if (Physics.Raycast(transform.position, useDirection, out hit))
        {
            //Physics.IgnoreCollision (instprojectile.collider, transform.root.collider);
            //Debug.Log("Poiting");
            if (firstShoot)
            {

				//if (rays[raycount]) Destroy(rays[raycount]);

                rays[raycount] = Instantiate(ray, firelocal, Quaternion.LookRotation(useDirection)) as GameObject;
                //rays[raycount].GetComponent<Renderer>().material.color = kinect.color;
                //rays[raycount].GetComponent<Light>().color = kinect.color;
                
				//Physics.IgnoreCollision(ray.GetComponent<Collider>(), transform.root.GetComponent<Collider>());



				firstShoot = false;
                //Debug.Log("Instantiating Pointer");



            }

            rays[raycount].GetComponent<Renderer>().enabled = true;

            scale = rays[raycount].transform.localScale;
            scale.z = hit.distance;
            rays[raycount].transform.localScale = scale;
            //size += hit.distance;



            // Rebating the raycast is not working
            // That is why the if statement was set to "size < 0" -> it will never be accessed (because it does not work)
            if (size < 0 && (Physics.Raycast(rays[raycount].transform.position, hit.normal, out hitRebate)) == true)
            {

                if (rebate == false)
                {
                    PointRebate();
                }
                else
                {
                    RebateRebate();
                }
            }
            else
            {
                rebate = false;
                size = hit.distance;

                rays[raycount].transform.position = firelocal;
                rays[raycount].transform.rotation = Quaternion.LookRotation(useDirection);

                scale = rays[raycount].transform.localScale;
                scale.z = hit.distance;
                rays[raycount].transform.localScale = scale;


            }
        }
        else
        {
            if (rays[raycount] != null)
            {
                //Destroy (rays [raycount].gameObject);
                //firstShoot = true;
                rays[raycount].GetComponent<Renderer>().enabled = false;
            }
        }
    }


    public void PointRebate()
    {

        rays[raycount].transform.position = hit.point;
        rays[raycount].transform.rotation = Quaternion.LookRotation(hitRebate.normal);

        scale = rays[raycount].transform.localScale;
        scale.z = hitRebate.distance;
        rays[raycount].transform.localScale = scale;
        size += hitRebate.distance;

        rebate = true;
    }

    public void RebateRebate()
    {
        if (Physics.Raycast(rays[raycount].transform.position, hitRebate.normal, out rebateRebate))
        {
            Physics.Raycast(rays[raycount].transform.position, rebateRebate.normal, out hitRebate);
            rays[raycount].transform.position = hitRebate.point;
            rays[raycount].transform.rotation = Quaternion.LookRotation(rebateRebate.normal);

            scale = rays[raycount].transform.localScale;
            scale.z = rebateRebate.distance;
            rays[raycount].transform.localScale = scale;
            size += rebateRebate.distance;

        }
    }


    public void Fire()
    {
        if (safetyTime && firetimer > firecooldown)
        {
			Debug.Log("Firing");

            GameObject objectProjectile = Instantiate(projectile, firelocal, transform.rotation) as GameObject;
            Projectile proj = objectProjectile.GetComponent("Projectile") as Projectile;
            objectProjectile.GetComponent<Renderer>().material.color = kinect.color;
            //set owner of projectile
            proj.owner = this.gameObject.name;

            // TODO set color of projectile ~> change mash renderer material

            Transform instprojectile = objectProjectile.transform;
            if(Input.GetButton("Fire1_P1") || Input.GetButton("Fire1_P2"))
            {
                
                totalforce = (projectileforce / 20) * useDirection;
            }
            else
            {
                totalforce = (projectileforce / 10) * kinect.GetDirection() / kinect.transform.localScale.x;
                if (totalforce.x > projectileforce)
                {
                    totalforce.x = projectileforce;
                }
                if (totalforce.y > projectileforce)
                {
                    totalforce.y = projectileforce;
                }
            }

            instprojectile.GetComponent<Rigidbody>().AddForce(totalforce);


            AudioSource.PlayClipAtPoint(fire, transform.position, 9F);
            Physics.IgnoreCollision(instprojectile.GetComponent<Collider>(), transform.root.GetComponent<Collider>());
			anim.SetTrigger(shootHash);

            firetimer = 0;
        }

    }

    void RestartGame()
    {
        firstShoot = true;
    }



}