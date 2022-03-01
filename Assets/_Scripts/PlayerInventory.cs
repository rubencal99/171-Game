using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 
public class PlayerInventory : MonoBehaviour
{
    public List<PassiveItem> items;
    public PlayerPassives stats;

    public Player player;

    
    void Start()
    {
        stats = this.transform.parent.GetComponent<PlayerPassives>();
        player = this.transform.parent.GetComponent<Player>();
    }

    public void applyEffects() {

//        int hpUP = 0;
//        float moveSpeedUp = 0.0f;

//        foreach(PassiveItem item in items) {

           if(!item.applied)
                switch(item.type)
                {
                    case PassiveType.HP:
                            Debug.Log("HP passive upgrade");
                            hpUP++;
                            break;
                    
                    case PassiveType.MoveSpeed:
                        Debug.Log("movespeed passive upgrade");
                        moveSpeedUp += item.intensity;
                        break;
                }

    }


}
