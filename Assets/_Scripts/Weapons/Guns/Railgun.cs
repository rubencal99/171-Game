using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

// This script is responsible for firing bullets from the selected weapon
public class Railgun : Gun
{
    public bool holding;
    public float maxHold = 2f;
    public float holdTimer;
    private void Start()
    {
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
       // sprite = GetComponent<SpriteRenderer>().sprite;

       //weaponItem.prefab = transform.gameObject;
       //Debug.Log(weaponItem.prefab);
    }

    protected void Update()
    {
        UseWeapon();
        //UseMelee();
        Reload();
       // infAmmo = weaponParent.InfAmmo;
       if(holding){
           holdTimer += Time.deltaTime;
       }
    }

    protected override void UseWeapon()
    {
        swapTimer -= Time.deltaTime;
        //Debug.Log("isShooting = " + isShooting);
        if (isShooting && !rateOfFireCoroutine && !reloadCoroutine)         // micro-optimization would be to replace relaodCoroutine with ROFCoroutine but I keep it for legibility
        {
            //Debug.Log("In useWeapon rail");
            //Debug.Log("ROF: " + rateOfFireCoroutine);
            //Debug.Log("Reload: " + reloadCoroutine);
            if (Ammo > 0)
            {
                //holdTimer += Time.deltaTime;
                holding = true;
                //Debug.Log(PlayerSignaler.CallCasingRecycler());
                if(!PlayerSignaler.CallCasingRecycler()){
                    Ammo--;
                }
                //I'd like the UI of this to show the ammo decreasing & increasing rapidly
                if (infAmmo)
                    Ammo++;
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
        else if((!isShooting && holdTimer > 0) || holdTimer >= maxHold)
        {
            OnShoot?.Invoke();
            for(int i = 0; i < weaponData.GetBulletCountToSpawn(); i++)
            {
                
                ShootBullet();
            }
            holdTimer = 0;
            holding = false;
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
       // Debug.Log("Rotation: " + rotation);

        //Debug.Log("Bullet rotation: " + rotation);
        //Debug.Log("Bullet spread rotation: " + bulletSpreadRotation);

        var bulletPrefab = Instantiate(weaponData.BulletData.BulletPrefab, position, rotation);
        bulletPrefab.transform.localScale = new Vector3(0.2f + holdTimer, 0.2f + holdTimer, 1);
        bulletPrefab.GetComponent<Bullet>().BulletData = weaponData.BulletData;
        bulletPrefab.GetComponent<Bullet>().direction = bulletSpreadRotation * (weaponParent.aimDirection);//bulletSpreadRotation * (weaponParent.aimDirection);
     //   Debug.Log("Bullet Direction: " + bulletPrefab.GetComponent<Bullet>().direction);
      //  Debug.Log("Bullet Rotation: " + bulletPrefab.GetComponent<Bullet>().transform.rotation);

        return rotation;
    }
}

