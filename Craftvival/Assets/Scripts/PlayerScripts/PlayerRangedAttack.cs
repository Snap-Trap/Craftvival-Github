using System.Collections;   
using UnityEngine;
using UnityEngine.InputSystem;

// Creator: Luca
public class PlayerRangedAttack : MonoBehaviour
{
    public InputAction rangedAttackAction;

    public float bulletSpeed;

    private float rangedCooldown = 1f;
    private bool canAttack = true;

    public GameObject bulletPrefab;
    private Transform firePoint;

    public void Awake()
    {
        firePoint = transform.Find("PlayerFirepoint");
    }

    public void Update()
    {
        if (rangedAttackAction.triggered)
        {
            Debug.Log("Ranged attack triggered");
            RangedAttack();
        }
    }

    public void RangedAttack()
    {
        if (canAttack)
        {
            GameObject TempBullet = Instantiate(bulletPrefab, transform.position, transform.rotation);

            Rigidbody rigidBullet = TempBullet.GetComponent<Rigidbody>();

            TempBullet.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed);

            StartCoroutine(AttackCooldown());
        }
    }

    private IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(rangedCooldown);
        Debug.Log(gameObject.name + " RANGED attack cooldown has ended");
        canAttack = true;
    }

    public void OnEnable()
    {
        rangedAttackAction.Enable();
    }

    public void OnDisable()
    {
        rangedAttackAction.Disable();
    }
}
