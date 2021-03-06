using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

// This script is responsible for firing bullets from the selected weapon
public class Gun : MonoBehaviour, IWeapon
{
    [field: SerializeField]
    public Vector3 startOffset {get; set;} = Vector3.zero;

    // This gives us a place to instantiate the bullet ie reference to our gun
    [SerializeField]
    public GameObject muzzle;

    protected AgentWeapon weaponParent;

    [SerializeField]
    public PlayerPassives passives;

    [SerializeField]
    public int ammo;

    [SerializeField]
    public int totalAmmo;

    [SerializeField]
    public bool infAmmo;

    // WeaponData Holds all our weapon data
    [SerializeField]
    public WeaponData weaponData;

    [SerializeField]
    public bool isPlayer;

    protected float baseSwapTime = 0.5f;
    [SerializeField]
    protected float swapTime = 0.5f;
    [SerializeField]
    protected float swapTimer;

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

    public bool isShooting = false;

    protected bool isReloading = false;

    [SerializeField]
    protected bool rateOfFireCoroutine = false;

    [SerializeField]
    protected bool reloadCoroutine = false;

    [SerializeField]
    protected bool meleeCoroutine = false;

    /*[SerializeField]
    public string name;*/

    public SpriteRenderer spriteRenderer;
    public Sprite sprite;

    public float reloadAnimMultiplier;

    protected virtual void OnEnable()
    {
        swapTime = baseSwapTime * PlayerSignaler.CallQuickdraw();
        swapTimer = swapTime;
    }

