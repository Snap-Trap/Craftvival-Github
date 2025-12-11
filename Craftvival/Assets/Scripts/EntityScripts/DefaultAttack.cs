using System.Collections;
using UnityEngine;
using UnityEngine.AI;

// Creator: Luca
public class DefaultAttack : MonoBehaviour
{
    // Pakt de SO aan voor de stats
    public BaseEntitySO entityStats;

    // Component voor pathfinding
    public NavMeshAgent agent;

    // Basic variables
    public float attackCooldown = 1f;

    public bool canAttack;

    public Transform playerTransform;

    public void Awake()
    {
        playerTransform = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        canAttack = true;
    }

    public void AttackPlayer()
    {
        // Enemy will set in place once the attack range is plenty so it doesn't push into the player
        agent.SetDestination(transform.position);

        // Rotates towards the player
        transform.LookAt(playerTransform);
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("playerLayer"))
        {
            if (canAttack)
            {
                Debug.Log(gameObject.name + " is attacking " + collision.gameObject.name);
                collision.gameObject.GetComponent<IDamagable>().TakeDamage(entityStats.attackDamage);

                canAttack = false;
                StartCoroutine(AttackCooldown());
            }
        }
    }

    private IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
}
