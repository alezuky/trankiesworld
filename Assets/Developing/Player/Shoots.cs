using UnityEngine;
using System.Collections;

public class Shoots : MonoBehaviour
{
		private bool safetyTime = false;

		public GameObject projectile = null;
		public float projectileforce  = 15000f;
		public GameObject ray = null;
		public GestureListener kinect;
		private Vector3 totalforce;
		public float firecooldown;
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


		public Animator anim;
		int shootHash = Animator.StringToHash("shoot");

	IEnumerator SafetyTime() {
		yield return new WaitForSeconds(3.0f);
		safetyTime = true;		
	}


	void Awake ()
		{

		anim = gameObject.GetComponent<Animator> ();
		firecooldown = 1f;
		firetimer = 0;
		firstShoot = true;

		}
		
		void Start ()
		{


				StartCoroutine("SafetyTime");

				// Matches with the script for Kinect
				kinect = this.GetComponent<GestureListener> ();


		}


	void FixedUpdate ()
	{

			firelocal = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
			firetimer = firetimer + Time.deltaTime;
			if (kinect.tracked == true) {
			Point ();
			}

		if (kinect.SetFire()) {
			gameObject.SendMessageUpwards("Fire");
			anim.SetTrigger(shootHash);						
		}

	}


		public void Point ()
		{
				if (Physics.Raycast (transform.position, kinect.GetDirection (), out hit)) {
					//Physics.IgnoreCollision (instprojectile.collider, transform.root.collider);
			Debug.Log ("Poiting");
						if (firstShoot) {
								rays [raycount] = Instantiate (ray, firelocal, Quaternion.LookRotation (kinect.GetDirection ())) as GameObject;
								//rays[raycount].GetComponent<Renderer>().material.color = kinect.color;
								//rays[raycount].GetComponent<Light>().color = kinect.color;
								firstShoot = false;
								Debug.Log ("Instantiating Pointer");
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
								rays [raycount].transform.rotation = Quaternion.LookRotation (kinect.GetDirection ());

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
		if (safetyTime && firetimer > firecooldown) {

			GameObject objectProjectile = Instantiate (projectile, firelocal, transform.rotation) as GameObject;
			Projectile proj = objectProjectile.GetComponent("Projectile") as Projectile;
			objectProjectile.GetComponent<Renderer>().material.color = kinect.color;
			//set owner of projectile
			proj.owner = this.gameObject.name;

			// TODO set color of projectile ~> change mash renderer material

			Transform instprojectile = objectProjectile.transform;
			totalforce = (projectileforce / 10) * kinect.GetDirection () / kinect.transform.localScale.x;;
			if (totalforce.x > projectileforce) {
				totalforce.x = projectileforce;
			}
			if (totalforce.y > projectileforce) {
				totalforce.y = projectileforce;
			}

			instprojectile.GetComponent<Rigidbody>().AddForce (totalforce);
			AudioSource.PlayClipAtPoint(fire, transform.position);
			Physics.IgnoreCollision (instprojectile.GetComponent<Collider>(), transform.root.GetComponent<Collider>());

			firetimer = 0;
		}

	}

	void RestartGame()
	{
		firstShoot = true;
	}



}


