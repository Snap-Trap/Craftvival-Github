using UnityEngine;
using UnityEngine.AI;

public class DefaultChase : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform playerTransform;

    public void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        playerTransform = GameObject.Find("Player").transform;
    }

    public void Update()
    {
        ChasePlayer();
    }

    public void ChasePlayer()
    {
        agent.SetDestination(playerTransform.position);
    }
}
