using UnityEngine;
using System.Collections;

public class Enemy : Entity {

    //public Variables
    public float damageOnTouch;
    public float moveSpeed;
    public int enemyId;

    //System
    private Vector2 playerDirection;
    public float enemyInstanceID { get; set; }
    public event System.Action EnemyDeath;
    //Components
    private GameObject player;
    private Rigidbody2D enemyRigidbody;

    //RNG
    private float rng;

	// Use this for initialization
	public override void Start () {

        base.Start();
      
        player = GameObject.FindGameObjectWithTag("Player");
        healthGUI = transform.GetChild(0).GetComponent<GUI>();
        enemyRigidbody = GetComponent<Rigidbody2D>();        

        rng = Random.Range(1, 100);
        enemyInstanceID = Time.time + rng;

        //RNG to make different instances not so alike
        moveSpeed = moveSpeed * (Mathf.Lerp(0.6f, 1.0f, rng / 100f));

	}
    void FixedUpdate() {   
             
        MoveToPlayer();
    }

    void MoveToPlayer() {
        if (player) {
            playerDirection = (player.transform.position - transform.position).normalized;
            enemyRigidbody.MovePosition(enemyRigidbody.position + playerDirection * moveSpeed * Time.fixedDeltaTime);
        }
    }

    void OnCollisionEnter2D(Collision2D col) {
        GameObject c = col.gameObject;
        if (c.tag == "Player") {
            player.GetComponent<Player>().TakeDamage(damageOnTouch);
            player.GetComponent<Player>().UpdateHealth();
        }

    }

    public override void Die() {
        base.Die();

        if (EnemyDeath != null) {
            EnemyDeath();
        }

    }
}
