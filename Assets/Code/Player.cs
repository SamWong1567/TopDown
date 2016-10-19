using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : Entity {
    //Components
    public event System.Action OnPlayerDeath;

    public override void Start() {
        base.Start();
        //Get Components
        healthGUI = GameObject.FindGameObjectWithTag("GUI").GetComponent<GUI>();
        UpdateHealth();

    }

    public override void Die() {
        base.Die();
        OnPlayerDeath();
    }
}
