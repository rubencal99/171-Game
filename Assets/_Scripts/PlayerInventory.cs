using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 
public class PlayerInventory : MonoBehaviour
{
    public List<PassiveItem> items;
    public PlayerPassives stats;

    public Player player;
    public static PlayerInventory instance;
    public ItemInventory itemInventory;

    void Awake()
    {
        instance = this;
    }
    
    void Start()
    {
        stats = this.transform.parent.GetComponent<PlayerPassives>();
        player = this.transform.parent.GetComponent<Player>();
        CheckInventory();
    }

    public void CheckInventory()
    {
        foreach(InventorySlot slot in itemInventory.Container)
        {
            if(slot.item != null)
            {
                InstantiateItem(slot.item);
                
            }
        }
    }

    public void InstantiateItem(ItemObject slotItem)
    {
        GameObject item = Instantiate(slotItem.prefab);
        item.transform.parent = GameObject.Find("InventoryParent").transform;
        item.transform.localPosition = new Vector3(0f, -0.25f, 0f);
        item.transform.position = Vector3.zero;
        item.transform.localRotation = Quaternion.identity;
        item.SetActive(false);
        slotItem.prefabClone = item;
        //Player.instance.inventory.AddItemToInventory(slotItem, 1);
    }

    public void applyEffects() {

//        int hpUP = 0;
//        float moveSpeedUp = 0.0f;

//        foreach(PassiveItem item in items) {

        //    if(!item.applied)
        //         switch(item.type)
        //         {
        //             case PassiveType.HP:
        //                     Debug.Log("HP passive upgrade");
        //                     hpUP++;
        //                     break;
                    
        //             case PassiveType.MoveSpeed:
        //                 Debug.Log("movespeed passive upgrade");
        //                 moveSpeedUp += item.intensity;
        //                 break;
        //         }

    }


}
