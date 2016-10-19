using UnityEngine;
using System.Collections;

public class FindNearestEnemy : MonoBehaviour {
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    GameObject FindClosestEnemy() {

        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 myPosition = transform.position;
        foreach (GameObject go in gos) {

            Vector3 diff = go.transform.position - myPosition;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance) {

                closest = go;
                distance = curDistance;
            }
        }

        return closest;
    }
}
