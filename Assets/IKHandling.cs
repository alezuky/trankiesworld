using UnityEngine;
using System;
using System.Collections;


public class IKHandling : MonoBehaviour
{
    Animator anim;

    public float ikWeight;

    public Transform leftIKTarget;
    public Transform rightIKTarget;


    void Start() {
        anim = GetComponent<Animator>();
    }

    void Update() {

    }

    void onAnimatorIK()
    {
        

    }
    
}
