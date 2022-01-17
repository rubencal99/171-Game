using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

// This script is responsible for firing bullets from the selected weapon
public class Weapon : MonoBehaviour
{
    // This gives us a place to instantiate the bullet ie reference to our gun
    [SerializeField]
    protected GameObject muzzle;

    [SerializeField]
    protected int ammo = 10;

    // WeaponDataSO Holds all our weapon data
    [SerializeField]
    protected WeaponDataSO weaponData;

    public int Ammo
    {
        get { return ammo; }
        set {
            ammo = Mathf.Clamp(value, 0, weaponData.AmmoCapacity);
        }
    }

    // Returns true if ammo full
    public bool AmmoFull { get => Ammo >= weaponData.AmmoCapacity; }

    protected bool isShooting = false;

    [SerializeField]
    protected bool rateOfFireCoroutine = false;

    [SerializeField]
    protected bool reloadCoroutine = false;

    private void Start()
    {
        Ammo = weaponData.AmmoCapacity;
    }

    [field: SerializeField]
    public UnityEvent OnShoot { get; set; }

    [field: SerializeField]
    public UnityEvent OnShootNoAmmo { get; set; }

    public float getReloadSpeed() {
        return weaponData.ReloadSpeed;
    }
    public void TryShooting()
    {
        isShooting = true;
    }
    public void StopShooting()
    {
        isShooting = false;
    }

    public void Reload()
    {
        StartCoroutine(ReloadCoroutine());
        Debug.Log("Reload eta: " + weaponData.ReloadSpeed);
        Ammo = weaponData.AmmoCapacity;
    }

    protected IEnumerator ReloadCoroutine()
    {
        // rateOfFireCoroutine = true;                      // For some reason using both bools causes bug where if you're spamming fire while the reload ends, you empty your clip within a few frames
        reloadCoroutine = true;
        yield return new WaitForSeconds(weaponData.ReloadSpeed);
         Debug.Log("Reload eta: " + weaponData.ReloadSpeed);
        // rateOfFireCoroutine = false;
        reloadCoroutine = false;
    }

    private void Update()
    {
        UseWeapon();
    }

    private void UseWeapon()
    {
        if (isShooting && !rateOfFireCoroutine && !reloadCoroutine)         // micro-optimization would be to replace relaodCoroutine with ROFCoroutine but I keep it for legibility
        {
            if (Ammo > 0)
            {
                Ammo--;
                OnShoot?.Invoke();
                for(int i = 0; i < weaponData.GetBulletCountToSpawn(); i++)
                {
                    ShootBullet();
                }
            }
            else
            {
                isShooting = false;
                OnShootNoAmmo?.Invoke();
                // Reload();                 // Use this if we want to reload automatically
                return;
            }
            FinishShooting();
        }
    }

    private void FinishShooting()
    {
        StartCoroutine(DelayNextShootCoroutine());
        if (weaponData.AutomaticFire == false)
        {
            isShooting = false;
        }
    }

    protected IEnumerator DelayNextShootCoroutine()
    {
        rateOfFireCoroutine = true;
        yield return new WaitForSeconds(weaponData.WeaponDelay);
        rateOfFireCoroutine = false;
    }

    private void ShootBullet()
    {
        SpawnBullet(muzzle.transform.position, CalculateAngle(muzzle));
       // Debug.Log("Bullet shot");
    }

    private void SpawnBullet(Vector3 position, Quaternion rotation)
    {
        var bulletPrefab = Instantiate(weaponData.BulletData.BulletPrefab, position, rotation);
        bulletPrefab.GetComponent<Bullet>().BulletData = weaponData.BulletData;
    }

    // Here we add some randomness for weapon spread
    private Quaternion CalculateAngle(GameObject muzzle)
    {
        float spread = Random.Range(-weaponData.SpreadAngle, weaponData.SpreadAngle);
        Quaternion bulletSpreadRotation = Quaternion.Euler(new Vector3(0, 0, spread));
        return muzzle.transform.rotation * bulletSpreadRotation;
    }
}
