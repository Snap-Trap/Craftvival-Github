using System.Collections;
using UnityEngine;
using UnityEngine.AI;

// Creator: Luca
public class DefaultAttack : MonoBehaviour
{
    // Pakt de SO aan voor de stats
    public BaseEntitySO entityStats;

    // Component voor de NavMeshAgent
    public NavMeshAgent agent;

    // Basic variables
    public float attackCooldown = 1f;
    public Transform playerTransform;
    public LayerMask playerLayer;

    public bool canAttack;
    public bool inRange;

    private void Awake()
    {
        canAttack = true;
        playerLayer = LayerMask.GetMask("playerLayer");
        playerTransform = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    public void AttackCheck()
    {
        // Range check
        // Offset so the origin isn't inside the entity's own collider
        Vector3 offsetOrigin = transform.position + Vector3.forward * 1f;
        Vector3 playerDirection = (playerTransform.position - offsetOrigin).normalized;
        
        Physics.Raycast(offsetOrigin, playerDirection, out RaycastHit hit, entityStats.attackRange, playerLayer);
        
        if (hit.collider != null)
        {
            Debug.Log(gameObject.name + " is in range to hit " + hit.collider.gameObject.name);
            inRange = true;
        }
        else
        {
            Debug.Log(gameObject.name + " is NOT in range to hit the player.");
            inRange = false;
            return;
        }

        if (canAttack && inRange)
        {
            // This makes the agent stand still because it doesn't need to move
            agent.SetDestination(transform.position);
            transform.LookAt(playerTransform);

            // Actual attack function
            hit.collider.gameObject.GetComponent<IDamagable>().TakeDamage(entityStats.attackDamage);
            Debug.Log(gameObject.name + " is attacking " + hit.collider.gameObject.name);
            canAttack = false;
            Debug.Log(gameObject.name + " is now on attack cooldown for " + entityStats.attackCooldown + " seconds.");
            StartCoroutine(AttackCooldown());
        }
    }
    private IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(entityStats.attackCooldown);
        Debug.Log(gameObject.name + " attack cooldown has ended");
        canAttack = true;
    }

    private void OnDrawGizmos()
    {
        Vector3 offsetOrigin = transform.position + Vector3.up * 0.5f;
        Vector3 playerDirection = (playerTransform.position - offsetOrigin).normalized;

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(offsetOrigin, playerDirection * entityStats.attackRange);
    }
}
