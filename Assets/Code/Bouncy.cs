using UnityEngine;
using System.Collections;

public class Bouncy : MonoBehaviour {

    private float projectileId=1f;
    Projectile projectileScript;
    void OnTriggerEnter2D(Collider2D col) {

        GameObject c = col.gameObject;
        

        if (c.tag == "Projectile") {
            projectileScript = c.GetComponent<Projectile>();
            //To prevent projectile from bouncing again
            if (projectileId != projectileScript.projectileInstanceId) {
                projectileScript.projectileDirection = projectileScript.projectileDirection * -1;
                projectileId = projectileScript.projectileInstanceId;
            }
        }
    }
}
