 using UnityEngine;
 using System.Collections;
 
 public class ArmRotation : MonoBehaviour {
 
     // Update is called once per frame
     void Update() {

        var pos = new Vector3(Input.mousePosition.x,Input.mousePosition.y,0);
        Debug.Log("Transform"+ pos);
        transform.LookAt(pos);

    }
 }