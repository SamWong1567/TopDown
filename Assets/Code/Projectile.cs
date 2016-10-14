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
                transform.Translate(Vector3.forward * projectileSpeed * Time.deltaTime);
                yield return null;
            }

            yield return new WaitForSeconds(.2f);
            if (Time.time > deathTime) {
                
                Destroy(gameObject);
            }
        }       
    }


    void OnTriggerEnter(Collider col) {
        if (col.tag == "Obstacle") {
            if (col.GetComponent<Entity>()) {
                col.GetComponent<Entity>().TakeDamage(damage);                
            }
            if (col.GetComponent<MockEnemyScript>()) {
                col.GetComponent<MockEnemyScript>().UpdateHealth();
            }
            Destroy(gameObject);
        }
    }

}
