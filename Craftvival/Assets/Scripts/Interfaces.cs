public interface IDamageable
{
    void TakeDamage(float amount);
}

public interface IEntity
{
    void Initialize(BaseEntitySO stats);
}

public interface IRoam
{
    void Roam();
}

public interface IDetect
{
    void Detect();
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
