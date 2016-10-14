using UnityEngine;
using System.Collections;

public class GUI : MonoBehaviour {

    public Transform healthBar;

    public void SetHealth(float percentHealth) {
        healthBar.localScale = new Vector3(percentHealth, 1, 1);

    }
}
