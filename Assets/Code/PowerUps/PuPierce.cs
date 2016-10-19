﻿using UnityEngine;
using System.Collections;

public class PuPierce : PowerUpBase {
    [Range (0,1)]
    public float increaseAmount;
    public override void Start() {
        base.Start();
        base.PowerUpReachedPlayer += ApplyPowerUp;
    }

    void Update() {

        transform.Rotate(Vector3.forward * 360 * Time.deltaTime);
    }

    void ApplyPowerUp() {
        ProjectileLauncher projLauncher = player.transform.GetChild(0).GetComponent<ProjectileLauncher>();
        projLauncher.UpdatePierce(increaseAmount);
        Destroy(gameObject);

    }
}
