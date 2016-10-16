using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
    //Public variables
    public float secondsBetweenWaves;
    public Wave[] waves;
    

    //Components
    public GameObject[] enemyType;


    //System
    int currentWaveNumber;
    float rng;
    int enemiesRemaining;

    Wave currentWave;

    void Start() {
        NextWave();
        
    }
    int[] ShuffleArray(int[] array){

        for (int i = array.Length; i > 0; i--) {
            int j = Random.Range(0,i);
            int k = array[j];
            array[j] = array[i - 1];
            array[i - 1] = k;
        }
        return array;

    }
    IEnumerator InstantiateEnemy() {

        foreach (int i in currentWave.numberToSpawn) {
            enemiesRemaining += i;
        }

        int enemiesToSpawn = enemiesRemaining;
        int[] currentEnemySpawnCount = new int[enemiesRemaining];

        int n = 0;

        for (int i = 0; i < currentWave.numberToSpawn.Length; i++) {
            print("up");
            for (int x = 0; x < currentWave.numberToSpawn[i]; x++) {
                print(n);
                currentEnemySpawnCount[n] = i;
                n++;
                
            }
        }

        currentEnemySpawnCount = ShuffleArray(currentEnemySpawnCount);
        

        for (int i = 0; i < enemiesToSpawn; i++) {

            int k = currentEnemySpawnCount[i];
            GameObject newEnemy = Instantiate(enemyType[k], new Vector3(Random.Range(-14, 14), Random.Range(-14, 14), 0),
                Quaternion.identity) as GameObject;

            newEnemy.GetComponent<Enemy>().EnemyDeath += OnEnemyDeath;
            yield return new WaitForSeconds(currentWave.secondsBetweenSpawn);
        }


        while (true) {
            if ((currentWaveNumber < waves.Length) && (enemiesRemaining<=0)) {
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
        print("Current Wave: " +currentWaveNumber);
    }

    void OnEnemyDeath() {
        enemiesRemaining -= 1;

    }

    [System.Serializable]
    public class Wave {

        public int[] numberToSpawn;
        public float secondsBetweenSpawn;

    }
}
