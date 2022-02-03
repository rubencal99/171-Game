using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteAmmoPassive : _BasePassive
{
    [SerializeField]
    protected float multiplier;

    [SerializeField]
    private PlayerWeapon weaponParent;

    public override IEnumerator Pickup(Collider2D player)
    {
        weaponParent = player.gameObject.GetComponentInChildren<PlayerWeapon>();
        weaponParent.InfAmmo = true;

        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;

        yield return new WaitForSeconds(duration);

        weaponParent.InfAmmo = false;

        Destroy(gameObject);
    }
}
