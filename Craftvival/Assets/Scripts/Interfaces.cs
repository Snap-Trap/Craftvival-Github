public interface IDamageable
{
    void TakeDamage(float amount);
}

public interface IEntity
{
    void Roam();

    void Detect();
}

public interface IPassive
{
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
