using UnityEngine;
using UnityEngine.AI;

// Creator: Luca
public class DefaultRoam : MonoBehaviour
{
    // Pakt de SO aan voor de stats
    public BaseEntitySO entityStats;

    // Component voor de NavMeshAgent
    public NavMeshAgent agent;

    // Voor de roaming ai
    public Vector3 walkPoint;

    private bool walkPointSet;

    public float walkPointRange;

    public LayerMask whatIsGround;

    public void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = entityStats.roamSpeed;

        whatIsGround = LayerMask.GetMask("groundLayer");
    }

    public void Update()
    {
        Roaming();
    }

    private void Roaming()
    {
        // If the enemy doesn't have a walk point set, it will search for one
        if (!walkPointSet) SearchWalkPoint();

        // NavMesh W moment
        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        // This is how much space or distance or whatever there is between the enemy and the walk point
        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        // If the enemy gets close to the walk point it will say it doesn't have a walk point so it can search for a new one
        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }
    private void SearchWalkPoint()
    {
        // Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        // If the walk point is on the ground
        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }
}
