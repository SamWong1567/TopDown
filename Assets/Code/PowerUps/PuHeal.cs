using UnityEngine;
using System.Collections;

public class PuHeal : MonoBehaviour {

    public float increaseAmount;

    void Update() {

        transform.Rotate(Vector3.forward * 360 * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D col) {
        GameObject c = col.gameObject;
        if (col.CompareTag("Player")) {
            Player playerScript = c.GetComponent<Player>();
            playerScript.Heal(increaseAmount);
            Destroy(gameObject);

        }
    }
}
