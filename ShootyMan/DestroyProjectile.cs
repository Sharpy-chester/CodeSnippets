using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyProjectile : MonoBehaviour
{
    [SerializeField] int bounces;
    Rigidbody2D rb;
    public int damage;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (bounces > 0)
        {
            bounces--;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}