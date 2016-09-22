using UnityEngine;
using System.Collections;

public class MachineController : MonoBehaviour {

    
    public Vector3 positionStart;
    public Vector3 positionEnd;
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
            transform.position = Vector3.Lerp(positionStart,
                                              positionEnd,
                                              Mathf.SmoothStep(0f, 1f, Mathf.PingPong(Time.time / secondsForOneLength, 1f))
                                              );
        }
        


    }
    

    public void setMove(bool control)
    {
        canMove = control;
    }
}
