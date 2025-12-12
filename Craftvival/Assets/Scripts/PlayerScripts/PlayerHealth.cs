using UnityEngine;
using UnityEngine.SceneManagement;

// Creator: Luca
// Why the fuck wants copilot to write down "Dylan" as the creator???
public class PlayerHealth : MonoBehaviour, IDamagable
{
    public float health = 100;
    public void TakeDamage (float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
