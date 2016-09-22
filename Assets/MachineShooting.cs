using UnityEngine;
using System.Collections;

public class MachineShooting : MonoBehaviour {

    public int damagePerShot = 20;
    public float timeBetweenBullets = 0.15f;
    public float range = 30f;
    public MachineController machinePlataform;

    float timer;
    Ray shootRay;
    RaycastHit shootHit;
    int shootableMask;
    ParticleSystem machineParticles;
    LineRenderer machineLine;
    AudioSource machineAudio;
    Light machineLight;
    public float effectsDisplayTime = 0.1f;


	// Use this for initialization
	void Awake () {

        shootableMask = LayerMask.GetMask("Shootable");
        machineParticles = GetComponent<ParticleSystem>();
        machineLine = GetComponent<LineRenderer>();
        machineAudio = GetComponent<AudioSource>();
        machineLight = GetComponent<Light>();
        


    }
	
	// Update is called once per frame
	void Update () {
        // Add the time since Update was last called to the timer.
        timer += Time.deltaTime;
        timeBetweenBullets = Random.Range(3f, 5f);
        //
        if (timer >= timeBetweenBullets)
        {
            // ... shoot the machine.
            Shoot();
            PlataformCantMove();
        }

        // If the timer has exceeded the proportion of timeBetweenBullets that the effects should be displayed for...
        if (timer >= timeBetweenBullets * effectsDisplayTime)
        {
            // ... disable the effects.
            DisableEffects();
            PlataformCanMove();
        }
    }

    public void DisableEffects()
    {
        // Disable the line renderer and the light.
        machineLine.enabled = false;
        machineLight.enabled = false;
    }

    void Shoot()
    {
        // Reset the timer.
        timer = 0f;

        // Play the gun shot audioclip.
        machineAudio.Play();

        // Enable the light.
        machineLight.enabled = true;

        // Stop the particles from playing if they were, then start the particles.
        machineParticles.Stop();
        machineParticles.Play();

        // Enable the line renderer and set it's first position to be the end of the machine.
        machineLine.enabled = true;
        machineLine.SetPosition(0, transform.position);



        // Set the shootRay so that it starts at the end of the machine and points forward from the barrel.
        shootRay.origin = new Vector3(-25, 10, 0);
        shootRay.direction = new Vector3(transform.up.x, transform.up.y, 0);


        // Perform the raycast against gameobjects on the shootable layer and if it hits something...
        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
        {
            // Try and find an Player script on the gameobject hit.
            TrankiesHealth trankiesHealth = shootHit.collider.GetComponent<TrankiesHealth>();
            
            // If the EnemyHealth component exist...
            if (trankiesHealth != null)
            {
                // ... the enemy should take damage.
                trankiesHealth.TakeDamage(damagePerShot);
                //}

                // Set the second position of the line renderer to the point the raycast hit.
                machineLine.SetPosition(1, shootHit.point);
            }
            // If the raycast didn't hit anything on the shootable layer...
            else
            {
                // ... set the second position of the line renderer to the fullest extent of the gun's range.
                machineLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
            }
        }

        
       }

    void PlataformCanMove()
    {
        machinePlataform.setMove(true);
    }

    void PlataformCantMove()
    {
        machinePlataform.setMove(false);
    }


}
