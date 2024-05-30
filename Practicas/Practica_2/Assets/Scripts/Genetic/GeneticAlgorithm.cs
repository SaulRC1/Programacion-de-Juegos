using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenecticAlgorithm: MonoBehaviour
{
    public GameObject enemyPrefab;
    private List<GameObject> population = new List<GameObject>();
    private int populationSize = 20;
    private float mutationRate = 0.01f;
    public EnemySpawner spawner;

    void Start()
    {
        spawner = FindObjectOfType<EnemySpawner>();
        for (int i = 0; i < populationSize; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, new Vector3(Random.Range(-10, 10), 0.5f, Random.Range(-10, 10)), Quaternion.identity);
            enemy.GetComponent<EnemyCore>().InitializeGenes(Random.Range(0, 10), Random.Range(0, 10)); // Inicializar con genes aleatorios
            population.Add(enemy);
        }
    }

    void Update()
    {
        if (population.Count == 0)
        {
            List<GameObject> newPopulation = new List<GameObject>();

            while (newPopulation.Count < populationSize)
            {
                GameObject parent1 = SelectParent();
                GameObject parent2 = SelectParent();
                GameObject offspring = CrossOver(parent1, parent2);
                Mutate(offspring);
                newPopulation.Add(offspring);
            }

            population = newPopulation;
        }
    }

    GameObject SelectParent()
    {
        int fathersSize = 5; // Definir la cantidad de padres a seleccionar
        List<GameObject> tournament = new List<GameObject>();

        // Seleccionar individuos aleatorios de la población
        for (int i = 0; i < fathersSize; i++)
        {
            tournament.Add(population[Random.Range(0, population.Count)]);
        }

        // Ordena a los individuos por aptitud 
        tournament.Sort((x, y) => CalculateFitness(y).CompareTo(CalculateFitness(x)));

        // Devuelve el individuo más apto
        return tournament[0];
    }

    float CalculateFitness(GameObject enemy)
    {
        // Aquí se define cómo calcular la aptitud de un individuo.
        // En este caso, daño hecho al jugador / tiempo de vida
        EnemyCore genes = enemy.GetComponent<EnemyCore>();
        return genes.damageGene / genes.lifeGene;
    }

    GameObject CrossOver(GameObject parent1, GameObject parent2)
    {
        GameObject offspring = spawner.SpawnEnemy();
        EnemyCore parent1Genes = parent1.GetComponent<EnemyCore>();
        EnemyCore parent2Genes = parent2.GetComponent<EnemyCore>();
        offspring.GetComponent<EnemyCore>().InitializeGenes((parent1Genes.damageGene + parent2Genes.damageGene) / 2, (parent1Genes.lifeGene + parent2Genes.lifeGene) / 2);
        return offspring;
    }

    void Mutate(GameObject enemy)
    {
        if (Random.Range(0f, 1f) <= mutationRate)
        {
            EnemyCore genes = enemy.GetComponent<EnemyCore>();
            genes.damageGene = Random.Range(0, 10);
            genes.lifeGene = Random.Range(0, 10);
        }
    }
}
