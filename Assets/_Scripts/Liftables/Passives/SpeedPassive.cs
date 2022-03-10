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

        popup popup = FindObjectOfType<popup>();
            popup.SetText("movement speed multiplier");
            popup.ShowText();
        yield return new WaitForSeconds(duration);

        passives.SpeedMultiplier /= multiplier;

        Destroy(gameObject);
    }
}
