using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PlayerWeapon : AgentWeapon
{
    [SerializeField]
    private AmmoUI ammoUI;

    public bool AmmoFull { get => weapon != null && weapon.AmmoFull;}

    [field: SerializeField]
    public UnityEvent<WeaponDataSO> OnWeaponChange { get; set; }

    private void Awake()
    {
        if (ammoUI == null)
            ammoUI = FindObjectOfType<AmmoUI>();
    }
    private void Start()
    {
        if (weapon != null)
        {
            OnWeaponChange.AddListener(ammoUI.UpdateWeaponUI);
        }
    }

    public void AddAmmo(int amount)
    {
        if (weapon != null)
        {
            weapon.Ammo += amount;
        }
    }

    internal void ChangeWeapon(WeaponDataSO weaponToChange)
    {
        Weapon oldWeapon = weapon;
        bool restartShooting = weapon.IsShooting();
        weapon.OnWeaponSwap?.Invoke();

        foreach (Weapon child in GetComponentsInChildren<Weapon>(true))
        {
            if (child != oldWeapon && child.weaponData.Name == weaponToChange.Name)
            {

                weapon.DelayShootAfterChangeWeapon();

                oldWeapon.gameObject.SetActive(false);
                child.gameObject.SetActive(true);
 
                weaponRenderer = child.GetComponent<WeaponRenderer>();    
                weapon = child;

                weapon.DelayShootAfterChangeWeapon();

                break;
            }            
        }
        if (restartShooting)
        {
            weapon.TryShooting();
        }
        OnWeaponChange?.Invoke(weaponToChange);
        
    }

}
