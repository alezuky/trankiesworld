using UnityEngine;
using System.Collections;

public class RandomRotation : MonoBehaviour
{


    public Transform rotationStart;
    public Transform rotationEnd;
    private float secondsForOneLength = 20f;
    public bool canMove;
    public float time;


    void Start()
    {

    }

    void FixedUpdate()
    {
        if (canMove)
        {
            //rotationStart = new Vector3((Random.Range(rotationStart.x, rotationEnd.x)), (Random.Range(rotationStart.y, rotationEnd.y)), (Random.Range(rotationStart.z, rotationEnd.z)));
            //rotationEnd = new Vector3((Random.Range(rotationEnd.x, rotationStart.x)), (Random.Range(rotationEnd.y, rotationStart.y)), (Random.Range(rotationEnd.z, rotationStart.z)));

            transform.rotation = Quaternion.Lerp(rotationStart.rotation, rotationEnd.rotation, Mathf.SmoothStep(0f, 1f, Mathf.PingPong(Time.time / secondsForOneLength, 1f)));
        }
        
    }


    public void setMove(bool control)
    {
        canMove = control;
    }

}