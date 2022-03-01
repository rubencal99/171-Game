using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Contains functions and overrides specific to enemy guns
public class EnemyGun : Gun
{
    // Overriding this function to add randomness to enemy firing patterns
    protected override IEnumerator DelayNextShootCoroutine()
    {
        rateOfFireCoroutine = true;
        float delay = UnityEngine.Random.Range(weaponData.WeaponDelay, weaponData.WeaponDelay + 1);
        yield return new WaitForSeconds(delay / passives.ROFMultiplier);
        rateOfFireCoroutine = false;
    }

    public override float getReloadSpeed()
    {
        return weaponData.ReloadSpeed;
    }

    protected override void UseWeapon()
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
                Debug.Log("About to reload");
                TryReloading();                 // Use this if we want to reload automatically
                return;
            }
            FinishShooting();
        }
    }
}
