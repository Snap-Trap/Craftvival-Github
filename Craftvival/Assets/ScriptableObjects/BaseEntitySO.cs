using UnityEngine;

[CreateAssetMenu(fileName = "BaseEntitySO", menuName = "Scriptable Objects/BaseEntitySO")]
public class BaseEntitySO : ScriptableObject
{
    public float health;
    public float roamSpeed;
    public float visionRange;

    [Header("title")]
    public float attackDamage;
}
