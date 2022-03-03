using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

// This script is responsible for firing bullets from the selected weapon
public class Gun : MonoBehaviour
{
    // This gives us a place to instantiate the bullet ie reference to our gun
    [SerializeField]
    protected GameObject muzzle;

    protected AgentWeapon weaponParent;

    [SerializeField]
    public PlayerPassives passives;

    [SerializeField]
    public int ammo;

    [SerializeField]
    public int totalAmmo;

    [SerializeField]
    public bool infAmmo;

    // WeaponDataSO Holds all our weapon data
    [SerializeField]
    public WeaponDataSO weaponData;

    // WeaponDataSO Holds all our weapon data
    [SerializeField]
    protected MeleeDataSO swordData;

    [SerializeField]
    public bool isPlayer;

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

    protected bool isMelee = false;

    protected bool isReloading = false;

    [SerializeField]
    protected bool rateOfFireCoroutine = false;

    [SerializeField]
    protected bool reloadCoroutine = false;

    [SerializeField]
    protected bool meleeCoroutine = false;

    private void Start()
    {
        if (transform.root.gameObject.tag == "Player"){
            isPlayer = true;
        }
        Ammo = weaponData.MagazineCapacity;
        TotalAmmo = weaponData.MaxAmmoCapacity;
        weaponParent = transform.parent.GetComponent<AgentWeapon>();
        passives = weaponParent.transform.parent.GetComponent<PlayerPassives>();
        infAmmo = weaponParent.InfAmmo;
    }

    [field: SerializeField]
    public UnityEvent OnShoot { get; set; }

     [field: SerializeField]
    public UnityEvent OnMelee { get; set; }

    [field: SerializeField]
    public UnityEvent OnShootNoAmmo { get; set; }
    
    [field: SerializeField]
    public UnityEvent OnReload { get; set; }

    /*[field: SerializeField]
    public UnityEvent<float, float> OnCameraShake { get; set; }*/

    public virtual float getReloadSpeed() {
        return PlayerSignaler.CallGunnerGloves(this);
    }
    public void TryShooting()
    {
        isShooting = true;
    }
    public void StopShooting()
    {
        isShooting = false;
    }

     public void TryMelee()
    {
        isMelee = true;
    }
    public void StopMelee()
    {
        isMelee = false;
    }

    public void TryReloading()
    {
        if(Ammo < weaponData.MagazineCapacity)
            isReloading = true;
    }
    public void StopReloading()
    {
        isReloading = false;
    }

    // There's a bug where if you switch weapons while reloading, the Coroutine is paused until you reload again
    // Doesn't play reload sound if this happens maybe adjust ammo inside Coroutine?
    public void Reload()
    {
        if(isReloading && !reloadCoroutine) {

            var neededAmmo = Mathf.Min(weaponData.MagazineCapacity - Ammo, TotalAmmo);
            Ammo += neededAmmo;
            TotalAmmo -= neededAmmo;
            if(isPlayer) {
                Debug.Log("In Reload");
                displayReloadProgressBar();
                this.GetComponent<Animator>().SetFloat("reloadtime", ( 10.0f - (weaponData.ReloadSpeed / passives.ReloadMultiplier)) / 10.0f);
                this.GetComponent<Animator>().Play("reload");
            }
            FinishReloading();
            
        }
    }

    public void AmmoFill()
    {
        TotalAmmo = weaponData.MaxAmmoCapacity;
    }

    protected void Update()
    {
        UseWeapon();
        UseMelee();
        Reload();
        infAmmo = weaponParent.InfAmmo;
    }

    protected virtual void UseWeapon()
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

    public void UseMelee()
    {
        if (isMelee)         // micro-optimization would be to replace relaodCoroutine with ROFCoroutine but I keep it for legibility
        {
            OnMelee?.Invoke();
            SpawnMelee(muzzle.transform.position, CalculateAngle(muzzle));
            }
            else
            {
                isMelee = false;
                // Reload();                 // Use this if we want to reload automatically
                return;
            }
            FinishMelee();
        }

    protected void FinishShooting()
    {
        StartCoroutine(DelayNextShootCoroutine());
        if (weaponData.AutomaticFire == false)
        {
            isShooting = false;
        }
    }

    private void FinishMelee()
    {
        StartCoroutine(DelayNextMeleeCoroutine());
        
        isMelee = false;
    }

    private void FinishReloading()
    {
        StartCoroutine(DelayNextReloadingCoroutine());
        
        isReloading = false;
    }


    protected virtual IEnumerator DelayNextShootCoroutine()
    {
        rateOfFireCoroutine = true;
        yield return new WaitForSeconds(weaponData.WeaponDelay / passives.ROFMultiplier);
        rateOfFireCoroutine = false;
    }

     protected IEnumerator DelayNextMeleeCoroutine()
    {
        meleeCoroutine = true;
        yield return new WaitForSeconds(swordData.RecoveryLength / passives.ROFMultiplier);
        meleeCoroutine = false;
    }

    protected IEnumerator DelayNextReloadingCoroutine()
    {
        reloadCoroutine = true;
        yield return new WaitForSeconds( weaponData.ReloadSpeed / passives.ROFMultiplier);
        //var neededAmmo = Mathf.Min(weaponData.MagazineCapacity - Ammo, TotalAmmo);
        //Ammo += neededAmmo;
        //TotalAmmo -= neededAmmo;
        reloadCoroutine = false;
    }


    protected void ShootBullet()
    {
        SpawnBullet(muzzle.transform.position, CalculateAngle(muzzle));
       // Debug.Log("Bullet shot");
       if (isPlayer)
       {
           // OnCameraShake?.Invoke(weaponData.recoilIntensity, weaponData.recoilTime);
           CameraShake.Instance.ShakeCamera(weaponData.recoilIntensity, weaponData.recoilFrequency, weaponData.recoilTime);
       }
    }

    private void SpawnMelee(Vector3 position, Quaternion rotation)
    {
        Debug.Log("Melee");


        var meleePrefab = Instantiate(swordData.BulletData.BulletPrefab, position, rotation);
       // meleePrefab.transform.parent = this.transform;
       meleePrefab.GetComponent<Bullet>().BulletData = weaponData.BulletData;
    }

    private void SpawnBullet(Vector3 position, Quaternion rotation)
    {
        var bulletPrefab = Instantiate(weaponData.BulletData.BulletPrefab, position, rotation);
        bulletPrefab.GetComponent<Bullet>().BulletData = weaponData.BulletData;
        bulletPrefab.GetComponent<Bullet>().direction = position - transform.position;
    }

    // Here we add some randomness for weapon spread
    protected Quaternion CalculateAngle(GameObject muzzle)
    {
        float spread = Random.Range(-weaponData.SpreadAngle, weaponData.SpreadAngle);
        Quaternion bulletSpreadRotation = Quaternion.Euler(new Vector3(0, 0, spread));
        return muzzle.transform.rotation * bulletSpreadRotation;
    }

    protected void displayReloadProgressBar() {
       var reloadBar = this.transform.parent.parent.GetComponentInChildren<PlayerReload>();
       reloadBar.displayReloadProgressBar();
       
    }
}

