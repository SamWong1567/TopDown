using UnityEngine;
using System.Collections;
using System;

public class PowerUpBase : MonoBehaviour {

    public GameObject player;
    CircleCollider2D thisCollider;
    public event Action PowerUpReachedPlayer;

    public virtual void Start() {

        thisCollider = GetComponent<CircleCollider2D>();
        thisCollider.radius = 4;
        player = GameObject.FindGameObjectWithTag("Player");
    }


    public virtual void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("Player")){
            StartCoroutine("MoveToPlayer");
        }

    }


    IEnumerator MoveToPlayer() {
        Vector2 playerDirection = Vector2.one;

        while (playerDirection.magnitude > 0.5f) {
            Vector2 playerLocation = player.transform.position;
            playerDirection = (playerLocation - (Vector2)transform.position);
            transform.Translate(playerDirection.normalized * 10 * Time.deltaTime, Space.World);

            yield return null;
        }

        PowerUpReachedPlayer();

    }

}
