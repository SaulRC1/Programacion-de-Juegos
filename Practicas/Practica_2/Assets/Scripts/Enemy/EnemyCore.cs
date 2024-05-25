using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCore : MonoBehaviour
{
    public int HP = 100;
    public Animator animator;
    private EnemySpawner spawner;

    void Start()
    {
        spawner = FindObjectOfType<EnemySpawner>();
    }

    public void takeDamage(int damage)
    {
        HP -= damage;
        if (HP <= 0 )
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
}
