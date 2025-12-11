using UnityEngine;

// Creator: Luca
public class DefaultEntityBehaviour : MonoBehaviour
{
    public DefaultAttack Dattack;
    public DefaultChase Dchase;
    public DefaultRoam Droam;
    public DefaultVision Dvision;

    public void Awake()
    {
        Dattack = GetComponent<DefaultAttack>();
        Dchase = GetComponent<DefaultChase>();
        Droam = GetComponent<DefaultRoam>();
        Dvision = GetComponent<DefaultVision>();
    }

    public void Update()
    {
        if (!Dvision.canSeePlayer)
        {
            Droam.Roaming();
        }
        if (Dvision.canSeePlayer)
        {
            Dchase.ChasePlayer();
        }
        if (Dvision.canSeePlayer && Dattack.inRange && Dattack.canAttack)
        {
            Dattack.AttackPlayer();
        }
    }
}