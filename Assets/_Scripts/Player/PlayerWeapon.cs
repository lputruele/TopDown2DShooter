using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : AgentWeapon
{
    [SerializeField]
    private AmmoUI ammoUI;

    public bool AmmoFull { get => weapon != null && weapon.AmmoFull;}

    private void Awake()
    {
        if (ammoUI == null)
            ammoUI = FindObjectOfType<AmmoUI>();
    }
    private void Start()
    {
        if (weapon != null)
        {
            weapon.OnAmmoChange.AddListener(ammoUI.UpdateAmmoText);
            ammoUI.UpdateAmmoText(weapon.Ammo);
        }
    }

    public void AddAmmo(int amount)
    {
        if (weapon != null)
        {
            weapon.Ammo += amount;
        }
    }
}
