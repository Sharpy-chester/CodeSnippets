using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] float waveTime = 60f;
    [SerializeField] float spawnDistFromPlayer = 5f;
    [SerializeField] GameObject winScreen;
    float timeBetweenSpawns;
    float timeSinceLastSpawn = 0f;
    public Wave[] waves;
    int currentWave = 0;
    int amountOfWaves;
    GameObject player;

    [Serializable]
    public struct Wave
    {
        public Enemy[] EnemiesToSpawn;
    }

    [Serializable]
    public struct Enemy
    {
        public GameObject enemy;
        public int enemiesInWave;
    }

    private void Awake()
    {
        timeBetweenSpawns = waveTime / EnemiesInWave();
        player = GameObject.FindGameObjectWithTag("Player");
        amountOfWaves = waves.Length;
    }

    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn > timeBetweenSpawns)
        {
            timeSinceLastSpawn = 0f;
            int rand = 0;
            do
            {
                rand = UnityEngine.Random.Range(0, waves[currentWave].EnemiesToSpawn.Length);
            } while (waves[currentWave].EnemiesToSpawn[rand].enemiesInWave <= 0); 
            
            if (waves[currentWave].EnemiesToSpawn[rand].enemiesInWave > 0)
            {
                SpawnEnemy(waves[currentWave].EnemiesToSpawn[rand].enemy);
                waves[currentWave].EnemiesToSpawn[rand].enemiesInWave--;
            }

            if (EnemiesInWave() <= 0)
            {
                NextWave();
            }
        }
    }

    void NextWave()
    {
        ++currentWave;
        if(currentWave == amountOfWaves)
        {
            Time.timeScale = 0;
            winScreen.SetActive(true);
            return;
        }
        timeBetweenSpawns = waveTime / EnemiesInWave();
    }

    int EnemiesInWave()
    {
        int count = 0;
        foreach(Enemy enemy in waves[currentWave].EnemiesToSpawn)
        {
            count += enemy.enemiesInWave;
        }
        return count;
    }

    void SpawnEnemy(GameObject Enemy)
    {
        GameObject spawnedEnemy = Instantiate(Enemy);
        spawnedEnemy.transform.position = RandOnCircle();
    }

    Vector2 RandOnCircle()
    {
        Vector2 randomPointOnCircle = UnityEngine.Random.insideUnitCircle.normalized * spawnDistFromPlayer;
        randomPointOnCircle = new Vector2(randomPointOnCircle.x + player.transform.position.x, randomPointOnCircle.y + player.transform.position.y);
        return randomPointOnCircle;
    }
}
