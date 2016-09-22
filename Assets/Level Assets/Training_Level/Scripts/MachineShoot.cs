using UnityEngine;
using System.Collections;

public class MachineShoot : MonoBehaviour 

	{
		
		public GameObject projectile = null;
		public float projectileforce  = 1500f;
		public GameObject ray = null;
		private Vector3 totalforce;
		public float firecooldown;
		private float firetimer2;
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

        public GameObject proje;
        public string nameProjectile;
        public int numproject = 0;



        void Start ()
		{
			
			firecooldown = 0.1f;
			firetimer2 = 0f;
			firstShoot = true;
	
		}
		
		
		void Update ()
		{

            firelocal = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            firetimer2 = firetimer2 + Time.deltaTime;
            Debug.Log(firecooldown + " Updating firetimer " + firetimer2);

            //Point ();
            if (firetimer2 > firecooldown)
            {

                firetimer2 = firetimer2 - firecooldown;
                Fire();

            }
        
            //proje = GameObject.Find(nameProjectile);
            //proje.gameObject.GetComponent<Rigidbody>().AddForce(proje.transform.forward*1000);


        }


    public void Point ()
		{
			if (Physics.Raycast (transform.position, new Vector3(0,1,0), out hit)) {
				//Physics.IgnoreCollision (instprojectile.collider, transform.root.collider);
				if (firstShoot) {
				rays [raycount] = Instantiate (ray, firelocal, Quaternion.LookRotation (new Vector3(0,1,0))) as GameObject;
					rays[raycount].GetComponent<Renderer>().material.color = Color.magenta;
					//rays[raycount].GetComponent<Light>().color = kinect.color;
					firstShoot = false;
					Debug.Log ("Instantiating Machine Pointer");
				}
				
				rays [raycount].GetComponent<Renderer>().enabled = true;
				
				scale = rays [raycount].transform.localScale;
				scale.z = hit.distance;
				rays [raycount].transform.localScale = scale;
				//size += hit.distance;
				
				
				
				// Rebating the raycast is not working
				// That is why the if statement was set to "size < 0" -> it will never be accessed (because it does not work)
				if (size < 0 && (Physics.Raycast (rays [raycount].transform.position, hit.normal, out hitRebate)) == true) {
					
					if (rebate == false) {
						PointRebate ();
					} else {
						RebateRebate ();
					}
				} else {
					rebate = false;
					size = hit.distance;
					
					rays [raycount].transform.position = firelocal;
					rays [raycount].transform.rotation = Quaternion.LookRotation (new Vector3(0,1,0));
					
					scale = rays [raycount].transform.localScale;
					scale.z = hit.distance;
					rays [raycount].transform.localScale = scale;
					
					
				}
			} else {
				if (rays [raycount] != null) {
					//Destroy (rays [raycount].gameObject);
					//firstShoot = true;
					rays [raycount].GetComponent<Renderer>().enabled = false;
				}
			}
		}
		
		
		public void PointRebate ()
		{
			
			rays [raycount].transform.position = hit.point;
			rays [raycount].transform.rotation = Quaternion.LookRotation (hitRebate.normal);
			
			scale = rays [raycount].transform.localScale;
			scale.z = hitRebate.distance;
			rays [raycount].transform.localScale = scale;
			size += hitRebate.distance;
			
			rebate = true;
		}
		
		public void RebateRebate ()
		{
			if (Physics.Raycast (rays [raycount].transform.position, hitRebate.normal, out rebateRebate)) {
				Physics.Raycast (rays [raycount].transform.position, rebateRebate.normal, out hitRebate);
				rays [raycount].transform.position = hitRebate.point;
				rays [raycount].transform.rotation = Quaternion.LookRotation (rebateRebate.normal);
				
				scale = rays [raycount].transform.localScale;
				scale.z = rebateRebate.distance;
				rays [raycount].transform.localScale = scale;
				size += rebateRebate.distance;
				
			}
		}
		
		
		public void Fire () {
				/*
				GameObject objectProjectile = Instantiate (projectile, firelocal, transform.rotation) as GameObject;
				MachineProjectile proj = objectProjectile.GetComponent("Projectile") as MachineProjectile;
				objectProjectile.GetComponent<Renderer>().material.color = Color.yellow;
				//set owner of projectile
				proj.owner = this.gameObject.name;
*/
				// TODO set color of projectile ~> change mash renderer material
				
				GameObject instprojectile = Instantiate (projectile, firelocal, transform.rotation) as GameObject;

                instprojectile.name = "project" + numproject;
                nameProjectile = instprojectile.name;

                //instprojectile.gameObject.GetComponent<Rigidbody>().AddForce (new Vector3(projectileforce,projectileforce,projectileforce));
                AudioSource.PlayClipAtPoint(fire, transform.position);
				//Physics.IgnoreCollision (instprojectile.GetComponent<Collider>(), transform.root.GetComponent<Collider>());

		        Debug.Log (firecooldown + " Firing " + firetimer2);
		
			
		}
		
		void RestartGame()
		{
			firstShoot = true;
		}
		
		
		
	}
