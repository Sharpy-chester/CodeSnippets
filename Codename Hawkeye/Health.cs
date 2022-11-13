using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public Animator animator;

    public int maxHealth = 5;
    int health = 5;
    [SerializeField] GameObject creature;

    void Awake()
    {
        creature = this.gameObject;
    }

    //Gets the current health of the creature this is attached to
    public int GetCurrentHealth()
    {
        return health;
    }

    //removes health from the creature this is attached to
    public void DamageCreature(int damage)
    {
        health -= damage;
        //Debug.Log(health);

        if (creature.CompareTag("Enemy"))
        {
            if (health > 0)
            {
                animator.SetTrigger("EnemyHurtTrigger");
            }
        }
    }

    //gets the max health of the creature this is attached to
    public int GetMaxHealth()
    {
        return maxHealth;
    }
}