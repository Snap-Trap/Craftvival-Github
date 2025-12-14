using UnityEngine;
using UnityEngine.AI;

public class ScaredRun : MonoBehaviour
{
    // Nodig voor de TakeDamage() functie
    public BaseEntity baseEntity;
    
    // Pakt de SO aan voor de stats
    public BaseEntitySO entityStats;

    // Component voor de NavMeshAgent
    public NavMeshAgent agent;

    // Basic variables
    private Transform playerTransform;
    
    // NavMesh variables
    private Vector3 walkPoint;

    private void Awake()
    {
        baseEntity = GetComponent<BaseEntity>();
        agent = GetComponent<NavMeshAgent>();
        playerTransform = GameObject.Find("Player").transform;
    }

    public void Update()
    {
        if (baseEntity.isDamaged)
        {
            RunFromPlayer();
        }
    }
    
    public void RunFromPlayer()
    {
        Vector3 playerDirection = playerTransform.position - transform.position;
        Vector3 oppositeDirection = transform.position - playerDirection;
        Debug.Log(gameObject.name + " is running from player");
        // Sets the agent speed needs to be in the roaming function so it resets when switching states
        agent.speed = entityStats.sprintSpeed;

        agent.SetDestination(oppositeDirection);
    }
}