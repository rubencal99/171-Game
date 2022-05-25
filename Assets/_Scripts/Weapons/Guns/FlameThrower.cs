using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

// This script is responsible for firing bullets from the selected weapon
public class FlameThrower : Gun
{
    [field: SerializeField]
    public UnityEvent OnStopShoot { get; set; }
    public float trueAmmo = 50f;

    public float radius;
    public LayerMask layerMask;

    public CapsuleCollider flameCollider;

    protected override void UseWeapon()
    {
        if(swapTimer > 0)
        {
            swapTimer -= Time.deltaTime;
        }
        if (isShooting)         // micro-optimization would be to replace relaodCoroutine with ROFCoroutine but I keep it for legibility
        {
            //Debug.Log("ROF: " + rateOfFireCoroutine);
            //Debug.Log("Reload: " + reloadCoroutine);
            if (Ammo > 0)
            {
                
                //I'd like the UI of this to show the ammo decreasing & increasing rapidly
                if (!infAmmo)
                {
                    trueAmmo -= Time.deltaTime;
                    Ammo = (int)trueAmmo;
                }
                OnShoot?.Invoke();
                CameraShake.Instance.ShakeCamera(weaponData.recoilIntensity, weaponData.recoilFrequency, weaponData.recoilTime);
                ShootFlame();
            }
            else
            {
                isShooting = false;
                OnShootNoAmmo?.Invoke();
                // Reload();                 // Use this if we want to reload automatically
                return;
            }
        }
        else
        {
            OnStopShoot?.Invoke();
            FinishShooting();
        }
    }

    protected void ShootFlame()
    {
        Vector3 secondPoint = transform.position;
        secondPoint.x += 1;
        Collider[] Enemies = Physics.OverlapCapsule(transform.position, secondPoint, radius, layerMask);
        foreach(Collider enemy in Enemies)
        {
            enemy.GetComponent<Enemy>().GetHit(Time.deltaTime, gameObject);
        }
    }
}

