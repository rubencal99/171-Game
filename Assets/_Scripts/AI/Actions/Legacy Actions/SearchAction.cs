using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchAction : AIAction
{
    GameObject self;

    void Start(){
        self = this.transform.root.gameObject;
    }

    public override void TakeAction(){
        GameObject allyToBuff = FindAlly();
    }

    GameObject FindAlly()
    {
        var allies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject allyReturn = allies[0];
        foreach (GameObject ally in allies){
            if (ally != self){
                allyReturn = ally;
                break;
            }
        }
        return allyReturn;
    }
}
