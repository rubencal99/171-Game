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

       int hpUP = 0;
       float moveSpeedUp = 0.0f;

       foreach(PassiveItem item in items) {

           if(!item.applied)
                switch(item.type)
                {
                    case ItemType.HP:
                            Debug.Log("HP passive upgrade");
                            hpUP++;
                            break;
                    
                    case ItemType.MoveSpeed:
                        Debug.Log("movespeed passive upgrade");
                        moveSpeedUp += item.intensity;
                        break;
                }

    }

        if(hpUP != 0) {
        //player.setMaxHp( player.MaxHealth + hpUP);
        player.Heal(hpUP);
        }

        if(moveSpeedUp != 0.0f) {
            float percentIncrease = moveSpeedUp / 100.0f;
            percentIncrease *= stats.SpeedMultiplier;
            

            stats.SpeedMultiplier += percentIncrease;
            Debug.Log("speed multiplier is" + stats.SpeedMultiplier); 
        }

   }


}
