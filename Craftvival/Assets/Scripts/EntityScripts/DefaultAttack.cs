using System.Collections;
using UnityEditor;
using UnityEngine;

// Creator: Luca
public class DefaultAttack : MonoBehaviour
{
    // Pakt de SO aan voor de stats
    public BaseEntitySO entityStats;

    // Basic variables
    public float attackCooldown = 1f;

    public bool canAttack;

    public void Awake()
    {
        canAttack = true;
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
