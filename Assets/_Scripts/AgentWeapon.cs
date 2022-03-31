using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentWeapon : MonoBehaviour
{
    public Vector3 aimDirection;
    public Quaternion rotation;
    public float desiredAngle;
    protected float xMax;
    protected float xMin;
    protected bool flipY;

    // Need our weaponRenderer to call it's functions
    [SerializeField]
    protected WeaponRenderer weaponRenderer;

    [SerializeField]
    public Gun gun;

    [SerializeField]
    public Melee melee;

    [SerializeField]
    public GameObject weapon;

    [SerializeField]
    public bool InfAmmo;

    private void Awake()
    {
        AssignWeapon();
    }

    public void AssignWeapon()
    {
        weaponRenderer = GetComponentInChildren<WeaponRenderer>();
        if(GetComponentInChildren<Gun>())
        {
            gun = GetComponentInChildren<Gun>();
            weapon = gun.gameObject;
            melee = null;
        }
        else if(GetComponentInChildren<Melee>())
        {
            melee = GetComponentInChildren<Melee>();
            weapon = melee.gameObject;
            gun = null;
        }

        
        xMax = gameObject.transform.position.x + 0.5f;
        xMin = gameObject.transform.position.x - 0.5f;
        flipY = false;
    }


    public virtual void AimWeapon(Vector3 pointerPosition)
    {

        aimDirection = ((Vector3)pointerPosition - transform.position).normalized;
        aimDirection.y = 0;
        //Debug.Log("Aim Direction: " + aimDirection);
        // Use arctan to find angle from our x-axis and convert to degrees
        desiredAngle = Mathf.Atan2(aimDirection.z, aimDirection.x) * Mathf.Rad2Deg;
        //Debug.Log("Desired Angle: " + desiredAngle);

        // Calculates rotation between angle A and angle B
        //Debug.Log("Axis of Rotation: " + Vector3.up);
        rotation = Quaternion.AngleAxis(desiredAngle, Vector3.up);
        rotation.x = 0;
        rotation.z = rotation.y;
        rotation.y = 0;
        //Debug.Log("Rotation: " + rotation);
        transform.localRotation = rotation;
        //transform.right = aimDirection;

        AdjustWeaponRendering();

    }

    // Flips weapon sprite if angle is to the left
    // Changes sortingOrder if angle is too high
    private void AdjustWeaponRendering()
    {
        // weaponRenderer?.FlipSprite(desiredAngle > 90 || desiredAngle < -90);
        // weaponRenderer?.RenderBehindHead(desiredAngle < 180 && desiredAngle > 0);
        if (weaponRenderer != null)     
        {
            //Vector3 current_pos = gameObject.transform.localPosition;

            // Explicitly check for null instead of using ?
            // This prevents bugs if weaponRenderer is deleted mid-game
           // weaponRenderer.RenderBehindHead(desiredAngle < 150 && desiredAngle > 0);

            if(desiredAngle > 90 || desiredAngle < -90){ // 6 to 12 Clockwise or LEFT
                //current_pos.x = -0.45f;
                //current_pos.y = -0.20f;
                flipY = true;
            } else { // 12 to 6 clockwise or RIGHT
                //current_pos.x = 0.25f;
                //current_pos.y = 0f;
                flipY = false;
            }   
             //gameObject.transform.localPosition = current_pos;
             weaponRenderer.FlipSprite(flipY);
            // gameObject.GetComponent<SpriteRenderer>().flipY = flipY;
        }

    }

    public void Reload()
    {
        if (gun != null && gun.TotalAmmo > 0 && !(gun.ammo >= gun.totalAmmo))
        {
            gun.TryReloading();
        }

    }

    public void Fill()
    {
        gun.AmmoFill();
    }

    public void Shoot()
    {
        if (gun != null)
        {
            gun.TryShooting();
        }
        else if (melee != null)
        {
            melee.TryMelee();
        }
    }

    public void MeleeAttack()
    {
        if (melee != null)
        {
            melee.TryMelee();
        }

    }

    public void StopShooting()
    {
        if (gun != null)
        {
            gun.StopShooting();
        }

    }

}
