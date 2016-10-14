using UnityEngine;
using System.Collections;

public class MockEnemyScript : Entity {

    private Player player;
    private GUI health;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        health = transform.GetChild(0).GetComponent<GUI>();

	}

    void OnTriggerEnter(Collider c) {
        if (c.tag == "Player") {
            player.TakeDamage(1);
            player.UpdateHealth();
        }

    }

    public void UpdateHealth() {
        health.SetHealth(currentHealth / maxHealth);
    }
}
