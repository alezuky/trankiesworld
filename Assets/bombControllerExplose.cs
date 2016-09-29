using UnityEngine;
using System.Collections;

public class bombControllerExplose : MonoBehaviour {

    [SerializeField]
    ManagerTrainningLevel scriptManagerTrainningLevel;
    private ParticleSystem hitBombExplosion;
    public GameObject bombBallExplosionPrefab;
    private GameObject bombBallParticle;


    // Use this for initialization
    void Awake () {

        scriptManagerTrainningLevel = ScriptableObject.FindObjectOfType<ManagerTrainningLevel>();
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Projectile")
        {
            bombBallParticle = Instantiate(bombBallExplosionPrefab);
            hitBombExplosion = bombBallParticle.GetComponent<ParticleSystem>();

            Debug.Log("Explode BombBall");
            scriptManagerTrainningLevel.bombExplode(this.name);
            hitBombExplosion.transform.position = this.transform.position;
            hitBombExplosion.Play();



        }
        
    }
}
