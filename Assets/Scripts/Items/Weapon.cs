using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Weapon", menuName = "Items/Weapon")]

public class Weapon : Item
{
    public GameObject prefab;
    public GameObject muzzleFlashParticles;
    public int magazineSize; //количество пулей в магазине
    public int magazineCount; //количество магазиноов, которое мы можем хранить
    public float range; //дальность стрельбы
    public WeaponType weaponType;
    public WeaponStyle weaponStyle;
}

public enum WeaponType { Melee, Pistol, AR, ShotGun, Sniper} //различные виды оружия (пока есть только пистолет и нож)
public enum WeaponStyle { Primary, Secondary, Melee} //различные виды оружия по важности
