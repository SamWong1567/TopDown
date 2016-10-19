using UnityEngine;
using System.Collections;

public class PuHeal : PowerUpBase{

    public float increaseAmount;
    public override void Start() {
        base.Start();
        base.PowerUpReachedPlayer += ApplyPowerUp;
    }

    void ApplyPowerUp() {
        Player playerScript = player.GetComponent<Player>();
        playerScript.Heal(increaseAmount);
        Destroy(gameObject);

    }
}
