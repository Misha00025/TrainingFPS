using UnityEngine.Events;

public interface IHealthState
{
    int Health { get; }
    int MaxHealth { get; }

    void AddListenerToHealthChange(UnityAction<IHealthState> action);
}

public interface IDamagable 
{
    void TakeDamage(int damage);
}

public interface IAmmo
{
    int Count { get; }
    int MaxCount { get; }
    AmmoType Type { get; }

}

public enum AmmoType
{
    None,
    Ammo_45
}