using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flammable : MonoBehaviour
{
    [SerializeField] bool onFire;
    [SerializeField] float timeLeftOnFire;
    [SerializeField] float putOutTime;
    [SerializeField] int damagePerSecond;
    [SerializeField] bool destroyWhenHealthDepleted;
    [SerializeField] bool turnRed;
    [SerializeField] float reddeningTime;
    Health health;
    float damageTimer;

    void Awake()
    {
        health = GetComponent<Health>();
        damageTimer = 0;
    }

    void Update()
    {
        if (onFire)
        {
            timeLeftOnFire -= Time.deltaTime;
            if (timeLeftOnFire <= 0)
            {
                onFire = false;
            }
            //If we want damage every frame i could change health to a float instead but i think taking damage each second works fine personally
            damageTimer += Time.deltaTime;
            if (damageTimer >= 1)
            {
                health.DamageCreature(damagePerSecond);
                damageTimer = 0;
            }
            if (destroyWhenHealthDepleted && health.GetCurrentHealth() <= 0)
            {
                Destroy(gameObject);
            }
            if (turnRed)
            {
                foreach (SpriteRenderer sr in transform.GetComponentsInChildren<SpriteRenderer>())
                {
                    sr.color = Color.Lerp(sr.color, Color.red, Time.deltaTime / health.GetCurrentHealth());
                }
            }
        }
    }

    public void SetAlight()
    {
        timeLeftOnFire = putOutTime;
        onFire = true;
        
    }
    
    //dont know if we'll ever use this, but if ya wanna set something alight for a specific amount of time u can call this overloaded function
    //Maybe could use for the vines
    public void SetAlight(float timeAlight)
    {
        onFire = true;
        timeLeftOnFire = timeAlight;
    }
   

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent(out FireArrow fireArrow))
        {

            SetAlight();

        }
    }
}
