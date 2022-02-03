using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPassive : _BasePassive
{
    [SerializeField]
    protected float multiplier;

    public override IEnumerator Pickup(Collider2D player)
    {
        PlayerPassives passives = player.GetComponent<PlayerPassives>();
        passives.SpeedMultiplier *= multiplier;

        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;

        yield return new WaitForSeconds(duration);

        passives.SpeedMultiplier /= multiplier;

        Destroy(gameObject);
    }
}
