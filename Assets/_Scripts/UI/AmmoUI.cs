using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AmmoUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;

    [SerializeField]
    private Image icon;

    public void UpdateAmmoText(int bulletCount)
    {
        if (bulletCount == 0)
        {
            text.color = Color.red;
        }
        else{
            text.color = Color.white;
        }
        text.SetText(bulletCount.ToString());
    }

    public void UpdateWeaponUI(WeaponDataSO weaponData)
    {
        text.SetText(weaponData.Name);
        icon.sprite = weaponData.Icon;
    }
}
