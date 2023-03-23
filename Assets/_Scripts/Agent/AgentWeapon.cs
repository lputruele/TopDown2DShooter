using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentWeapon : MonoBehaviour
{
    protected float desiredAngle;

    [SerializeField]
    protected WeaponRenderer weaponRenderer;

    [SerializeField]
    protected Weapon weapon;

    private void Awake()
    {
        AssignWeapon();
    }

    protected void AssignWeapon()
    {
        weaponRenderer = GetComponentInChildren<WeaponRenderer>();
        weapon = GetComponentInChildren<Weapon>();
    }

    public virtual void AimWeapon(Vector2 pointerPosition)
    {
        var aimDirection = (Vector3)pointerPosition - transform.position;
        desiredAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        AdjustWeaponRendering();
        transform.rotation = Quaternion.AngleAxis(desiredAngle, Vector3.forward);
    }

    // This function is not used and probably makes a wrong calculation
    public virtual void RotateWeapon(int degrees)
    {
        var aimDirection = Quaternion.Euler(0, 0, degrees) * Vector3.up;
        desiredAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        AdjustWeaponRendering();
        transform.rotation = Quaternion.AngleAxis(desiredAngle, Vector3.forward);
    }

    private void AdjustWeaponRendering()
    {
        if (weaponRenderer != null)
        {
            weaponRenderer.FlipSprite(desiredAngle > 90 || desiredAngle < -90);
            weaponRenderer.RenderBehindHead(desiredAngle < 180 && desiredAngle > 0);
        }
        
    }

    public void Shoot()
    {        
        if (weapon != null)
        {
            weapon.TryShooting();
        }
    }

    public void StopShooting()
    {
        if (weapon != null)
            weapon.StopShooting();
    }
}
