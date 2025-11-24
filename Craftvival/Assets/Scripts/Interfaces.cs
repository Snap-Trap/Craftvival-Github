public interface IDamageable
{
    void TakeDamage(float amount);
}

public interface IEntity
{
    void SpawnConditions();
}

public interface IPassive
{
    void Roam();

    void Flee();
}

public interface INeutral
{
    void Roam();

    void Retaliate();
}

public interface IAggressive
{
    void Roam();

    void Chase();

    void Attack();
}
