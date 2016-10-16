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
            c.GetComponent<Projectile>().projectileDirection = c.GetComponent<Projectile>().projectileDirection * -1;
        }
    }

    //void OnCollisionEnter2D(Collision2D col) {

    //    GameObject c = col.gameObject;

    //    if (c.tag == "Projectile") {
    //        c.GetComponent<Projectile>().projectileDirection = c.GetComponent<Projectile>().projectileDirection * -1;
    //    }

    //    }
    }
