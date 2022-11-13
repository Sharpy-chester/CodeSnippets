using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField] float initialGrenadeLifetime;
    [SerializeField] float grenadeLifetime;
    [SerializeField] float blastRadius;
    [SerializeField] float blastForce;
    

    void Awake()
    {
        grenadeLifetime = 0;
    }
    
    void Update()
    {
        grenadeLifetime += Time.deltaTime;
        if(grenadeLifetime > initialGrenadeLifetime)
        {
            Collider2D[] inBlastZone = Physics2D.OverlapCircleAll(transform.position, blastRadius);
            foreach (Collider2D objectInZone in inBlastZone)
            {
                Vector2 dir = objectInZone.transform.position - transform.position;
                if (objectInZone.TryGetComponent(out Rigidbody2D objectRb) && !objectInZone.CompareTag("Player"))
                {
                    objectRb.AddForce(dir * blastForce, ForceMode2D.Impulse);
                }
                if (objectInZone.gameObject.CompareTag("Enemy"))
                {
                    objectInZone.gameObject.GetComponent<Health>().currentHealth = 0;
                }
            }
            Destroy(gameObject);
        }
    }
}
