using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    protected GameObject muzzle;

    [SerializeField]
    protected int ammo = 10;

    [SerializeField]
    public WeaponDataSO weaponData;

    [SerializeField]
    public WeaponBonusStats weaponBonusStats;
    [SerializeField]
    public BulletBonusStats bulletBonusStats;

    public int Ammo {
        get { return ammo; }
        set { 
            ammo = Mathf.Clamp(value, 0, weaponData.AmmoCapacity);
            OnAmmoChange?.Invoke(ammo);
        }
    }

    public bool AmmoFull { get => Ammo >= weaponData.AmmoCapacity; }

    private bool isChangingWeapon;

    protected bool isShooting = false;

    [SerializeField]
    protected bool reloadCoroutine = false;

    [field:SerializeField]
    public UnityEvent OnShoot { get; set; }

    [field: SerializeField]
    public UnityEvent OnShootNoAmmo { get; set; }

    [field: SerializeField]
    public UnityEvent<int> OnAmmoChange { get; set; }

    [field: SerializeField]
    public UnityEvent OnWeaponSwap { get; set; }

    private void Start()
    {
        Ammo = weaponData.AmmoCapacity;
        weaponBonusStats = GetComponentInParent<WeaponBonusStats>();
        bulletBonusStats = GetComponentInParent<BulletBonusStats>();
    }
    public void TryShooting()
    {
        isShooting = true;
    }


    public void StopShooting()
    {
        isShooting = false;
    }

    public bool IsShooting()
    {
        return isShooting;
    }

    public void SetShooting(bool value)
    {
        isShooting = value;
    }

    public void Reload(int ammo)
    {
        Ammo += ammo;
    }

    private void Update()
    {
        UseWeapon();
    }

    private void UseWeapon()
    {
        if (isShooting && !reloadCoroutine && !isChangingWeapon)
        {
            if (Ammo > 0)
            {
                Ammo -= weaponData.AmmoPerShot;
                OnShoot?.Invoke();
                for (int i = 0; i < weaponData.GetBulletCountToSpawn() + weaponBonusStats.BulletCountBonus; i++)
                {
                    ShootBullet();
                }
            }
            else
            {
                isShooting = false;
                OnShootNoAmmo?.Invoke();
                return;
            }
            FinishShooting();
        }
    }

    private void FinishShooting()
    {
        StartCoroutine(DelayNextShootCoroutine(weaponData.WeaponDelay - weaponBonusStats.WeaponDelayBonus));
        if (!weaponData.AutomaticFire)
        {
            isShooting = false;
        }
    }

    protected IEnumerator DelayNextShootCoroutine(float timeDelay)
    {
        reloadCoroutine = true;
        yield return new WaitForSeconds(timeDelay);
        reloadCoroutine = false;
    }

    public void DelayShootAfterChangeWeapon()
    {
        StartCoroutine(DelayNextShootCoroutine(.2f));
        isChangingWeapon = false;
    }



    private void ShootBullet()
    {
        SpawnBullet(muzzle.transform.position, CalculateAngle(muzzle));
    }

    private void SpawnBullet(Vector3 position, Quaternion rotation)
    {
        var bulletPrefab = Instantiate(weaponData.BulletData.BulletPrefab, position, rotation);
        bulletPrefab.transform.localScale += new Vector3(weaponBonusStats.BulletSizeBonus, weaponBonusStats.BulletSizeBonus, 0);
        bulletPrefab.GetComponent<Bullet>().BulletData = weaponData.BulletData;
        bulletPrefab.GetComponent<Bullet>().bulletBonusStats = bulletBonusStats;
    }

    private Quaternion CalculateAngle(GameObject muzzle)
    {
        float minSpreadAngle = -weaponData.SpreadAngle - weaponBonusStats.SpreadAngleBonus;
        float maxSpreadAngle = weaponData.SpreadAngle + weaponBonusStats.SpreadAngleBonus;
        float spread = Random.Range(minSpreadAngle, maxSpreadAngle);
        Quaternion bulletSpreadRotation = Quaternion.Euler(new Vector3(0, 0, spread));
        return muzzle.transform.rotation * bulletSpreadRotation;
    }
}
