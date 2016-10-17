using UnityEngine;
using System.Collections;

public class PuIncreaseProjectileCount : MonoBehaviour {
    public int increaseAmout;
    void Update() {

        transform.Rotate(Vector3.forward * 360 * Time.deltaTime);
    }
    void OnTriggerEnter2D(Collider2D col) {
        GameObject c = col.gameObject;

        if (col.tag == "Player") {
            ProjectileLauncher projLauncher = c.transform.GetChild(0).GetComponent<ProjectileLauncher>();
            projLauncher.numberOfProj += increaseAmout;
            Destroy(gameObject);

        }
    }
}
