using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TargetFollowMouse : MonoBehaviour {
    
    private float offset = 1.5f;
    Vector3 pos;
    public Text indentified;


    //public Transform lastMarker;
    //public Transform newMarker;
    //public float speed = 1.0F;
    //private float startTime;
    //private float journeyLength;

    // Use this for initialization
    void Start () {
        //startTime = Time.time;      
        indentified = GameObject.Find("Identifier_text").GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {

        transform.position = pos;

        //journeyLength = Vector3.Distance(lastMarker.position, newMarker.position);

        //lastMarker.position = pos;
        //float distCovered = (Time.time - startTime) * speed;
        //float fracJourney = distCovered / journeyLength;
        //StartCoroutine(WaitForNewMark());
        //transform.position = Vector3.Lerp(lastMarker.position, newMarker.position, fracJourney);


        
        //Ray cursorRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        //RaycastHit hit;
        //if (Physics.Raycast(cursorRay, out hit))
        //{
        //    //transform.position= new Vector3(hit.point.x + offset * Vector3.up.x, hit.point.y + offset * Vector3.up.y, 0);
        //    transform.position = new Vector3(hit.transform.position.x + offset, hit.transform.position.y + offset, 0);
        //    //transform.position = new Vector3(hit.point.x, hit.point.y, 0);
        //}


        ////pos = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
        //////transform.position = pos;

    }

    public void setPositionTarget(RaycastHit hitPositionReceived) {

        pos = new Vector3(hitPositionReceived.transform.position.x+.05f, hitPositionReceived.transform.position.y+1.5f, 0);
        indentified.text = hitPositionReceived.collider.name.ToString() == "Player_2" ? "???????" : hitPositionReceived.collider.name.ToString(); 


    }

    //public Vector3 getNewMark() {
    //    return pos;
    //}

    //IEnumerator WaitForNewMark()
    //{
    //    yield return new WaitForSeconds(5);
    //    newMarker.position = getNewMark();
    //}
}
