using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCore : MonoBehaviour
{
    public Animator animator;
    private EnemySpawner spawner;

    public int damageGene;
    public int lifeGene;

    void Start()
    {
        spawner = FindObjectOfType<EnemySpawner>();
    }

    public void takeDamage(int damage)
    {
        lifeGene -= damage;
        if (lifeGene <= 0 )
        {
            //Activar animacion de muerte
            animator.SetTrigger("die");
            GetComponent<Collider>().enabled = false;
            if (spawner != null)
            {
                spawner.EnemyDestroyed();
            }
        } 
        else
        {
            //Activar animacion de golpe
            animator.SetTrigger("damage");
        }
    }

    // Añade un método para inicializar los genes
    public void InitializeGenes(int damage, int life)
    {
        this.damageGene = damage;
        this.lifeGene = life;
    }
}
