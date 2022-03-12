using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentWeapon : MonoBehaviour
{
    protected float desiredAngle;
    protected float xMax;
    protected float xMin;
    protected bool flipY;

    // Need our weaponRenderer to call it's functions
    [SerializeField]
    protected WeaponRenderer weaponRenderer;

    [SerializeField]
    public Gun weapon;

    [SerializeField]
    public bool InfAmmo;

    private void Awake()
    {
        AssignWeapon();
    }

    public void AssignWeapon()
    {
        weaponRenderer = GetComponentInChildren<WeaponRenderer>();
        weapon = GetComponentInChildren<Gun>();
        xMax = gameObject.transform.position.x + 0.5f;
        xMin = gameObject.transform.position.x - 0.5f;
        flipY = false;
    }


    public virtual void AimWeapon(Vector2 pointerPosition)
    {

        var aimDirection = (Vector3)pointerPosition - transform.position;
        // Use arctan to find angle from our x-axis and convert to degrees
        desiredAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        AdjustWeaponRendering();
        // Calculates rotation between angle A and angle B
        transform.rotation = Quaternion.AngleAxis(desiredAngle, Vector3.forward);

    }

    // Flips weapon sprite if angle is to the left
    // Changes sortingOrder if angle is too high
    private void AdjustWeaponRendering()
    {
        // weaponRenderer?.FlipSprite(desiredAngle > 90 || desiredAngle < -90);
        // weaponRenderer?.RenderBehindHead(desiredAngle < 180 && desiredAngle > 0);
        if (weaponRenderer != null)     
        {
            Vector3 current_pos = gameObject.transform.localPosition;

            // Explicitly check for null instead of using ?
            // This prevents bugs if weaponRenderer is deleted mid-game
           // weaponRenderer.RenderBehindHead(desiredAngle < 150 && desiredAngle > 0);

            if(desiredAngle > 90 || desiredAngle < -90){ // 6 to 12 Clockwise or LEFT
             current_pos.x = -0.45f;
             current_pos.y = -0.20f;
            flipY = true;
            } else { // 12 to 6 clockwise or RIGHT
            current_pos.x = 0.25f;
            current_pos.y = 0f;
            flipY = false;
            }   
             gameObject.transform.localPosition = current_pos;
             weaponRenderer.FlipSprite(flipY);
            // gameObject.GetComponent<SpriteRenderer>().flipY = flipY;
        }

    }

    public void Reload()
    {
        if (weapon != null && weapon.TotalAmmo > 0 && !(weapon.ammo >= weapon.totalAmmo))
        {
            weapon.TryReloading();
        }

    }

    public void Fill()
    {
        weapon.AmmoFill();
    }

    public void Shoot()
    {
        if (weapon != null)
        {
            weapon.TryShooting();
        }

    }

    public void MeleeAttack()
    {
        if (weapon != null)
        {
            weapon.TryMelee();
        }

    }

    public void StopShooting()
    {
        if (weapon != null)
        {
            weapon.StopShooting();
        }

    }

}
