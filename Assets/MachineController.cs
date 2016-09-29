using UnityEngine;
using System.Collections;

public class MachineController : MonoBehaviour {

    
    public Vector3 positionStart;
    public Vector3 positionEnd;
    public float secondsForOneLength = 20f;
    public bool canMove;
    
    

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

    public void changeStatus()
    {
        this.gameObject.SetActive(true);
    }

    public void changeTime(float timeChanged)
    {
        secondsForOneLength = timeChanged;
    }
}
