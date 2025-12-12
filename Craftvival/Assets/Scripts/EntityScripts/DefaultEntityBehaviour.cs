using UnityEngine;

// Creator: Luca
public class DefaultEntityBehaviour : MonoBehaviour
{
    public DefaultRoam Droam;
    public DefaultVision Dvision;
    public DefaultChase Dchase;
    public DefaultAttack Dattack;

    public void Awake()
    {
        Dattack = GetComponent<DefaultAttack>();
        Dchase = GetComponent<DefaultChase>();
        Droam = GetComponent<DefaultRoam>();
        Dvision = GetComponent<DefaultVision>();
    }

    public void Update()
    {
        if (!Dvision.canSeePlayer && !Dattack.inRange)
        {
            Droam.Roaming();
        }
        if (Dvision.canSeePlayer && !Dattack.inRange)
        {
            Dchase.ChasePlayer();
            Dattack.AttackCheck();
        }
    }
}