using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    private float lifeTime = 4;
    private float deathTime;

	// Use this for initialization
	void Start () {
        deathTime = Time.time + lifeTime;
        StartCoroutine("Destroy");
	}

    IEnumerator Destroy() {
        while (true) {
            yield return new WaitForSeconds(.2f);
            if (Time.time > deathTime) {
                Destroy(gameObject);
            }
        }       
    }


    void OnTriggerEnter(Collider col) {
        if (col.tag == "Obstacle") {           
            Destroy(gameObject);
        }
    }

}
