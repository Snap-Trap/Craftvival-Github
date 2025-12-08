using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using static UnityEngine.InputSystem.OnScreen.OnScreenStick;

public class BaseEntity : MonoBehaviour, IDamageable
{
    // Luca
    // Component for pathfinding
    public NavMeshAgent agent;

    // SO for entity stats
    public BaseEntitySO entityStats;

    // Private interface die elke entity gaat gebruiken
    private IEntity behaviour;
    private IDetect vision;
    private IRoam roaming;

    // References to the player needed for the agent component above :3
    public Transform player;
    public LayerMask playerLayer, groundLayer, objectLayer;

    // Basic variables
    public float health;
    public float speed;
    public float sprintSpeed;

    // Variables for other stuff
    public bool canSeePlayer;
    public float randX, randZ;
    public float roamRadius = 7f;
    public Vector3 currentPos;
    public Vector3 nextPos;

    public void Awake()
    {
        // Finds things it needs to find
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player").transform;
        playerLayer = LayerMask.GetMask("playerLayer");
        groundLayer = LayerMask.GetMask("groundLayer");
        objectLayer = LayerMask.GetMask("objectLayer");

        health = entityStats.entityHealth;
        speed = entityStats.roamSpeed;
        sprintSpeed = entityStats.sprintSpeed;

        behaviour = GetComponent<IEntity>();
        vision = GetComponent<IDetect>();
        roaming = GetComponent<IRoam>();

        if (behaviour != null)
        {
            behaviour.Initialize(entityStats);
        }
    }

    private void CreateEnemy()
    {
        gameObject.name = entityStats.entityName;

        health = entityStats.entityHealth;
    }

    public void Update()
    {
        if (vision != null)
        {
            vision.Detect();
        }

        if (roaming != null)
        {
            roaming.Roam();
        }
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

    public void Roam()
    {

    }

    public void Detect()
    {

    }
}
