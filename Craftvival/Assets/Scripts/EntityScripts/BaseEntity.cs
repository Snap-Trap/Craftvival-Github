using System.Collections;
using UnityEngine;
using UnityEngine.AI;

// Creator: Luca
public class BaseEntity : MonoBehaviour, IDamagable
{
    public BaseEntitySO entityStats;
    // References to the player needed for the agent component above :3
    public Transform player;
    public LayerMask playerLayer, groundLayer, objectLayer;

    // Basic variables
    public float health;
    public float speed;
    public float sprintSpeed;

    // Variables for other stuff
    public bool canSeePlayer;

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
