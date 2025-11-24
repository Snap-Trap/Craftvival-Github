using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class BaseEntity : MonoBehaviour, IDamageable, IEntity
{
    // Component for pathfinding
    public NavMeshAgent agent;

    // References to the player needed for the agent component above :3
    public Transform player;
    public LayerMask playerLayer, groundLayer;

    public void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player").transform;
        playerLayer = LayerMask.GetMask("playerLayer");
        groundLayer = LayerMask.GetMask("groundLayer");
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }

    public void Die()
    {
        if (health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
