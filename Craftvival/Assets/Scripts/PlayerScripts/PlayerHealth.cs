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
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadSceneAsync(2); // Loads the scene of the Death Screen
    }
}
