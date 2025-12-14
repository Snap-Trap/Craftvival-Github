using System.Collections;
using UnityEngine;

// Creator: Luca
public class BaseEntity : MonoBehaviour, IDamagable
{
    public BaseEntitySO entityStats;

    // Basic variables
    public float health;

    public void Awake()
    {
        // Finds things it needs to find
        health = entityStats.entityHealth;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }

    public void Die()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
