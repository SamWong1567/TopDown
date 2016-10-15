using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
    
    //Variables
    public float lifeTime = 4;   
    public float damage;
    public float projectileSpeed;
    public Vector2 direction;

    //Components
    private Rigidbody2D rigidProj;
    
    
	void Start () {
        //Get Components
        rigidProj = GetComponent<Rigidbody2D>();

        //Get where the ball is facing when instantiated. This is in line with the parent Projectile Launcher
        direction = new Vector2(transform.up.x, transform.up.y);
        
        Debug.Log(direction);        
        Destroy(gameObject, lifeTime);
        //StartCoroutine("Deploy");
    }

    void FixedUpdate() {

        rigidProj.MovePosition(rigidProj.position +  direction * projectileSpeed * Time.fixedDeltaTime);
    }
   

    //IEnumerator Deploy() {        
    //        while (this) {
    //            transform.Translate(Vector3.up * projectileSpeed * Time.deltaTime);
    //            yield return null;
    //        }             
    //}


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
