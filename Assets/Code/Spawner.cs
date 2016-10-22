using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
    //Public variables
    public float secondsBetweenWaves;
    public Wave[] waves;
    //Components
    public GameObject[] enemyType;
    public GameObject spawnWarning;
    
    //System
    int currentWaveNumber;
    int enemiesRemaining;
    int enemiesToSpawn;
    public int[] numberToSpawn { get; set; }
    int[] currentWaveSpawnList;
    Wave currentWave;

    void Start() {
        numberToSpawn = new int[enemyType.Length];

        if (waves.Length != 0) {
            NextWave();
        }
    }
    void NextWave() {

        //Update current wave number and set currentWave to the correct Wave
        currentWaveNumber++;

        currentWave = waves[currentWaveNumber - 1];

        numberToSpawn[0] = currentWave.numberOfNormal;
        numberToSpawn[1] = currentWave.numberOfReflect;
        numberToSpawn[2] = currentWave.numberOfCharger;

        StartCoroutine("QueueSpawning");
        print("Current Wave: " + currentWaveNumber);
    }
    IEnumerator QueueSpawning() {

        CountEnemiesToBeSpawned();
        PopulateCurrentWaveSpawnList();

        //Instantiate enemies by the currentWaveSpawnList order
        for (int i = 0; i < enemiesToSpawn; i++) {

            StartCoroutine("InstantiateEnemy", i);
            yield return new WaitForSeconds(currentWave.secondsBetweenSpawn);
        }
        print("All enemies in wave spawned");       
    }

    IEnumerator InstantiateEnemy(int i) {

        int k = currentWaveSpawnList[i];
        Vector3 spawnLocation = new Vector3(Random.Range(-14, 14), Random.Range(-14, 14), 0);
        GameObject newSpawnWarning = Instantiate(spawnWarning, spawnLocation, Quaternion.identity) as GameObject;
        float spawnWarningDuration = newSpawnWarning.transform.GetComponent<FadeInSprite>().duration;

        Destroy(newSpawnWarning, spawnWarningDuration);
        yield return new WaitForSeconds(spawnWarningDuration);
        GameObject newEnemy = Instantiate(enemyType[k], spawnLocation, Quaternion.identity) as GameObject;

        //Subscribe the OnEnemyDeath method to the EnemyDeath event in every instance of enemy
        newEnemy.GetComponent<Enemy>().EnemyDeath += OnEnemyDeath;

    }
    IEnumerator CallNextWave() {

        yield return new WaitForSeconds(secondsBetweenWaves);
        NextWave();
    }
    public void PopulateCurrentWaveSpawnList() {

        //set the currentWaveSpawnList size to enemies to be spawned
        currentWaveSpawnList = new int[enemiesToSpawn];

        int n = 0;

        for (int i = 0; i < numberToSpawn.Length; i++) {
            for (int x = 0; x < numberToSpawn[i]; x++) {
                currentWaveSpawnList[n] = i;
                n++;
            }
        }

        //Shuffle the currentWaveList
        currentWaveSpawnList = ShuffleArray(currentWaveSpawnList);
    }
    public void CountEnemiesToBeSpawned() {
        //count the total number of enemies to be spawned
        foreach (int i in numberToSpawn) {
            enemiesToSpawn += i;
        }

        //set enemies remaining equal to enemies to be spawned
        enemiesRemaining = enemiesToSpawn;

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

    void OnEnemyDeath() {
        enemiesRemaining -= 1;
        if (enemiesRemaining == 0) {
            if (currentWaveNumber < waves.Length) {
                StartCoroutine("CallNextWave");
            }
            else {
                print("game end");
            }
        }
  
    }
    [System.Serializable]
    public class Wave {

        public int numberOfNormal;
        public int numberOfReflect;
        public int numberOfCharger;
        public float secondsBetweenSpawn; 
    }
}
