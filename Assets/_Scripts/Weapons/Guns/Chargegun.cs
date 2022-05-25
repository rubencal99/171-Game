using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

// This script is responsible for firing bullets from the selected weapon
public class Chargegun : Railgun
{
    public Animator animator;
    public bool linearAnim = true;
    public bool singleAnim = true;
    protected override void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if(animator == null)
        {
            animator = GetComponent<Animator>();
        }
        if (transform.root.gameObject.tag == "Player"){
            isPlayer = true;
        }
        Ammo = weaponData.MagazineCapacity;
        TotalAmmo = weaponData.MaxAmmoCapacity;
        if(transform.parent.GetComponent<AgentWeapon>())
        {
            weaponParent = transform.parent.GetComponent<AgentWeapon>();
        }
        if(isPlayer) {           
            passives = weaponParent.transform.parent.GetComponent<PlayerPassives>();
            infAmmo = weaponParent.InfAmmo;
        }
        holdTimer = 0;
        reloadAnimMultiplier = 1f / weaponData.ReloadSpeed;
       // sprite = GetComponent<SpriteRenderer>().sprite;

       //weaponItem.prefab = transform.gameObject;
       //Debug.Log(weaponItem.prefab);
    }

    protected override void Update()
    {
        if(swapTimer > 0)
        {
            swapTimer -= Time.deltaTime;
        }
        UseWeapon();
        //UseMelee();
        Reload();
       // infAmmo = weaponParent.InfAmmo;
       if(holding && holdTimer < maxHold){
           holdTimer += Time.deltaTime * PlayerSignaler.CallTriggerHappy();
       }
    }

    public override void TryReloading()
    {
        if(Ammo < weaponData.MagazineCapacity)
            holding = false;
            holdTimer = 0;
            isReloading = true;
    }

    public override void TryShooting()
    {
        holding = true;
        if(holdTimer >= maxHold)
        {
            holdTimer = maxHold;
            isShooting = true;
        }
    }
    public override void StopShooting()
    {
        holding = false;
        isShooting = false;
    }

    protected override void UseWeapon()
    {
        CheckAnimation();
        if(!holding)
        {
            isShooting = false;
        }
        else if(holdTimer >= maxHold)
        {
            holdTimer = maxHold;
            isShooting = true;
        }
        if (isShooting && !rateOfFireCoroutine && !reloadCoroutine)         // micro-optimization would be to replace relaodCoroutine with ROFCoroutine but I keep it for legibility
        {
            if (Ammo > 0)
            {
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
                // Reload();                 // Use this if we want to reload automatically
                return;
            }
            FinishShooting();
        }
        else if(!holding && holdTimer > 0)
        {
            holdTimer -= Time.deltaTime;
            holding = false;
        }
    }

    protected override void FinishShooting()
    {
        if(!weaponData.AutomaticFire)
        {
            //holdTimer = 0;
            holding = false;
        }
        StartCoroutine(DelayNextShootCoroutine());
        if (weaponData.AutomaticFire == false)
        {
            isShooting = false;
        }
    }

    void CheckAnimation()
    {

        if(singleAnim)
        {
            CheckSingleAnim();
        }
        else
        {
            CheckDoubleAnim();
        }
    }

    void CheckSingleAnim()
    {
        if(holdTimer <= 0)
        {
            Debug.Log("Single Anim Reset");
            Debug.Log("holdTimer = " + holdTimer);
            animator.SetTrigger("Reset");
            animator.SetFloat("Charge", 0);
        }
        else if(isReloading)
        {
            if(linearAnim)
            {
                animator.SetFloat("Charge", 1 * PlayerSignaler.CallTriggerHappy());
            }
            else
            {
                animator.SetFloat("Charge", holdTimer * PlayerSignaler.CallTriggerHappy());
            }
        }
        else if(!holding && holdTimer > 0)
        {
            if(linearAnim)
            {
                animator.SetFloat("Charge", 1 * PlayerSignaler.CallTriggerHappy());
            }
            else
            {
                animator.SetFloat("Charge", holdTimer * PlayerSignaler.CallTriggerHappy());
            }
        }
        else
        {
            animator.SetTrigger("Attack");
            if(linearAnim)
            {
                animator.SetFloat("Charge", 1 * PlayerSignaler.CallTriggerHappy());
            }
            else
            {
                animator.SetFloat("Charge", holdTimer * PlayerSignaler.CallTriggerHappy());
            }
        }
    }

    void CheckDoubleAnim()
    {
        if(holdTimer <= 0 && animator.GetFloat("Charge") != 0)
        {
            animator.SetTrigger("Reset");
            animator.SetBool("Attack1", false);
            animator.SetFloat("Charge", 0);
        }
        else if(isReloading)
        {
            if(linearAnim)
            {
                animator.SetFloat("Charge", -1 * PlayerSignaler.CallTriggerHappy() * 2);
            }
            else
            {
                animator.SetFloat("Charge", -holdTimer * PlayerSignaler.CallTriggerHappy() * 2);
            }
        }
        else if(!holding && holdTimer > 0)
        {
            if(linearAnim)
            {
                animator.SetFloat("Charge", -1 * PlayerSignaler.CallTriggerHappy());
            }
            else
            {
                animator.SetFloat("Charge", -holdTimer * PlayerSignaler.CallTriggerHappy());
            }
        }
        else if(holding && holdTimer >= maxHold)
        {
            animator.SetBool("Attack1", true);
            animator.SetFloat("Charge", 0);
        }
        else if(holding)
        {
            animator.SetTrigger("Attack");
            animator.SetBool("Attack1", true);
            if(linearAnim)
            {
                animator.SetFloat("Charge", 1 * PlayerSignaler.CallTriggerHappy());
            }
            else
            {
                animator.SetFloat("Charge", holdTimer * PlayerSignaler.CallTriggerHappy());
            }
        }
    }

    /*public void ForceShoot()
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
    }*/

    /*protected virtual void FinishShooting()
    {
        StartCoroutine(DelayNextShootCoroutine());
        if (weaponData.AutomaticFire == false)
        {
            isShooting = false;
        }
    }*/


    /*protected virtual IEnumerator DelayNextShootCoroutine()
    {
        rateOfFireCoroutine = true;
        yield return new WaitForSeconds(weaponData.WeaponDelay / passives.ROFMultiplier);
        rateOfFireCoroutine = false;
    }*/


    // Here we add some randomness for weapon spread
    protected override Quaternion CalculateAngle(GameObject muzzle, Vector3 position)
    {
        //muzzle.transform.localRotation = weaponParent.transform.localRotation;
        float spread = Random.Range(-weaponData.SpreadAngle, weaponData.SpreadAngle);
        Quaternion bulletSpreadRotation = Quaternion.Euler(new Vector3(0, spread, 0));
        Quaternion rotation = weaponParent.transform.localRotation * bulletSpreadRotation;
        //Debug.Log("weaponParent.transform.localRotation: " + weaponParent.transform.localRotation);
        //Debug.Log("Bullet spread rotation: " + bulletSpreadRotation);
       // Debug.Log("Rotation: " + rotation);
        //rotation.y = rotation.z;
        //rotation.z = 0;
        //Debug.Log("Bullet rotation: " + rotation);
        //Debug.Log("");

        var bulletPrefab = Instantiate(weaponData.BulletData.BulletPrefab, position, Quaternion.identity);
        //bulletPrefab.transform.localScale = new Vector3(0.2f + holdTimer, 0.2f + holdTimer, 1);
        RegularBullet bullet = bulletPrefab.GetComponent<RegularBullet>();
        bullet.BulletData = weaponData.BulletData;
        bullet.direction = (bulletSpreadRotation * (weaponParent.aimDirection)).normalized;//bulletSpreadRotation * (weaponParent.aimDirection);
        bullet.direction.y = 0;
        bullet.transform.right = bullet.direction;
     //   Debug.Log("Bullet Direction: " + bulletPrefab.GetComponent<Bullet>().direction);
      //  Debug.Log("Bullet Rotation: " + bulletPrefab.GetComponent<Bullet>().transform.rotation);

        return rotation;
    }
}

