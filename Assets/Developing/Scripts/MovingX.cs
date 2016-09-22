using UnityEngine;
using System.Collections;

public class MovingX : MonoBehaviour {

    

	void Start (){
        ////gameObject.MoveAdd(new Vector3(20,0,0),5,0);
        iTween.MoveAdd(gameObject, iTween.Hash("x", transform.position.x + 20, "time", 5, "looptype", iTween.LoopType.pingPong, "easetype", iTween.EaseType.linear, "delay", Random.Range(0, .4f)));
        gameObject.RotateAdd(new Vector3(0, 360, 0), 5, 0);

    }
    void update() {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0);
    }    

}
