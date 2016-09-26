using UnityEngine;
using System.Collections;

public class MachineShooting : MonoBehaviour {

    public int damagePerShot = 1;
    public float fireRate = 2f;
    public float weaponRange = 100f;
    public float hitForce = 100f;
    public Transform machineEnd;
    public GameObject projectile = null;

    private WaitForSeconds shotDuration = new WaitForSeconds(2f);
    private AudioSource machineAudio;
    private LineRenderer machineLine;
    private float nextFire = 0.25f;
    private Vector3 posOrigin;

    public MachineController machinePlataform;

    
    public Ray shootRay;
    RaycastHit shootHit = new RaycastHit();
    int shootableMask;
    ParticleSystem machineParticles;


    Light machineLight;
   

    // Use this for initialization
    void Awake () {

        // Create a layer mask for the Shootable layer.
        shootableMask = LayerMask.GetMask("Shootable");
        machineParticles = GetComponent<ParticleSystem>();
        machineLine = GetComponent<LineRenderer>();
        machineAudio = GetComponent<AudioSource>();
        machineLight = GetComponent<Light>();
        machineEnd = GetComponent<Transform>();



    }

    // Update is called once per frame
    void Update () {

        //shootRay.origin = new Vector3(0 , 0, 0);

        if (Time.time > nextFire)
        {
            
            nextFire = Time.time + fireRate;

            StartCoroutine(ShotEffect());

            //shootRay.origin = new Vector3(transform.position.x, transform.position.y, 0);
            //shootRay.origin = new Vector3(transform.position.x+ weaponRange, transform.position.y + weaponRange, 0);
            //shootRay.origin = new Vector3(transform.position.x + weaponRange, transform.position.y + weaponRange, 0);
            shootRay.origin = machineEnd.transform.position;
            //No change This Code Nameku ¬_¬
            //shootRay.direction = new Vector3(Random.Range(-transform.up.x, transform.up.x), Random.Range(-transform.up.y, transform.up.y), Random.Range(-transform.up.z, transform.up.y));
            //No change This Code Nameku ¬_¬
            //shootRay.direction = new Vector3(transform.position.x + weaponRange, transform.position.y + weaponRange, 0);
            shootRay.direction = new Vector3(1,0,0);
            PlataformCanMove();

            machineLine.SetPosition(0, new Vector3(1, 0, 0));
            machineLine.SetPosition(1, new Vector3(0, 0, 0));
            
            
            if (Physics.Raycast(shootRay.origin, shootRay.direction, out shootHit, weaponRange))
            {

                
                machineLine.SetPosition(0, shootHit.point);
                
                Debug.Log("Hitou"+ shootHit.point);

                TrankiesHealth health = shootHit.collider.GetComponent<TrankiesHealth>();
                if (health != null)
                {
                    health.TakeDamage(damagePerShot);
                    
                }

                if (shootHit.rigidbody != null)
                {
                    shootHit.rigidbody.AddForce(-shootHit.normal * hitForce);
                }
            }
            else
            {
                machineLine.SetPosition(1, shootRay.origin + (shootRay.direction * weaponRange));
                //machineLine.SetPosition(1, shootRay.origin + shootRay.direction * weaponRange);
                //machineLine.SetPosition(1, new Vector3(0, 60, 0));
                
                //machineLine.SetPosition(1, new Vector3(0, 0, 0));

            }
        }


    }

    private IEnumerator ShotEffect()
    {
        // Disable the line renderer and the light.
        machineLine.enabled = true;
        machineLight.enabled = true;
        yield return shotDuration;
        machineLine.enabled = false;
        machineLight.enabled = false;
    }

    //void Shoot()
    //{
    //    // Reset the timer.
    //    timer = 0f;

    //    // Play the gun shot audioclip.
    //    //machineAudio.Play();

    //    // Enable the light.
    //    machineLight.enabled = true;

    //    // Stop the particles from playing if they were, then start the particles.
    //    machineParticles.Stop();
    //    machineParticles.Play();

    //    // Enable the line renderer and set it's first position to be the end of the machine.
    //    // Enable the line renderer and set it's first position to be the end of the gun.
    //    machineLine.enabled = true;
    //    machineLine.SetPosition(0, transform.position);



    //    // Set the shootRay so that it starts at the end of the machine and points forward from the barrel.
    //    //shootRay.origin = new Vector3(-25, 10, 0);
    //    //shootRay.direction = new Vector3(transform.up.x, transform.up.y, 0);

    //    shootRay.origin = transform.position;
    //    shootRay.direction = transform.forward;

    //    Debug.DrawRay(shootRay.origin, shootRay.direction, Color.red);
    //    if (Physics.Raycast(shootRay.origin, shootRay.direction, out shootHit, 30))
    //    {
            
    //        if (shootHit.collider.gameObject.name == "Player_1")
    //        {
    //            Debug.Log("HIT");
    //        }
    //    }

    //    // Perform the raycast against gameobjects on the shootable layer and if it hits something...
    //    if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
    //    {
            
    //        // Try and find an Player script on the gameobject hit.
    //        TrankiesHealth trankiesHealth = shootHit.collider.GetComponent<TrankiesHealth>();

    //        // If the TrankiesHealth component exist...
    //        if (trankiesHealth != null)
    //        {
    //            // ... the Trankies should take damage.
    //            trankiesHealth.TakeDamage(damagePerShot);
    //            //}

    //            // Set the second position of the line renderer to the point the raycast hit.
    //            machineLine.SetPosition(1, shootHit.point);
    //        }
    //        // If the raycast didn't hit anything on the shootable layer...
    //        else
    //        {
    //            // ... set the second position of the line renderer to the fullest extent of the gun's range.
    //            machineLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
    //        }
    //    }
        
    //   }

    void PlataformCanMove()
    {
        machinePlataform.setMove(true);
    }

    void PlataformCantMove()
    {
        machinePlataform.setMove(false);
    }


}
