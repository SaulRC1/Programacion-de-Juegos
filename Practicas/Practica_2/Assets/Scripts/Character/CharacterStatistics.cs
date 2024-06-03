using Assets.Scripts.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatistics
{
    private int health;
    private int maxHealth;

    private bool isDead;

    private List<ICharacterStatusListener> listeners;

    public CharacterStatistics(int health, int maxHealth, bool isDead)
    {
        this.maxHealth = maxHealth;
        Health = health;
        this.isDead = isDead;
    }

    public void CheckHealth()
    {
        //Debug.Log("Health: " + health);
        if(health <= 0)
        {
            health = 0;
            Die();
        }

        if(health >= maxHealth) 
        {
            health = maxHealth;
        }
    }

    public void Die()
    {
        isDead = true;
        NotifyCharacterDeath();
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        CheckHealth();
        NotifyCharacterDamaged();
    }

    public void Heal(int heal)
    {
        Health += heal;
        CheckHealth();
        NotifyCharacterHealed();
    }

    public void AddCharacterStatusListener(ICharacterStatusListener characterStatusListener)
    {
        if(listeners == null)
        {
            listeners = new List<ICharacterStatusListener>();
        }

        listeners.Add(characterStatusListener);
    }

    private void NotifyCharacterDeath()
    {
        foreach (ICharacterStatusListener listener in listeners)
        {
            listener.OnCharacterDeath();
        }
    }

    private void NotifyCharacterHealed()
    {
        foreach (ICharacterStatusListener listener in listeners)
        {
            listener.onCharacterHealed();
        }
    }

    private void NotifyCharacterDamaged()
    {
        foreach (ICharacterStatusListener listener in listeners)
        {
            listener.onCharacterDamaged();
        }
    }

    public int Health 
    {
        get { return health; }
        set
        {
            if (value >= maxHealth)
            {
                health = maxHealth;
            }
            else if (value < 0)
            {
                health = 0;
            }
            else 
            {
                health = value;
            }
        }
    }

    public int MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }

    public bool IsDead
    {
        get { return isDead; }
        set { isDead = value; }
    }
}
