using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffAction : AIAction
{
    public bool hasAlly = false;

    GameObject self;

    GameObject allyToBuff;
    AgentRenderer allySprite;

    public float buffTimer;
    public float speedBuff;
    public float ROFBuff;

    void Start(){
        self = this.transform.root.gameObject;
    }

    public override void TakeAction(){

        // Give enemies isBuffed attribute

        // Grab ally to buff
        // If ally dies, grab new ally
        if (allyToBuff == null)
        {
            // If we somehow only find ourself, do nothing
            if (allyToBuff == self)
            {
                return;
            }
            allyToBuff = FindAlly();

            // While enemy alive, buff
            if (hasAlly)
            {
                BuffAlly(allyToBuff);
            }
        }

        // If we somehow only find ourself, do nothing
        if (allyToBuff == self)
        {
            return;
        }

        // if no more enemies, transition

    }

    void BuffAlly(GameObject ally)
    {
        // AgentAnimations anim = this.transform.parent.GetComponentInChildren<AgentAnimations>();
        // //anim.SetBuffAnimation(true); 
        // Debug.Log("In buffAlly");
        // Here is where we'd want to call a coroutine or animation
        PlayerPassives allyPassives = ally.GetComponent<PlayerPassives>();
        // StartCoroutine(MutationProcess());
        MutationProcess();
        allyPassives.SpeedMultiplier *= speedBuff;
        allyPassives.ROFMultiplier *= ROFBuff;
        self.GetComponentInChildren<AgentAnimations>().SetBuffAnimation(true);
    }

    void MutationProcess()
    {
        allySprite = allyToBuff.GetComponentInChildren<AgentRenderer>();
        allySprite.isBuffed = true;
    }

    /* private IEnumerator MutationProcess()
    {
        AgentRenderer allySprite = allyToBuff.GetComponentInChildren<AgentRenderer>();
        allySprite.isBuffed = true;
        yield return new WaitForSeconds(buffTimer);
    }*/

    // If buffer dies, ally's Buffs turn off
    void OnDestroy()
    {
        // Debug.Log("Buffer Destroyed");
        PlayerPassives allyPassives = null;
        if (allyToBuff != null)
        {
            allyPassives = allyToBuff.GetComponent<PlayerPassives>();
            allySprite.isBuffed = false;
        } 
        if (allyPassives != null)
        {
            allyPassives.SpeedMultiplier /= speedBuff;
            allyPassives.ROFMultiplier /= ROFBuff;
        }
    }

    GameObject FindAlly()
    {
        // Debug.Log("In find ally");
        var allies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject allyReturn = allies[0];
        foreach (GameObject ally in allies){
            if (ally.GetInstanceID() != self.GetInstanceID()){
                allyReturn = ally;
                hasAlly = true;
                return allyReturn;
            }
        }
        hasAlly = false;
        return allyReturn;
    }
}
