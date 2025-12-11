using UnityEngine;

[CreateAssetMenu(fileName = "BaseEntitySO", menuName = "Scriptable Objects/BaseEntitySO")]
public class BaseEntitySO : ScriptableObject
{
    // Luca
    [Header("GeneralStats")]
    public string entityName;
    public float entityHealth;
    public float roamSpeed;
    public float sprintSpeed;
    public float visionRange;
    public float coneAngle;

    [Header("AttackStats")]
    public float attackDamage;
    public float attackRange;
    public float attackCooldown;

    [Header("DropStats")]
    public float dropChance;
    public GameObject dropItem;
}
