using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : Entity {
    //Components
    private GUI gui;

    void Start() {
        //Get Components
        gui = GameObject.FindGameObjectWithTag("GUI").GetComponent<GUI>();
        UpdateHealth();

    }

    //Update health displayed on GUI
    public void UpdateHealth() {       
        gui.SetHealth(currentHealth/maxHealth);
    }

}
