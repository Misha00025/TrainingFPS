using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CharacterController), typeof(PlayerHUD))]
public class PlayerController : MonoBehaviour, IMover, IHealthState, IDamagable, IInventory, IEquipmentManager, IShooter, IPicker
{
    [SerializeField] private CameraController _cameraController;
    [SerializeField] private CharacterMover _mover;
    [SerializeField] private PlayerStats _stats;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private EquipmentManager _equipment;
    [SerializeField] private WeaponShooting _shooter;
    [SerializeField] private PlayerPickUp _pickup;
    private Animator anim;

    public CameraController CameraController => _cameraController;
    public Vector3 Direction => _mover.Direction;
    public int Health => _stats.Health;
    public int MaxHealth => _stats.MaxHealth;
    public WeaponStyle CurrentWeaponStyle => _equipment.CurrentWeaponStyle;
    public Weapon CurrentWeapon => _equipment.CurrentWeapon;
    public Transform WeaponHolder => _equipment.WeaponHolder;

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        anim = GetComponentInChildren<Animator>();

        _cameraController.InitVariables();
        _mover.InitVariables(GetComponent<CharacterController>());
        _stats.InitVariables();
        _inventory.InitVariables(this);
        _equipment.InitVariables(this);

        Camera camera = GetComponentInChildren<Camera>();

        _shooter.InitVariables(camera, this);
        _pickup.InitVariables(camera, this);
    }

    public void SetRuning(bool run)
    {
        _mover.SetRuning(run);
        if (_mover.Direction == Vector3.zero)
        {
            anim.SetFloat("Speed", 0f, 0.2f, Time.deltaTime);
        }
        else if (!run)
        {
            anim.SetFloat("Speed", 0.5f, 0.2f, Time.deltaTime);
        }
        else
        {
            anim.SetFloat("Speed", 1f, 0.2f, Time.deltaTime);
        }
    }

    public void Move(Vector3 direction, bool jump) => _mover.Move(direction, jump);
    public void AddListenerToHealthChange(UnityAction<IHealthState> action) => _stats.AddListenerToHealthChange(action);
    public void TakeDamage(int damage) => _stats.TakeDamage(damage);
    public void AddItem(WeaponScriptableObject weapon) => _inventory.AddItem(weapon);
    public void RemoveItem(WeaponStyle weaponStyle) => _inventory.RemoveItem(weaponStyle);
    public Weapon GetItem(WeaponStyle weaponStyle) => _inventory.GetItem(weaponStyle);
    public void SetCurrentWeapon(WeaponStyle weaponStyle) =>  _equipment.SetCurrentWeapon(weaponStyle);
    public void EquipWeapon() => _equipment.EquipWeapon();
    public void UnequipWeapon() => _equipment.UnequipWeapon();
    public void AddListenerToWeaponChanged(UnityAction<Weapon> action) => _equipment.AddListenerToWeaponChanged(action);
    public void Shoot() => _shooter.Shoot();
    public bool TryPickup() => _pickup.TryPickup();
    public void AddListenerToDie(UnityAction<IHealthState> action) => _stats.AddListenerToDie(action);
}
