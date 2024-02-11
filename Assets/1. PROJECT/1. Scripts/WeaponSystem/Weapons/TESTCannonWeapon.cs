using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TESTCannonWeapon : BaseWeapon
{
    public float bulletSpeed = 1000;
    public Transform bulletSpawnPoint;
    public ObjectPool bulletPool;
    

    protected override void Start()
    {
        base.Start();
    }

    public override void Fire()
    {
        if (!CanAttack() || currentAmmo <= 0) return;

        GameObject bullet = bulletPool.GetPooledObject();
        bullet.GetComponent<PoolObject>().Initialize(bulletPool);
        bullet.transform.position = bulletSpawnPoint.position;
        bullet.transform.rotation = bulletSpawnPoint.rotation;

        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        if (bulletRigidbody != null)
        {
            bulletRigidbody.velocity = Vector3.zero; // Сброс скорости на случай, если снаряд был использован ранее
            bulletRigidbody.AddForce(bulletSpawnPoint.forward * bulletSpeed);
        }

        currentAmmo--;
        ResetAttackTimer();
        OnFireEvent.Invoke();
    }
    
    
}
