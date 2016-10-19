    using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    //public Variables
    public float lifeTime;
    public float damage;
    public float projectileSpeed;
    public float size;
    public bool canHome;

    public Vector2 projectileDirection;
    public LayerMask layerMaskToBeCollided;
    public LayerMask overlapCircleCollider;
    public float knockBackStrength;

    public float percentChanceToPierce { get; set; }

    [Range(0, 1)]
    public float percentReflectedDamage;

    //Components
    //private Rigidbody2D rigidProj;
    //System
    public float recentlyHitId { get; set; }
    public float projectileInstanceId { get; set; }
    private float moveDistance;
    private bool canPierce;
    public GameObject closestEnemy { get; set; }
    Enemy enemyScript;
    Rigidbody2D cRigid;
    private Collider2D hitColliders;

    void Start() {
        //Get Components
        //rigidProj = GetComponent<Rigidbody2D>();
        transform.localScale = Vector3.one * size;
        //Get where the ball is facing when instantiated. This is in line with the parent Projectile Launcher
        projectileDirection = Vector2.up;

        //Give an Id to the new instance of proj
        projectileInstanceId = Time.time + (Random.Range(0, 100));

        //Destroy projectile after lifeTime (seconds) has passed
        Destroy(gameObject, lifeTime);

        CheckPierce();
       //hitColliders = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y), 20, overlapCircleCollider);
    }

    void Update() {

        MoveProjectile();

        if (canHome) {

            RotateToFaceEnemy();
        }                  
    }


    void MoveProjectile() {

        moveDistance = projectileSpeed * Time.deltaTime;
        transform.Translate(projectileDirection * moveDistance);
    }

    //Homing
    void RotateToFaceEnemy() {
        if (closestEnemy != null) {
            float rotationSpeed = 4;
            Vector3 targetDirection = closestEnemy.transform.position - transform.position;
            //From to rotation, creates a rotation from the first vector to second vector
            Quaternion targetRotation = Quaternion.FromToRotation(Vector3.up, targetDirection);
            //print(targetRotation.eulerAngles);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
     //Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
     //Gizmos.DrawWireSphere(transform.position, 20);

    }

    void CheckPierce() {

        //RNG for pierce
        float projRNGRoll = Random.Range(1, 100);
        //Debug.Log("RNG: "+projRNGRoll);
        if (percentChanceToPierce * 100 >= projRNGRoll) {

            canPierce = true;
        }
    }

    public virtual void OnTriggerEnter2D(Collider2D col) {

        GameObject c = col.gameObject;

        //Check with layer mask which layers can the projectile interact with
        if ((layerMaskToBeCollided.value & 1 << c.layer) != 0) {

            if (c.tag == "Enemy") {
                enemyScript = c.GetComponent<Enemy>();
                EnemyMods enemyModsScript = c.GetComponent<EnemyMods>();

                if (!ObjectHasAlreadyBeenHit()) {
                    cRigid = c.GetComponent<Rigidbody2D>();

                    //Knockback
                    enemyScript.KnockBackCaller();
                    //Enemy takes damage
                    enemyScript.TakeDamage(damage);
                    //Set recentlyHitId to the enemy that was just hit
                    recentlyHitId = enemyScript.enemyInstanceID;
                }

                //If projectile cant pierce then destroy it
                if (enemyModsScript) {
                    if (!enemyModsScript.reflect) {

                        Destroy(gameObject);
                    }
                }else
                if (!canPierce) {

                    Destroy(gameObject);
                }
               
            }

            //If triggers Obstacle, Destroy Projectile
            if (c.tag == "Obstacle") {
                Destroy(gameObject);
            }

            if (c.tag == "Player") {
                Destroy(gameObject);
                c.gameObject.GetComponent<Entity>().TakeDamage(damage * percentReflectedDamage);
            }
        }
    }

    bool ObjectHasAlreadyBeenHit() {

        //Check if projectile has already hit the enemy with the matching recentlyHitId
        if (recentlyHitId == enemyScript.enemyInstanceID) {

            return true;
        }

        return false;

    }
    //BELOW ARE UNUSED CODE

    //void FixedUpdate() {

    //    Move Projectile
    //    rigidProj.MovePosition(rigidProj.position + projectileDirection * projectileSpeed * Time.fixedDeltaTime);
    //}

    ////DOESNT WORK WITH PIERCING PROJECTILES
    //void OnCollisionEnter2D(Collision2D col) {
    //    //rigidProj.isKinematic = true;
    //    GameObject c = col.gameObject;
    //    //Check layer mask that states what can the projectile collide with
    //    if ((layerMaskToBeCollided.value & 1 << col.gameObject.layer) != 0) {

    //if (c.tag == "Enemy") {
    //    Rigidbody2D cRigid = c.GetComponent<Rigidbody2D>();
    //    cRigid.AddForce(projectileDirection * knockBackStrength);
    //}

    //        Destroy(gameObject);

    //        if (c.GetComponent<MockEnemyScript>()) {

    //            c.GetComponent<Entity>().TakeDamage(damage);
    //            c.GetComponent<MockEnemyScript>().UpdateHealth();
    //        }
    //    }
    //}

    //void CheckCollisions(float moveDistance) {
    //    RaycastHit2D hit = Physics2D.Raycast(transform.position, projectileDirection, moveDistance, layerMaskToBeCollided);

    //    if (hit) {

    //        OnHitObject(hit);
    //    }
    //}

    //void OnHitObject(RaycastHit2D hit) {
    //    if (hit.transform.tag == "Obstacle") {

    //        Destroy(gameObject);
    //    }

    //    Enemy enemyScript = hit.collider.gameObject.GetComponent<Enemy>();

    //    if (hit.transform.tag == "Enemy") {
    //        if (recentlyHitId != enemyScript.enemyInstanceID) {
    //            Rigidbody2D cRigid = hit.collider.gameObject.GetComponent<Rigidbody2D>();
    //            cRigid.AddForce(transform.TransformDirection(projectileDirection) * knockBackStrength);

    //            enemyScript.TakeDamage(damage);                
    //        }
    //        recentlyHitId = enemyScript.enemyInstanceID;

    //        if (!canPierce) {
    //            Destroy(gameObject);
    //        }
    //    }

    //}
}
