using UnityEngine;
using UnityEngine.AI;

// Creator: Luca
public class DefaultChase : MonoBehaviour
{
    // Pakt de SO aan voor de stats
    public BaseEntitySO entityStats;

    // Component voor de NavMeshAgent
    public NavMeshAgent agent;

    private Transform playerTransform;

    public void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        playerTransform = GameObject.Find("Player").transform;
    }

    public void ChasePlayer()
    {
        // Sets the agent speed needs to be in the roaming function so it resets when switching states
        agent.speed = entityStats.sprintSpeed;

        agent.SetDestination(playerTransform.position);
    }
}
