using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ROFPassive : _BasePassive
{
    [SerializeField]
    protected float multiplier;

    public override IEnumerator Pickup(Collider player)
    {
        PlayerPassives passives = player.GetComponent<PlayerPassives>();
        passives.ROFMultiplier *= multiplier;

        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;

        popup popup = FindObjectOfType<popup>();
            popup.SetText("rate of fire multiplier");
            popup.ShowText();
        yield return new WaitForSeconds(duration);

        passives.ROFMultiplier /= multiplier;

        
        Destroy(gameObject);
    }

    public override IEnumerator Pickup(Collision player)
    {
        PlayerPassives passives = player.gameObject.GetComponent<PlayerPassives>();
        passives.ROFMultiplier *= multiplier;

        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;

        popup popup = FindObjectOfType<popup>();
            popup.SetText("rate of fire multiplier");
            popup.ShowText();
        yield return new WaitForSeconds(duration);

        passives.ROFMultiplier /= multiplier;

        
        Destroy(gameObject);
    }
}
