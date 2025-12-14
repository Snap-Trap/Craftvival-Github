using System.Collections;
using UnityEngine;

// Creator: Luca
public class BaseEntity : MonoBehaviour, IDamagable
{
    public BaseEntitySO entityStats;

    // Basic variables
    public float health;
    public bool isDamaged;

    public void Awake()
    {
        // Finds things it needs to find
        health = entityStats.entityHealth;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        StartCoroutine(DamagedTimer());

        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
    
    private IEnumerator DamagedTimer()
    {
        isDamaged = true;
        Debug.Log(gameObject.name + " has been damaged");
        yield return new WaitForSeconds(15);
        isDamaged = false;
        Debug.Log(gameObject.name + " is no longer damaged");
    }
}
