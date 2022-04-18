using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : Gun
{
    public float trueAmmo = 50f;
    public LayerMask layerMask;
    RaycastHit hit;

    protected override void UseWeapon()
    {
        if (isShooting && !reloadCoroutine)         // micro-optimization would be to replace relaodCoroutine with ROFCoroutine but I keep it for legibility
        {
            if (Ammo > 0)
            {
                trueAmmo -= Time.deltaTime;
                Ammo = (int)trueAmmo;
                //I'd like the UI of this to show the ammo decreasing & increasing rapidly
                if (infAmmo)
                    trueAmmo += Time.deltaTime;
                OnShoot?.Invoke();
                ShootLazer();
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

    public void ShootLazer()
    {
        //Debug.Log("In shoot lazer");
        Vector3 direction = weaponParent.aimDirection;
        Debug.DrawRay(transform.position, direction * 100);
        hit = new RaycastHit();
        if(Physics.Raycast(transform.position, direction, out hit, 100, layerMask))
        {
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Obstacles")){
                //Debug.Log("Lazer Hitting Obstacles");
            }
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Enemy")){
                Debug.Log("Lazer Hitting Enemy");
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawSphere(hit.point, 0.2f);
    }
}
