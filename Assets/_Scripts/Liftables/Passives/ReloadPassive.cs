using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadPassive : _BasePassive
{
    [SerializeField]
    protected float multiplier;

    public override IEnumerator Pickup(Collider player)
    {
        PlayerPassives passives = player.GetComponent<PlayerPassives>();
        passives.ReloadMultiplier *= multiplier;
         popup popup = FindObjectOfType<popup>();
            popup.SetText("reload speed increase");
            popup.ShowText();

        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;

        yield return new WaitForSeconds(duration);

        passives.ReloadMultiplier /= multiplier;

       
        Destroy(gameObject);
    }
}
