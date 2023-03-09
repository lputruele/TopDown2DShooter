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
                for (int i = 0; i < weaponData.GetBulletCountToSpawn(); i++)
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
        StartCoroutine(DelayNextShootCoroutine(weaponData.WeaponDelay));
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
        bulletPrefab.GetComponent<Bullet>().BulletData = weaponData.BulletData;
    }

    private Quaternion CalculateAngle(GameObject muzzle)
    {
        float spread = Random.Range(-weaponData.SpreadAngle, weaponData.SpreadAngle);
        Quaternion bulletSpreadRotation = Quaternion.Euler(new Vector3(0, 0, spread));
        return muzzle.transform.rotation * bulletSpreadRotation;
    }
}
