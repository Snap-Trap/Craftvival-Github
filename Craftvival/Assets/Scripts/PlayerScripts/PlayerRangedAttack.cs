using System.Collections;   
using UnityEngine;
using UnityEngine.InputSystem;

// Creator: Luca
public class PlayerRangedAttack : MonoBehaviour
{
    public InputAction rangedAttackAction;

    public float rangedPower;
    public float rangedDamage;

    private float rangedCooldown = 1f;
    private bool canAttack;

    private GameObject bulletPrefab;
    public Rigidbody rb;
    
    public void Update()
    {
        if (rangedAttackAction.triggered)
        {
            RangedAttack();
        }
    }

    public void RangedAttack()
    {
        if (canAttack)
        {
            Rigidbody TempBullet = bulletPrefab;
            Instantiate(TempBullet, transform.position, transform.rotation);
            
        }
    }

    private IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(rangedCooldown);
        Debug.Log(gameObject.name + " RANGED attack cooldown has ended");
        canAttack = true;
    }
}
