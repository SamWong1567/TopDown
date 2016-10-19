using UnityEngine;
using System.Collections;

public class PuIncreaseProjectileCount : PowerUpBase {
    public int increaseAmout;

    public override void Start() {
        base.Start();
        base.PowerUpReachedPlayer += ApplyPowerUp;
    }


    void Update() {

        transform.Rotate(Vector3.forward * 360 * Time.deltaTime);
    }

    void ApplyPowerUp() {
        ProjectileLauncher projLauncher = player.transform.GetChild(0).GetComponent<ProjectileLauncher>();
        projLauncher.numberOfProj += increaseAmout;
        Destroy(gameObject);

    }
}
