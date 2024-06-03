using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] public GameObject enemyPrefab; // El prefab del enemigo
    [SerializeField] public Transform[] spawnPoints; // Puntos donde pueden aparecer los enemigos
    [SerializeField] public float spawnInterval; // Tiempo entre cada spawn de enemigo
    private GenecticAlgorithm ga;

    private float timer;
    private int currentEnemyCount = 0;


    void Start()
    {
        timer = spawnInterval;
        ga = FindObjectOfType<GenecticAlgorithm>();
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            if (currentEnemyCount < ga.populationSize)
            {
                SpawnEnemy();
            }
            timer = spawnInterval;
        }
    }

    public GameObject SpawnEnemy()
    {
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPoints[spawnIndex].position, spawnPoints[spawnIndex].rotation);
       
        BoxCollider enemyBoxCollider = newEnemy.GetComponent<BoxCollider>();
        if (enemyBoxCollider != null)
        {
            newEnemy.SetActive(true);
            Debug.Log("Generando enemigos");
            enemyBoxCollider.enabled = true;
        }

        currentEnemyCount++;
        return newEnemy;
    }

    public void EnemyDestroyed()
    {
        currentEnemyCount--;
    }
}
