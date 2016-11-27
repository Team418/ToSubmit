using UnityEngine;
using System.Collections;

public class WaveSpawner : MonoBehaviour
{
    public int[] Counter = new int[5];
    public Transform currentSpawnPoint;
    public float timeBetweenWaves;
    public GameObject enemyPrefab;
    public float countDoun;
    public float spawnCoolDown;
    public int waveIndex;

    void Start()
    {
        waveIndex = 1;
    }

    void ChoseSpawnPoint()
    {
        switch (waveIndex % 4)
        {
            case 0:
                currentSpawnPoint = GameObject.Find("SpawnPoint_1").GetComponent<Transform>();
                break;
            case 1:
                currentSpawnPoint = GameObject.Find("SpawnPoint_2").GetComponent<Transform>();
                break;
            case 2:
                currentSpawnPoint = GameObject.Find("SpawnPoint_3").GetComponent<Transform>();
                break;
            case 3:
                currentSpawnPoint = GameObject.Find("SpawnPoint_4").GetComponent<Transform>();
                break;
        }
    }

    void Update()
    {
        
        if (countDoun <= 0f)
        {
            ChoseSpawnPoint();
            StartCoroutine( SpawnWave() );
            countDoun = timeBetweenWaves;
        }

        countDoun -= Time.deltaTime;
        
    }

    IEnumerator SpawnWave()
    {
        for (int i = 1; i <= 3; i++)
        {
            SpawnServerEnemy();
            yield return new WaitForSeconds(spawnCoolDown);
        }
        waveIndex++;
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, currentSpawnPoint.transform.position, currentSpawnPoint.transform.rotation);
    }

    public void SpawnServerEnemy() {
        Counter[1] = 1;
        ChoseSpawnPoint();
        for (;;) {
            if (GameObject.Find("enemy_" + Counter[1]) != null) { //if it exists 
            } else {
                Object ServerSpawnedEnemy = Instantiate(enemyPrefab, currentSpawnPoint.transform.position, currentSpawnPoint.transform.rotation);
                ServerSpawnedEnemy.name = "enemy_" + Counter[1];
                break;
            }
            if (Counter[1] > 100) { //max enemies
                break;
            }
            Counter[1]++;
        }
    }
}
