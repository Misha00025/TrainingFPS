using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BarWithCounter : MonoBehaviour
{
    [SerializeField] private Image _bar;
    [SerializeField] private TextMeshProUGUI _counter;

    public void UpdateBar(int currentCount, int maxCount)
    {
        float filling = (float)currentCount / maxCount;
        if (_bar == null || _counter == null)
            throw new System.Exception($"{name}: Bar or Counter is unset");
        _bar.fillAmount = filling;
        _counter.SetText(currentCount.ToString());
    }
}
