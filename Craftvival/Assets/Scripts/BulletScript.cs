using UnityEngine;

// Creator: Luca
public class BulletScript : MonoBehaviour
{
    public float damage;

    private void OnCollisionEnter(Collision collision)
    {
        // Check if what we hit can take damage
        IDamagable damageable = collision.collider.GetComponent<IDamagable>();

        if (damageable != null)
        {
            damageable.TakeDamage(damage);
        }

        // Bullet is destroyed after hitting anything
        Destroy(gameObject);
    }
}
