using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Weapon", menuName = "Items/Weapon")]
public class WeaponScriptableObject : Item
{
    [field: SerializeField] public GameObject prefab { get; private set; }
    [field: SerializeField] public GameObject muzzleFlashParticles { get; private set; }
    [field: SerializeField] public int magazineSize { get; private set; }
    [field: SerializeField] public int magazineCount { get; private set; }
    [field: SerializeField] public float range { get; private set; }
    [field: SerializeField] public float fireRate { get; private set; }
    [field: SerializeField] public WeaponType weaponType { get; private set; }
    [field: SerializeField] public WeaponStyle weaponStyle { get; private set; }
}

public class Weapon : IWeapon, IShooter, IDisablable, IAmmo
{
    private WeaponScriptableObject _config;
    private GameObject _gameObject = null;
    private GameObject _muzzleFlashParticles = null;
    private Transform _barel = null;

    private float _range;
    private float _fireRate;
    private int _ammoCount;
    private int _maxAmmoCount;

    private WeaponType _type;
    private WeaponStyle _style;

    public Weapon(WeaponScriptableObject scriptableObject, Transform parrent)
    {
        _config = scriptableObject;
        _gameObject = GameObject.Instantiate(scriptableObject.prefab, parrent);
        _muzzleFlashParticles = scriptableObject.muzzleFlashParticles;
        _barel = _gameObject.transform.GetChild(0);

        _range = scriptableObject.range;
        _fireRate = scriptableObject.fireRate;
        _maxAmmoCount = scriptableObject.magazineCount * scriptableObject.magazineSize;
        _ammoCount = _maxAmmoCount;

        _type = scriptableObject.weaponType;
        _style = scriptableObject.weaponStyle;

        Disable();
    }

    public float Range => _range;
    public float FireRate => _fireRate;
    public WeaponType Type => _type;
    public WeaponStyle Style => _style;
    public bool IsEnable => _gameObject.activeSelf;
    public int Count => _ammoCount;
    public int MaxCount => _maxAmmoCount;
    public WeaponScriptableObject Config => _config;

    AmmoType IAmmo.Type => AmmoType.None;

    public void Disable() => _gameObject.SetActive(false);
    public void Enable() => _gameObject.SetActive(true);
    public void Shoot()
    {
        if (_muzzleFlashParticles != null)
            GameObject.Instantiate(_muzzleFlashParticles, _barel);
    }
}

interface IWeapon
{
    WeaponScriptableObject Config { get; }
    float Range { get; }
    float FireRate { get; }
    WeaponType Type { get; }
    WeaponStyle Style { get; }
}

interface IShooter
{
    void Shoot();
}

interface IDisablable
{
    bool IsEnable { get; }
    void Disable();
    void Enable();
}

public enum WeaponType { Melee, Pistol, AR, ShotGun, Sniper} 
public enum WeaponStyle { Primary, Secondary, Melee, None} 
