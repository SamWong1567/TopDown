using UnityEngine;
using System.Collections;

public class Bouncy : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D col) {

        GameObject c = col.gameObject;
        
        if (c.tag == "Projectile") {
            Projectile projectileScript = c.GetComponent<Projectile>();
            projectileScript.projectileDirection = projectileScript.projectileDirection * -1;

            //Set projectile collider layer mask to collide with player layer ONLY
            projectileScript.layerMaskToBeCollided.value = 1 << 9;

        }
    }

    //void OnCollisionEnter2D(Collision2D col) {

    //    GameObject c = col.gameObject;

    //    if (c.tag == "Projectile") {
    //        c.GetComponent<Projectile>().projectileDirection = c.GetComponent<Projectile>().projectileDirection * -1;
    //    }

    //    }
    }
