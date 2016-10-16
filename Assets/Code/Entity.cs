using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour {

    //Public Variables
    public float maxHealth;
    protected float currentHealth;
    public GUI healthGUI { get; set; }

    public virtual void Start() {
        currentHealth = maxHealth;

    }
    //Take Damage method
    public virtual void TakeDamage(float dmg){
        
        currentHealth -= dmg;
        //Debug.Log("health: " + currentHealth);
        UpdateHealth();
        //Check if current health drops below 0
        if (currentHealth <= 0) {
            Die();
        }

    }

    public virtual void Die(){
        //Debug.Log("Dead");
        Destroy(gameObject);

    }


    public void UpdateHealth() {
        healthGUI.SetHealth(currentHealth / maxHealth);
    }
}
