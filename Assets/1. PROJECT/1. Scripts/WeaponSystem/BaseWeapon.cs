using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    public void UseWeapon();
}

public abstract class BaseWeapon : MonoBehaviour, IWeapon
{
    public WeaponData weaponData;

    protected float lastAttackTime;
    protected float reloadStartTime;
    protected int currentAmmo;
    protected bool isReloading;

    protected virtual void Start()
    {
        currentAmmo = weaponData.ammo;
    }

    public void UseWeapon()
    {
        if (isReloading)
            return;

        if (currentAmmo <= 0)
        {
            StartReload();
            return;
        }

        if (CanAttack())
        {
            Fire();
        }
    }

    protected virtual void Update()
    {
        if (isReloading)
        {
            if (Time.time - reloadStartTime >= weaponData.reloadTime)
            {
                FinishReload();
            }
        }
    }

    protected void StartReload()
    {
        isReloading = true;
        reloadStartTime = Time.time;
        // Дополнительная логика при начале перезарядки (например, анимация)
    }

    protected void FinishReload()
    {
        isReloading = false;
        currentAmmo = weaponData.ammo;
        // Дополнительная логика при завершении перезарядки
    }

    public abstract void Fire();

    protected bool CanAttack()
    {
        return Time.time - lastAttackTime >= 1f / weaponData.attackRate && !isReloading;
    }

    protected void ResetAttackTimer()
    {
        lastAttackTime = Time.time;
    }
}

