public interface IDamagable
{
    void TakeDamage(float amount);
}

public interface IEntity
{
    void Initialize(BaseEntitySO stats);
}

public interface IAggressive
{
    void Chase();
    void Attack();
}

public interface IPassive
{
    void Flee();
}
