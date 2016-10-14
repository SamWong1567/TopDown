using UnityEngine;
using System.Collections;

public class MockEnemyScript : Entity {

    private Player player;
    public float damageOnTouch;
    private GUI health;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        health = transform.GetChild(0).GetComponent<GUI>();

	}



    public void UpdateHealth() {
        health.SetHealth(currentHealth / maxHealth);
    }
}
