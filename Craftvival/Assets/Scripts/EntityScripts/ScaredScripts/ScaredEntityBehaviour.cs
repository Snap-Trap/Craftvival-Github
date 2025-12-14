using System;
using UnityEngine;

public class ScaredEntityBehaviour : MonoBehaviour
{
    public BaseEntity baseEntity;
    public DefaultRoam Droam;
    public ScaredRun scaredRun;

    private void Awake()
    {
        baseEntity = GetComponent<BaseEntity>();
        Droam = GetComponent<DefaultRoam>();
        scaredRun = GetComponent<ScaredRun>();
    }

    private void Update()
    {
        if (baseEntity.isDamaged)
        {
            scaredRun.RunFromPlayer();
        }
        else
        {
            Droam.Roaming();
        }
    }
}
