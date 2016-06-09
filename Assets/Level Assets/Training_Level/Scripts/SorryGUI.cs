using UnityEngine;
using System.Collections;

public class SorryGUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
		iTween.ScaleAdd(gameObject,iTween.Hash("amount",new Vector3(0.1f,0.1f,0),"time",1,"looptype",iTween.LoopType.pingPong,"easetype",iTween.EaseType.linear,"delay",Random.Range(0,.4f)));
	}
	
}
