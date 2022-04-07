using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanicAction : AIAction
{
    GameObject self;
     void Start(){
        self = transform.parent.parent.gameObject;
    }

    public override void TakeAction(){
         self.GetComponentInChildren<AgentAnimations>().SetPanicAnimation(true);
         //Debug.Log("Player gameObject: " + Player.instance.gameObject);
         enemyBrain.Target = Player.instance.gameObject;
    }
}
