using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanicAction : AIAction
{
    GameObject self;
     void Start(){
        self = this.transform.root.gameObject;
    }

    public override void TakeAction(){
         self.GetComponentInChildren<AgentAnimations>().SetPanicAnimation(true);
    }
}
