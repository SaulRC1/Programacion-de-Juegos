using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenecticAlgorithm : MonoBehaviour
{
    public GameObject enemyPrefab;
    private List<GameObject> population = new List<GameObject>();
    public int populationSize = 10;
    private float mutationRate = 0.2f;
    public EnemySpawner spawner;

    void Start()
    {
        spawner = FindObjectOfType<EnemySpawner>();
        for (int i = 0; i < populationSize; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, new Vector3(Random.Range(-10, 10), 0.5f, Random.Range(-10, 10)), Quaternion.identity);
            enemy.GetComponent<EnemyCore>().InitializeGenes(Random.Range(0, 10)); // Inicializar con genes aleatorios
            enemy.SetActive(true);
            population.Add(enemy);
        }

    }

    void Update()
    {
        if (population.Count <= 4)
        {
            Debug.Log("Tamaño de la poblacion: " + population.Count);
            List<GameObject> newPopulation = new List<GameObject>();

            while (newPopulation.Count < populationSize)
            {
                GameObject parent1 = SelectParent();
                GameObject parent2 = SelectParent();

                Debug.Log("Genes del padre 1: " + parent1.GetComponent<EnemyCore>().damageGene + ", " + parent1.GetComponent<EnemyCore>().GetLifetime());
                Debug.Log("Genes del padre 2: " + parent2.GetComponent<EnemyCore>().damageGene + ", " + parent2.GetComponent<EnemyCore>().GetLifetime());

                GameObject offspring = CrossOver(parent1, parent2);

                Debug.Log("Genes del hijo antes de la mutación: " + offspring.GetComponent<EnemyCore>().damageGene + ", " + offspring.GetComponent<EnemyCore>().GetLifetime());

                Mutate(offspring);

                Debug.Log("Genes del hijo después de la mutación: " + offspring.GetComponent<EnemyCore>().damageGene + ", " + offspring.GetComponent<EnemyCore>().GetLifetime());

                newPopulation.Add(offspring);
            }

            population = newPopulation;
        }
    }

    GameObject SelectParent()
    {
        int fathersSize = 2; // Definir la cantidad de padres a seleccionar
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
        float fitness = genes.damageGene * genes.GetLifetime();
        Debug.Log("Fitness del enemigo: " + fitness);
        return fitness;
    }

    GameObject CrossOver(GameObject parent1, GameObject parent2)
    {
        GameObject offspring = spawner.SpawnEnemy();
        EnemyCore parent1Genes = parent1.GetComponent<EnemyCore>();
        EnemyCore parent2Genes = parent2.GetComponent<EnemyCore>();
        offspring.GetComponent<EnemyCore>().InitializeGenes((parent1Genes.damageGene + parent2Genes.damageGene) / 2);
        return offspring;
    }

    void Mutate(GameObject enemy)
    {
        if (Random.Range(0f, 2f) <= mutationRate)
        {
            EnemyCore genes = enemy.GetComponent<EnemyCore>();
            genes.damageGene = Random.Range(0, 10);
        }
    }

    public void RemoveFromPopulation(GameObject enemy)
    {
        population.Remove(enemy);
        spawner.EnemyDestroyed();
    }

}

