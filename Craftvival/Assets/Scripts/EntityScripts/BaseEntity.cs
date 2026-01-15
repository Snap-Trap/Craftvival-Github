using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Creator: Luca
public class BaseEntity : MonoBehaviour, IDamagable
{
    public BaseEntitySO entityStats;

    // Basic variables
    public float health;
    public bool isDamaged;
    public List<GameObject> droppedItems;

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
        if(droppedItems != null && droppedItems.Count > 0)
        {
            foreach (GameObject item in droppedItems)
            {
                if (item != null)
                {
                    Instantiate(item, transform.position, Quaternion.identity);
                    Debug.Log(gameObject.name + " dropped " + item.name);
                }
            }
        }
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
