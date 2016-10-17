﻿using UnityEngine;
using System.Collections;

public class PuPierce : MonoBehaviour {
    [Range (0,1)]
    public float increaseAmount;

    void Update() {

        transform.Rotate(Vector3.forward * 360 * Time.deltaTime);
    }
    void OnTriggerEnter2D(Collider2D col) {
        GameObject c = col.gameObject;

        if (col.tag == "Player") {
            ProjectileLauncher projLauncher = c.transform.GetChild(0).GetComponent<ProjectileLauncher>();
            projLauncher.UpdatePierce(increaseAmount);
            Destroy(gameObject);

        }
    }
}
