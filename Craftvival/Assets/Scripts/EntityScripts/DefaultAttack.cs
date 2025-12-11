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
    private Transform playerTransform;
    public LayerMask playerLayer;

    public bool canAttack;
    public bool inRange;

    public void Awake()
    {
        playerLayer = LayerMask.GetMask("playerLayer");
        playerTransform = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void RangeCheck(float range)
    {
        Vector3 playerDirection = (playerTransform.position - transform.position).normalized;
        
        Physics.Raycast(transform.position, playerDirection, out RaycastHit hit, range, playerLayer);
        
        if (hit.collider != null)
        {
            inRange = true;
        }
        else
        {
            inRange = false;
        }
    }

public void AttackPlayer()
    {
        RangeCheck(entityStats.attackRange);

        Vector3 playerDirection = (playerTransform.position - transform.position).normalized;

        // This makes the agent stand still because it doesn't need to move
        agent.SetDestination(transform.position);
        transform.LookAt(playerTransform);
        
        Physics.Raycast(transform.position, playerDirection, out RaycastHit hitRange, entityStats.attackRange, playerLayer);

        if (hitRange.collider != null)
        {
            canAttack = true;
        }
        else
        {
            canAttack = false;
        }

        if (canAttack && inRange)
        {
            // Actual attack function
            Debug.Log($"{gameObject.name} attacked the player for {entityStats.attackDamage} damage!");
            canAttack = false;
            StartCoroutine(AttackCooldown());


            // Actual attack function
            Debug.Log(gameObject.name + " is attacking " + hitRange.collider.gameObject.name);
            hitRange.collider.gameObject.GetComponent<IDamagable>().TakeDamage(entityStats.attackDamage);
            canAttack = false;
            StartCoroutine(AttackCooldown());
        }
    }

    private IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    public void OnDrawGizmos()
    {
        Vector3 playerDirection = (playerTransform.position - transform.position).normalized;
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, playerDirection * entityStats.attackRange);
    }
}
