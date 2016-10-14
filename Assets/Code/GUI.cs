using UnityEngine;
using System.Collections;
using System;

public class GUI : MonoBehaviour {

    public Transform healthBar;

    public void SetHealth(float percentHealth) {
        healthBar.localScale = new Vector3(Math.Max(0,percentHealth), 1, 1);

    }
}
