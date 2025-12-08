using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class EntityRoam : MonoBehaviour, IRoam, IEntity
{
    // Luca

    // Inherit van BaseEntity om toegang te krijgen tot vrijwel alles
    private BaseEntity parent;
    private BaseEntitySO entityStats;

    public float roamRadius = 7f;

    public void Initialize(BaseEntitySO entityData)
    {
        entityStats = entityData;
    }

    void Awake()
    {
        parent = GetComponent<BaseEntity>();
    }
    public void Roam()
    {
        if (parent == null)
        {
            return;
        }

        Vector3 current = parent.transform.position;
        float randX = Random.Range(-roamRadius, roamRadius);
        float randZ = Random.Range(-roamRadius, roamRadius);
        Vector3 nextPos = new Vector3(current.x + randX, current.y, current.z + randZ);

        parent.agent.SetDestination(nextPos);
    }
}
