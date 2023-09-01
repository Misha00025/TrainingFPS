
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerController))]
public class StatsHUD : MonoBehaviour
{
    [SerializeField] private Image _healthBar;
    [SerializeField] private TextMeshProUGUI _healthCounter;

    private PlayerStats _playerStats;

    private void Start()
    {
        InitReferences();

        //HealthBarUpdate(_playerStats);
    }

    private void InitReferences()
    {
        PlayerController controller = GetComponent<PlayerController>();
        _playerStats = controller.Stats;
        _playerStats.AddListenerToHealthChange(HealthBarUpdate);
    }

    private void HealthBarUpdate(IHealthState state)
    {
        int max = state.MaxHealth;
        int current = state.Health;
        float filling = (float)current / max;

        _healthBar.fillAmount = filling;
        _healthCounter.SetText(current.ToString());
    }
}