    protected virtual void Start()
    {
        if (transform.root.gameObject.tag == "Player"){
            isPlayer = true;
        }
        Ammo = weaponData.MagazineCapacity;
        TotalAmmo = weaponData.MaxAmmoCapacity;
        if(spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        if(transform.parent.GetComponent<AgentWeapon>())
        {
            weaponParent = transform.parent.GetComponent<AgentWeapon>();
        }
        //if(isPlayer) {           
        passives = weaponParent.transform.parent.GetComponent<PlayerPassives>();
        infAmmo = weaponParent.InfAmmo;
        //}
        //Debug.Log("Weapon Reload Speed: " + weaponData.ReloadSpeed);
         reloadAnimMultiplier = 1f / weaponData.ReloadSpeed;
         //Debug.Log("reloadAnimMultiplier: " + reloadAnimMultiplier);
       // sprite = GetComponent<SpriteRenderer>().sprite;

       //weaponItem.prefab = transform.gameObject;
       //Debug.Log(weaponItem.prefab);
    }

    protected virtual void Update()
    {
        UseWeapon();
        //UseMelee();
        Reload();
       // infAmmo = weaponParent.InfAmmo;
    }


    [field: SerializeField]
    public UnityEvent OnShoot { get; set; }

    [field: SerializeField]
    public UnityEvent OnShootNoAmmo { get; set; }
    
    [field: SerializeField]
    public UnityEvent OnReload { get; set; }

    /*[field: SerializeField]
    public UnityEvent<float, float> OnCameraShake { get; set; }*/

    public virtual float getReloadSpeed() {
        return PlayerSignaler.CallGunnerGloves(this);
    }
    public virtual void TryShooting()
    {
        isShooting = true;
        //Debug.Log("Is shooting = " + isShooting);
    }
    public virtual void StopShooting()
    {
        isShooting = false;
    }

    public virtual void TryReloading()
    {
        if(Ammo < weaponData.MagazineCapacity)
            isReloading = true;
    }
    public void StopReloading()
    {
        isReloading = false;
    }
    public bool CheckSwap()
    {
        if(swapTimer <= 0)
        {
            return true;
        }
        return false;
    }

    // There's a bug where if you switch weapons while reloading, the Coroutine is paused until you reload again
    // Doesn't play reload sound if this happens maybe adjust ammo inside Coroutine?
    public virtual void Reload()
    {
        if(isReloading && !reloadCoroutine) {

            var neededAmmo = Mathf.Min(weaponData.MagazineCapacity - Ammo, TotalAmmo);
            Ammo += neededAmmo;
            TotalAmmo -= neededAmmo;
            if(isPlayer) {
                //Debug.Log("In Reload");
                displayReloadProgressBar();
                this.GetComponent<Animator>().SetFloat("reloadtime", reloadAnimMultiplier * 1 / (getReloadSpeed() / weaponData.ReloadSpeed));
                
                this.GetComponent<Animator>().Play("reload");
            }
            FinishReloading();
            
        }
    }

    public void ForceReload() {
        Debug.Log("In Force Reload");
        reloadCoroutine = false;
        rateOfFireCoroutine = false;
        spriteRenderer.sprite = sprite;
        if(isPlayer)
        {
            GetComponent<Animator>().Play("idle");
        }
    }

    public void AmmoFill()
    {
        TotalAmmo = weaponData.MaxAmmoCapacity;
    }

    public void ReSupply(int frac)
    {
        TotalAmmo += (weaponData.MaxAmmoCapacity)/frac;
    }

    protected virtual void UseWeapon()
    {
        if(swapTimer > 0)
        {
            swapTimer -= Time.deltaTime;
        }
        if (isShooting && !rateOfFireCoroutine && !reloadCoroutine)         // micro-optimization would be to replace relaodCoroutine with ROFCoroutine but I keep it for legibility
        {
            //Debug.Log("ROF: " + rateOfFireCoroutine);
            //Debug.Log("Reload: " + reloadCoroutine);
            if (Ammo > 0)
            {
                //Debug.Log("Casing Recycle = " + PlayerAugmentations.AugmentationList["CasingRecycle"]);
                if(!PlayerSignaler.CallCasingRecycler()){
                    Ammo--;
                }
                //I'd like the UI of this to show the ammo decreasing & increasing rapidly
                if (infAmmo)
                    Ammo++;
                OnShoot?.Invoke();
                CameraShake.Instance.ShakeCamera(weaponData.recoilIntensity, weaponData.recoilFrequency, weaponData.recoilTime);
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

    /*
    public void UseMelee()
    {
        if (isMelee)         // micro-optimization would be to replace relaodCoroutine with ROFCoroutine but I keep it for legibility
        {
            OnMelee?.Invoke();
            SpawnMelee(muzzle.transform.position, CalculateAngle(muzzle, muzzle.transform.position));
        }
        else
        {
            isMelee = false;
            // Reload();                 // Use this if we want to reload automatically
            return;
        }
        FinishMelee();
    }*/
    public void ForceShoot()
    {
        if (Ammo > 0)
        {
            Ammo--;
            //I'd like the UI of this to show the ammo decreasing & increasing rapidly
            if (infAmmo)
                Ammo++;
            OnShoot?.Invoke();
            CameraShake.Instance.ShakeCamera(weaponData.recoilIntensity, weaponData.recoilFrequency, weaponData.recoilTime);
            for(int i = 0; i < weaponData.GetBulletCountToSpawn(); i++)
            {
                
                ShootBullet();
            }
        }
    }

    protected virtual void FinishShooting()
    {
        StartCoroutine(DelayNextShootCoroutine());
        if (weaponData.AutomaticFire == false)
        {
            isShooting = false;
        }
    }
    /*
    private void FinishMelee()
    {
        StartCoroutine(DelayNextMeleeCoroutine());
        
        isMelee = false;
    }*/

    protected void FinishReloading()
    {
        StartCoroutine(DelayNextReloadingCoroutine());
        
        isReloading = false;
    }


    protected virtual IEnumerator DelayNextShootCoroutine()
    {
        rateOfFireCoroutine = true;
        yield return new WaitForSeconds(weaponData.WeaponDelay / passives.ROFMultiplier / PlayerSignaler.CallTriggerHappy());
        rateOfFireCoroutine = false;
    }

     /*protected IEnumerator DelayNextMeleeCoroutine()
    {
        meleeCoroutine = true;
        yield return new WaitForSeconds(swordData.RecoveryLength / passives.ROFMultiplier);
        meleeCoroutine = false;
    }*/

    protected IEnumerator DelayNextReloadingCoroutine()
    {
        reloadCoroutine = true;
        yield return new WaitForSeconds(getReloadSpeed());
        //var neededAmmo = Mathf.Min(weaponData.MagazineCapacity - Ammo, TotalAmmo);
        //Ammo += neededAmmo;
        //TotalAmmo -= neededAmmo;
        reloadCoroutine = false;
    }


    protected void ShootBullet()
    {
        SpawnBullet(muzzle.transform.position);// + muzzle.transform.right);//, CalculateAngle(muzzle));
       // Debug.Log("Bullet shot");
       if (isPlayer)
       {
           // OnCameraShake?.Invoke(weaponData.recoilIntensity, weaponData.recoilTime);
           //CameraShake.Instance.ShakeCamera(weaponData.recoilIntensity, weaponData.recoilFrequency, weaponData.recoilTime);
       }
    }
    /*
    private void SpawnMelee(Vector3 position, Quaternion rotation)
    {
        Debug.Log("Melee");


        var meleePrefab = Instantiate(swordData.BulletData.BulletPrefab, position, rotation);
       // meleePrefab.transform.parent = this.transform;
       meleePrefab.GetComponent<Bullet>().BulletData = weaponData.BulletData;
    }*/

    protected void SpawnBullet(Vector3 position)//, Quaternion rotation)
    {
        Quaternion rotation = CalculateAngle(muzzle, position);
        //var bulletPrefab = Instantiate(weaponData.BulletData.BulletPrefab, position, rotation);
        //bulletPrefab.GetComponent<Bullet>().BulletData = weaponData.BulletData;
        //bulletPrefab.GetComponent<Bullet>().direction = bulletPrefab.transform.rotation * (position - transform.position);
    }

    // Here we add some randomness for weapon spread
    protected virtual Quaternion CalculateAngle(GameObject muzzle, Vector3 position)
    {
        //muzzle.transform.localRotation = weaponParent.transform.localRotation;
        float spread = Random.Range(-weaponData.SpreadAngle, weaponData.SpreadAngle);
        Quaternion bulletSpreadRotation = Quaternion.Euler(new Vector3(0, spread, 0));
        Quaternion rotation = weaponParent.transform.localRotation * bulletSpreadRotation;
        //Debug.Log("weaponParent.transform.localRotation: " + weaponParent.transform.localRotation);
       // Debug.Log("Rotation: " + rotation);

        //Debug.Log("Bullet rotation: " + rotation);
        //Debug.Log("Bullet spread rotation: " + bulletSpreadRotation);

        var bulletPrefab = Instantiate(weaponData.BulletData.BulletPrefab, position, Quaternion.identity);
        RegularBullet bullet = bulletPrefab.GetComponent<RegularBullet>();
        bullet.BulletData = weaponData.BulletData;
        bullet.direction = (bulletSpreadRotation * (weaponParent.aimDirection)).normalized;//bulletSpreadRotation * (weaponParent.aimDirection);
        bullet.direction.y = 0;
        bullet.transform.right = bullet.direction;
        //bulletPrefab.SpriteTransform.
     //   Debug.Log("Bullet Direction: " + bulletPrefab.GetComponent<Bullet>().direction);
      //  Debug.Log("Bullet Rotation: " + bulletPrefab.GetComponent<Bullet>().transform.rotation);

        return rotation;
    }

    protected void displayReloadProgressBar() {
       var reloadBar = this.transform.parent.parent.GetComponentInChildren<PlayerReload>();
       reloadBar.displayReloadProgressBar();
       
    }
}

