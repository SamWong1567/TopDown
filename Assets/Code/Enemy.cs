using UnityEngine;
using System.Collections;

public enum State { Idle, Moving, Attacking, KnockedBack }

public class Enemy : Entity {

    //public Variables
    public float damageOnTouch;
    public float moveSpeed;
    public bool hasMods { get; set; }

    //System
    public Vector2 playerDirection { get; set; }
    public float enemyInstanceID { get; set; }
    public event System.Action EnemyDeath;
    public Projectile projectileScript { get; set; }
    public Enemy enemyScript { get; set; }
    public GameObject onTriggerEnterObject { get; set; }
    public Rigidbody2D thisRigidbody { get; set; }
    public float projectileId { get; set; }
    private Vector3 projectileDirectionOnImpact;
    private float projectileKnockBackStrength;
    public GameObject healGlobe;
    public ParticleSystem deathParticle;

    public State currentState;

    //Components
    public GameObject player { get; set; }
    public Rigidbody2D enemyRigidbody { get; set; }

    //RNG
    private float rng;


	public override void Start () {

        base.Start();

        currentState = State.Moving;
      
        player = GameObject.FindGameObjectWithTag("Player");
        healthGUI = transform.GetChild(0).GetComponent<GUI>();
        enemyRigidbody = GetComponent<Rigidbody2D>();        

        //Get a unique enemyInstanceId for every new instance of enemy
        rng = Random.Range(1, 100);
        enemyInstanceID = Time.time + rng;

        //RNG to make different instances not so alike
        moveSpeed = moveSpeed * (Mathf.Lerp(0.6f, 1.0f, rng / 100f));

	}

    void FixedUpdate() {   
             
       MoveToPlayer();
    }

    public virtual void MoveToPlayer() {
        if (player&&currentState==State.Moving) {
            playerDirection = (player.transform.position - transform.position).normalized;
            enemyRigidbody.MovePosition(enemyRigidbody.position + playerDirection * moveSpeed * Time.fixedDeltaTime);
        }
    }

    public virtual void OnCollisionEnter2D(Collision2D collisionObject) {
        
        GameObject c = collisionObject.gameObject;
        if (c.CompareTag("Player")) {
            player.GetComponent<Player>().TakeDamage(damageOnTouch);
            player.GetComponent<Player>().UpdateHealth();
        }

    }

    public virtual void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("Projectile")) {
            onTriggerEnterObject = col.gameObject;
            projectileScript = onTriggerEnterObject.transform.GetComponent<Projectile>();
            projectileDirectionOnImpact = onTriggerEnterObject.transform.TransformDirection(Vector2.up);
            projectileKnockBackStrength = projectileScript.knockBackStrength;
        }
    }
    public void KnockBackCaller() {

        StartCoroutine("KnockBack");
    }


    IEnumerator KnockBack() {
        if (currentState == State.Moving || currentState == State.Idle) {
            currentState = State.KnockedBack;

            float knockBackStartTime = Time.time;
            float knockBackDuration = 0.1f;

            while (knockBackStartTime + knockBackDuration > Time.time) {
                enemyRigidbody.MovePosition(transform.position + projectileDirectionOnImpact * projectileKnockBackStrength * Time.deltaTime);
                yield return null;
            }
            currentState = State.Moving;
        }
    }
    public override void Die() {
        base.Die();

        if (EnemyDeath != null) {
            EnemyDeath();
        }
        Destroy(Instantiate(deathParticle.gameObject, transform.position + projectileDirectionOnImpact * transform.localScale.x / 2, Quaternion.FromToRotation(Vector3.forward, projectileDirectionOnImpact)), deathParticle.duration);
        //LOOT

        int rng = UnityEngine.Random.Range(1, 100);

        if (10 > rng) {
            Instantiate(healGlobe, transform.position, Quaternion.identity);
        }

    }
}
