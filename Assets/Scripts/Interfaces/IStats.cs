using UnityEngine.Events;

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