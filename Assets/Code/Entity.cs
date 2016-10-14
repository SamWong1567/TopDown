using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour {

    public float maxHealth;
    public float currentHealth;

    public virtual void TakeDamage(float dmg) {

        currentHealth -= dmg;

        Debug.Log(currentHealth);

        if (currentHealth <= 0) {

            Die();
        }
    }

    public virtual void Die() {
        Debug.Log("Dead");
        Destroy(gameObject);

    }
	
}
