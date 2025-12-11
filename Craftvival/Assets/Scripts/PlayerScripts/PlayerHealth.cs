using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour, IDamagable
{
    // Luca
    public float health = 100;
    public void TakeDamage (float damage)
    {
        health -= damage;
    }

    public void Die()
    {
        if (health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
