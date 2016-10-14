using UnityEngine;
using System.Collections;

public class EnemyDamageOnTouch : MonoBehaviour {
    private Player player;
    private MockEnemyScript enemy;

    void Start() {
        enemy = transform.parent.GetComponent<MockEnemyScript>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
       

    }
    void OnTriggerEnter(Collider c) {
        if (c.tag == "Player") {
            player.TakeDamage(enemy.damageOnTouch);
            player.UpdateHealth();

        }

    }
}
