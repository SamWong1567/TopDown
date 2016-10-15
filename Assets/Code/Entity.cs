using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour {

    //Variables
    public float maxHealth;
    public float currentHealth;

    //Take Damage method
    public virtual void TakeDamage(float dmg){

        currentHealth -= dmg;
        Debug.Log(currentHealth);

        //Check if current health drops below 0
        if (currentHealth <= 0) {
            Die();
        }
    }

    public virtual void Die(){
        Debug.Log("Dead");
        Destroy(gameObject);

    }
	
}
