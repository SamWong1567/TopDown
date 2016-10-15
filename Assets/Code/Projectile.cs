using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
    
    //Variables
    public float lifeTime = 4;   
    public float damage;
    public float projectileSpeed;
    public Vector2 direction;
    public LayerMask layerMaskToBeCollided;
    public int percentChanceToPierce;
    public bool canPierce;

    //Components
    private Rigidbody2D rigidProj;
    
    
	void Start () {
        //Get Components
        rigidProj = GetComponent<Rigidbody2D>();

        //Get where the ball is facing when instantiated. This is in line with the parent Projectile Launcher
        direction = new Vector2(transform.up.x, transform.up.y);
        
        //Debug.Log(direction);        
        Destroy(gameObject, lifeTime);
        //StartCoroutine("Deploy");

        //RNG for pierce
        int projRNGRoll = Random.Range(1, 100);
        //Debug.Log("RNG: "+projRNGRoll);
        if (percentChanceToPierce >= projRNGRoll){

            canPierce = true;
        }
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

    //DOESNT WORK WITH PIERCING PROJECTILES
    //void OnCollisionEnter2D(Collision2D col) {

    //    GameObject c = col.gameObject;
    //    //Check layer mask that states what can the projectile collide with
    //    if ((layerMaskToBeCollided.value & 1 << col.gameObject.layer) != 0) {

    //        //Destroy(gameObject);

    //        if (c.GetComponent<MockEnemyScript>()) {
                                
    //            c.GetComponent<Entity>().TakeDamage(damage);
    //            c.GetComponent<MockEnemyScript>().UpdateHealth();
    //        }
    //    }
    //}

    void OnTriggerEnter2D(Collider2D col) {
        GameObject c = col.gameObject;
        //Check with layer mask which states what the projectile can interact with
        if ((layerMaskToBeCollided.value & 1 << c.layer) != 0) {        
                           
            if (c.GetComponent<MockEnemyScript>()) {

                //If project is set to cant pierce then destroy it
                if (!canPierce) {
                    Destroy(gameObject);
                }

                c.GetComponent<Entity>().TakeDamage(damage);
                c.GetComponent<MockEnemyScript>().UpdateHealth();
            }

            if (c.tag == "Obstacle") {
                Destroy(gameObject);
            }
        }
    }

}
