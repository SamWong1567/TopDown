using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class EnemyMods : Enemy {
    //MOD LIST
    //REMEMBER TO UPDATE POPULATEMODLIST WHENVER ADDING NEW MODS
    public bool reflect;
    public bool charger;

    //public variables
    public float chargerSpeed;
    public LayerMask layerMask;

    List<bool> modsActivatorBool;
    List<Action> modMethods = new List<Action>();

    public GameObject[] LootTable;
    
    public override void Start() {
        base.Start();
        //Get components
        thisRigidbody = transform.GetComponent<Rigidbody2D>();
       
        PopulateModList();

        if (charger) {
            StartCoroutine("Charger");
        }
    }

    void PopulateModList() {

        modsActivatorBool = new List<bool>();

        modsActivatorBool.Add(reflect);
        modsActivatorBool.Add(charger);


        modMethods.Add(() => Reflect());
        modMethods.Add(() => Charger());


        //set enemyScript.hasMods to true if this script is attached
        foreach (bool b in modsActivatorBool) {
            if (b == true) {

                hasMods = true;
            }
        }

    }

    public override void MoveToPlayer() {
        if (!charger) {
            base.MoveToPlayer();
            return;
        }
    }

    public override void OnTriggerEnter2D(Collider2D col) {
        if((layerMask.value & 1<<col.transform.gameObject.layer) != 0) {
   
        base.OnTriggerEnter2D(col);

            if (reflect) {

                //The coroutine below calls the Reflect() after one frame
                //this is done to make sure the Projectile OnTrigger event is called first before the Reflect()
                //If not, the Reflect() will change the Projectile layer mask to not hit enemies
                //and will not trigger the Projectile OnTrigger
                StartCoroutine("CallReflectAfterOneFrame");
            }
        }
    }

    void Reflect() {

        if (projectileId != projectileScript.projectileInstanceId) {
            //Reflect
            projectileScript.projectileDirection = projectileScript.projectileDirection * -1;
            onTriggerEnterObject.GetComponent<Renderer>().material.color = Color.blue;
            //change projectile layermask to only hit player
            projectileScript.layerMaskToBeCollided.value -= 1 << 11;
            projectileScript.layerMaskToBeCollided.value += 1 << 9;
            projectileId = projectileScript.projectileInstanceId;
        }
    }

    IEnumerator CallReflectAfterOneFrame() {
        yield return null;
        Reflect();
    }

    IEnumerator Charger() {

        float chargeDuration = 0.3f;
        float secondsBetweenCharges = 1;
        float startTime;
        yield return new WaitForSeconds(0.5f);
        while (player) {

            currentState = State.Attacking;
            playerDirection = (player.transform.position - transform.position).normalized;
            startTime = Time.time;
            while (startTime + chargeDuration > Time.time) {
                enemyRigidbody.MovePosition(enemyRigidbody.position + playerDirection * chargerSpeed * Time.fixedDeltaTime);
                yield return new WaitForFixedUpdate();
            }
            currentState = State.Idle;
            yield return new WaitForSeconds(secondsBetweenCharges);
        }
    }
    public override void Die() {
        base.Die();
        int rng = UnityEngine.Random.Range(1, 100);
        int puRng = UnityEngine.Random.Range(0, LootTable.Length);

        if (reflect) {
            Reflect();
        }

        if (50 > rng) {
            Instantiate(LootTable[puRng], transform.position, Quaternion.identity);
        }
    }
    
    //Not practical
    ////check if mod is activated, if true then call the action
    //for (int i = 0; i<modsActivatorBool.Count; i++) {

    //            if (modsActivatorBool[i]) {
    //                modMethods[i]();
    //            }
    //        }
}

