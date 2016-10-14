using UnityEngine;
using System.Collections;

public class MockEnemyScript : MonoBehaviour {

    private Player player;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider c) {
        if (c.tag == "Player") {
            player.TakeDamage(1);
            player.UpdateHealth();
        }

    }
}
