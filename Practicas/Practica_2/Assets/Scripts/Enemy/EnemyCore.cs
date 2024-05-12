using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCore : MonoBehaviour
{
    public int HP = 100;
    public Animator animator;

    public void takeDamage(int damage)
    {
        HP -= damage;
        if (HP <= 0 )
        {
            //Activar animacion de muerte
            animator.SetTrigger("die");
            GetComponent<Collider>().enabled = false;
        } 
        else
        {
            //Activar animacion de golpe
            animator.SetTrigger("damage");
        }
    }
}
