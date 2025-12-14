using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

// Creator: Luca
public class PlayerMeleeAttack : MonoBehaviour
{
    public InputAction meleeAttackAction;

    public float playerAttackRange;

    private LayerMask enemyLayer;

    private float attackCooldown = 1f;
    public float playerDamage = 30f;
    private bool canAttack = true;

    public void Awake()
    {
        enemyLayer = LayerMask.GetMask("enemyLayer");
    }

    public void Update()
    {
        if (meleeAttackAction.triggered)
        {
            Debug.Log("Melee attack triggered");
            PerformMeleeAttack();
        }
    }

    private void PerformMeleeAttack()
    {
        if (canAttack)
        {
            Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, playerAttackRange, enemyLayer);
            if (hit.collider != null)
            {
                Debug.Log(gameObject.name + " hit " + hit.collider.gameObject.name + " with a melee attack.");
                hit.collider.gameObject.GetComponent<IDamagable>().TakeDamage(playerDamage);

                canAttack = false;
                StartCoroutine(AttackCooldown());
            }
            else
            {
                Debug.Log("Missed attack");
            }
        }
    }
    private IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        Debug.Log(gameObject.name + " attack cooldown has ended");
        canAttack = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawRay(transform.position, transform.forward * playerAttackRange);
    }

    public void OnEnable()
    {
        meleeAttackAction.Enable();
    }

    public void OnDisable()
    {
        meleeAttackAction.Disable();
    }
}
