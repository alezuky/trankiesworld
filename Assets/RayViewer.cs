using UnityEngine;
using System.Collections;

public class RayViewer : MonoBehaviour {

    public float weaponRange = 50;
    MachineShooting machineShooting;

    void Start () {
	
	}
	
	void Update () {
        Vector3 lineOrigin = transform.up;
        Debug.DrawRay(lineOrigin, transform.forward * weaponRange, Color.green);
    }
}
