using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

    public float secondsBetweenSpawn;
    public float numberToSpawn;

    private float rng;
    public GameObject enemy;

	// Use this for initialization
	void Start () {
        StartCoroutine("InstantiateEnemy");
	}
	
	// Update is called once per frame
	void Update () {
        

	}

    IEnumerator InstantiateEnemy() {
        for (int i = 0; i < numberToSpawn; i++) {
            GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(-14, 14), Random.Range(-14, 14), 0),
                Quaternion.identity) as GameObject;
            yield return new WaitForSeconds(secondsBetweenSpawn);
        }
    }

        
    
}
