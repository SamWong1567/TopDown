using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    private float lifeTime = 4;
    private float deathTime;

    public float damage;
    public float projectileSpeed;

	// Use this for initialization
	void Start () {
        deathTime = Time.time + lifeTime;
        StartCoroutine("Destroy");
	}

    IEnumerator Destroy() {
        while (true) {
            while (this) {
                transform.Translate(Vector3.up * projectileSpeed * Time.deltaTime);
                yield return null;
            }

            yield return new WaitForSeconds(.2f);
            if (Time.time > deathTime) {
                
                Destroy(gameObject);
            }
        }       
    }


    void OnCollisionEnter2D(Collision2D col) {
        //Debug.Log("Collided");
       
        GameObject c = col.gameObject;

        //If hits obstacle destroy proj
        if (c.tag == "Obstacle") {
            Destroy(gameObject);
        }
        //if hit enemy, enemy takes damage, enemy health bar updates, destroy proj
        if (c.tag == "Enemy") {

            if (c.GetComponent<MockEnemyScript>()) {
                Destroy(gameObject);
                Debug.Log("Collided with enemy");
                c.GetComponent<Entity>().TakeDamage(damage);
                c.GetComponent<MockEnemyScript>().UpdateHealth();
            }
            
        }        
        
    }

}
