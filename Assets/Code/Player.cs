using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : Entity {

    private GUI gui;

    void Start() {
        gui = GameObject.FindGameObjectWithTag("GUI").GetComponent<GUI>();
        UpdateHealth();

    }

    public void UpdateHealth() {       
        gui.SetHealth(currentHealth/maxHealth);
    }

}
