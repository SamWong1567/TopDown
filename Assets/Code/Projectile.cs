﻿using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
    
    //Variables
    public float lifeTime = 4;   
    public float damage;
    public float projectileSpeed;
    public Vector2 projectileDirection;
    public LayerMask layerMaskToBeCollided;
    public int percentChanceToPierce;
    public bool canPierce;
    public float knockBackStrength;
    private float moveDistance;

    //Components
    //private Rigidbody2D rigidProj;
   
    //System
    private float recentlyHitId=0;
    
	void Start () {
        //Get Components
        //rigidProj = GetComponent<Rigidbody2D>();

        //Get where the ball is facing when instantiated. This is in line with the parent Projectile Launcher
        projectileDirection = Vector2.up;
    
        //Destroy projectile after lifeTime (seconds) has passed
        Destroy(gameObject, lifeTime);
       
        CheckPierce();
    }

    void Update() {

        MoveProjectile();
    }


    void MoveProjectile() {

        moveDistance = projectileSpeed * Time.deltaTime;
        transform.Translate(projectileDirection * moveDistance);
    }

    void CheckPierce() {

        //RNG for pierce
        int projRNGRoll = Random.Range(1, 100);
        //Debug.Log("RNG: "+projRNGRoll);
        if (percentChanceToPierce >= projRNGRoll) {

            canPierce = true;
        }
    }
  
    void OnTriggerEnter2D(Collider2D col) {

        GameObject c = col.gameObject;
        //Check with layer mask which layers can the projectile interact with
        if ((layerMaskToBeCollided.value & 1 << c.layer) != 0) {

            if (c.tag == "Enemy") {

                Enemy enemyScript = c.GetComponent<Enemy>();
                if (recentlyHitId != enemyScript.enemyInstanceID) {
                    //Knockback
                    Rigidbody2D cRigid = c.GetComponent<Rigidbody2D>();
                    cRigid.AddForce(transform.TransformDirection(projectileDirection) * knockBackStrength);
                    enemyScript.TakeDamage(damage);
                }

                //If projectile cant pierce then destroy it
                if (!canPierce) {
                    Destroy(gameObject);
                }

            }

            //If triggers Obstacle, Destroy Projectile
            if (c.tag == "Obstacle") {
                Destroy(gameObject);
            }
        }
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
