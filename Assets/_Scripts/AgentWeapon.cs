using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentWeapon : MonoBehaviour
{
    protected float desiredAngle;

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
            // Explicitly check for null instead of using ?
            // This prevents bugs if weaponRenderer is deleted mid-game
            weaponRenderer.FlipSprite(desiredAngle > 90 || desiredAngle < -90);
            weaponRenderer.RenderBehindHead(desiredAngle < 180 && desiredAngle > 0);
        }

    }

    public void Reload()
    {
        if (weapon != null && weapon.TotalAmmo > 0)
        {
            weapon.Reload();
        }

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
