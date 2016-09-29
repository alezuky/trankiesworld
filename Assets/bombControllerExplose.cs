using UnityEngine;
using System.Collections;

public class bombControllerExplose : MonoBehaviour {

    public ManagerTrainningLevel scriptManagerTrainningLevel;
	// Use this for initialization
	void Awake () {

        scriptManagerTrainningLevel = ScriptableObject.FindObjectOfType<ManagerTrainningLevel>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Projectile")
        {
            Debug.Log("Explode BombBall");
            scriptManagerTrainningLevel.bombExplode(this.name);
        }
        
    }
}
