using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // El prefab del enemigo
    public Transform[] spawnPoints; // Puntos donde pueden aparecer los enemigos
    public float spawnInterval = 5f; // Tiempo entre cada spawn de enemigo
    public int maxEnemies = 20; // Límite máximo de enemigos

    private float timer;
    private int currentEnemyCount = 0;

    void Start()
    {
        timer = spawnInterval;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            if (currentEnemyCount < maxEnemies)
            {
                SpawnEnemy();
            }
            timer = spawnInterval;
        }
    }

    public GameObject SpawnEnemy()
    {
        //int spawnIndex = Random.Range(0, spawnPoints.Length);
        //Instantiate(enemyPrefab, spawnPoints[spawnIndex].position, spawnPoints[spawnIndex].rotation);
        //currentEnemyCount++;
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPoints[spawnIndex].position, spawnPoints[spawnIndex].rotation);
        currentEnemyCount++;
        return newEnemy;
    }

    public void EnemyDestroyed()
    {
        currentEnemyCount--;
    }
}
