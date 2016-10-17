using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour {

    //Public Variables
    public float maxHealth;
    public float currentHealth { get; set; }
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
    public virtual void Heal(float healAmount) {
        currentHealth += healAmount;
        if (currentHealth > maxHealth) {

            currentHealth = maxHealth;
        }
        UpdateHealth();

    }
    public virtual void Die(){
        //Debug.Log("Dead");
        Destroy(gameObject);

    }


    public void UpdateHealth() {
        healthGUI.SetHealth(currentHealth / maxHealth);
    }
}
