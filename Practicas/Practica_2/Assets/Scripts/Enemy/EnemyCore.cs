using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCore : MonoBehaviour
{
    public Animator animator;
    private EnemySpawner spawner;
    private GenecticAlgorithm ga;

    public int damageGene;
    private int life = 10;
    public float lifeTimeGene;
    private float DespawnTime = 30f;

    void Start()
    {
        spawner = FindObjectOfType<EnemySpawner>();
        ga = FindObjectOfType<GenecticAlgorithm>();
        lifeTimeGene = Time.time;

    }

    public void takeDamage(int damage)
    {
        if (life >= damage)
        {
            life -= damage;
        }
        if (life <= 0)
        {
            //Activar animacion de muerte
            animator.SetTrigger("die");
            GetComponent<Collider>().enabled = false;
            if (spawner != null)
            {
                spawner.EnemyDestroyed();
            }
            if (ga != null)
            {
                ga.RemoveFromPopulation(this.gameObject); // Notifica al algoritmo genético que este enemigo ha muerto
            }
            if (animator.transform.gameObject.name == "Enemy")
            {
                animator.transform.gameObject.SetActive(false);
            }
            else
            {
                Destroy(animator.transform.gameObject, DespawnTime);
            }
            PlayerScore.incrementScore(1);
        }
        else
        {
            //Activar animacion de golpe
            animator.SetTrigger("damage");
        }


    }

    // Añade un método para inicializar los genes
    public void InitializeGenes(int damage)
    {
        this.damageGene = damage;
    }

    // Añade un método para obtener el tiempo de vida del enemigo
    public float GetLifetime()
    {
        return Time.time - lifeTimeGene;
    }
}
