using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI magazineSizeText;
    [SerializeField] private TextMeshProUGUI magazineCountText;

    public void UpdateInfo(Sprite weaponIcon, int currentAmo, int maxAmmo)
    {
        icon.sprite = weaponIcon;
        magazineSizeText.SetText( currentAmo.ToString() );
        magazineCountText.SetText( maxAmmo.ToString() );
    }
}
