using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
    //Public variables
    public float secondsBetweenWaves;
    public Wave[] waves;
    

    //Components
    public GameObject enemy;

    //System
    int currentWaveNumber;
    float rng;
    int enemiesRemaining;

    Wave currentWave;

    void Start() {
        NextWave();
        
    }

    IEnumerator InstantiateEnemy() {

        enemiesRemaining = currentWave.numberToSpawn;

        for (int i = 0; i < currentWave.numberToSpawn; i++) {
            GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(-14, 14), Random.Range(-14, 14), 0),
                Quaternion.identity) as GameObject;

            newEnemy.GetComponent<Enemy>().EnemyDeath += OnEnemyDeath;
            yield return new WaitForSeconds(currentWave.secondsBetweenSpawn);
        }
        while (true) {
            if ((currentWaveNumber <= waves.Length) && (enemiesRemaining<=0)) {
                yield return new WaitForSeconds(secondsBetweenWaves);
                NextWave();
                break;
            }
            yield return null;
        }
    }
    void NextWave() {
        currentWaveNumber++;
        currentWave = waves[currentWaveNumber - 1];
        StartCoroutine("InstantiateEnemy");
        print(currentWaveNumber);
    }

    void OnEnemyDeath() {
        enemiesRemaining -= 1;

    }

    [System.Serializable]
    public class Wave {

        public int numberToSpawn;
        public float secondsBetweenSpawn;

    }
}
