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
}
