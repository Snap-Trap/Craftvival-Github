using UnityEngine;

// Creator: Luca
public class DefaultEntityBehaviour : MonoBehaviour
{
    // Reference to other scripts for behaviour
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
        // State machine
        // Holy fuck these few lines were so annoying to configure
        if (!Dvision.canSeePlayer && !Dattack.inRange)
        {
            Droam.Roaming();
        }
        if (Dvision.canSeePlayer && !Dattack.inRange)
        {
            Dchase.ChasePlayer();
        }
        // God fucking dammit do NOT add another condition to the line below I swear to god I will actually kill you I'm not even joking
        if (Dattack.canAttack)
        {
            Dattack.AttackCheck();
        }
    }
}