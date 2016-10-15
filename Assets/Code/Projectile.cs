using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    public float lifeTime = 4;

    public float damage;
    public float projectileSpeed;
    private Rigidbody2D rigidProj;
    public Vector2 direction;
    
	// Use this for initialization
	void Start () {
        direction = new Vector2(transform.up.x, transform.up.y);
        rigidProj = GetComponent<Rigidbody2D>();
        Debug.Log(direction);
        //StartCoroutine("Deploy");
        Destroy(gameObject, lifeTime);
	}

    void FixedUpdate() {

        rigidProj.MovePosition(rigidProj.position +  direction * projectileSpeed * Time.fixedDeltaTime);
    }
   

    IEnumerator Deploy() {
        
            while (this) {
                transform.Translate(Vector3.up * projectileSpeed * Time.deltaTime);
                yield return null;
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
