using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadPassive : _BasePassive
{
    [SerializeField]
    protected float multiplier;

    public override IEnumerator Pickup(Collider2D player)
    {
        PlayerPassives passives = player.GetComponent<PlayerPassives>();
        passives.ReloadMultiplier *= multiplier;

        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;

        yield return new WaitForSeconds(duration);

        passives.ReloadMultiplier /= multiplier;

        Destroy(gameObject);
    }
}
