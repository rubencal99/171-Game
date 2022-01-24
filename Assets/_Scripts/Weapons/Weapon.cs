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

    protected PlayerWeapon weaponParent;

    [SerializeField]
    public int ammo;

    [SerializeField]
    public int totalAmmo;

    [SerializeField]
    public bool infAmmo;

    // WeaponDataSO Holds all our weapon data
    [SerializeField]
    protected WeaponDataSO weaponData;

    public int Ammo
    {
        get { return ammo; }
        set {
            ammo = Mathf.Clamp(value, 0, weaponData.MagazineCapacity);
        }
    }

    public int TotalAmmo
    {
        get { return totalAmmo; }
        set {
            totalAmmo = Mathf.Clamp(value, 0, weaponData.MaxAmmoCapacity);
        }
    }

    // Returns true if ammo full
    public bool AmmoFull { get => Ammo >= weaponData.MagazineCapacity; }

    protected bool isShooting = false;

    [SerializeField]
    protected bool rateOfFireCoroutine = false;

    [SerializeField]
    protected bool reloadCoroutine = false;

    private void Start()
    {
        Ammo = weaponData.MagazineCapacity;
        TotalAmmo = weaponData.MaxAmmoCapacity;
        weaponParent = transform.parent.GetComponent<PlayerWeapon>();
        infAmmo = weaponParent.InfAmmo;
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

    // There's a bug where if you switch weapons while reloading, the Coroutine is paused until you reload again
    // Doesn't play reload sound if this happens maybe adjust ammo inside Coroutine?
    public void Reload()
    {
        StartCoroutine(ReloadCoroutine());
        var neededAmmo = Mathf.Min(weaponData.MagazineCapacity - Ammo, TotalAmmo);
        Ammo += neededAmmo;
        TotalAmmo -= neededAmmo;
    }

    protected IEnumerator ReloadCoroutine()
    {
        // rateOfFireCoroutine = true;                      // For some reason using both bools causes bug where if you're spamming fire while the reload ends, you empty your clip within a few frames
        reloadCoroutine = true;
        yield return new WaitForSeconds(weaponData.ReloadSpeed);
        // rateOfFireCoroutine = false;
        reloadCoroutine = false;
    }

    private void Update()
    {
        UseWeapon();
        infAmmo = weaponParent.InfAmmo;
    }

    private void UseWeapon()
    {
        if (isShooting && !rateOfFireCoroutine && !reloadCoroutine)         // micro-optimization would be to replace relaodCoroutine with ROFCoroutine but I keep it for legibility
        {
            if (Ammo > 0)
            {
                Ammo--;
                //I'd like the UI of this to show the ammo decreasing & increasing rapidly
                if (infAmmo)
                    Ammo++;
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
