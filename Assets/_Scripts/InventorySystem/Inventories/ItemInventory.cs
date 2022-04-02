using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInventory
{
    private List<ItemObject> itemList;

    public TabInventory() {
        itemList = new List<ItemObject>(); 

        Debug.Log("Inventory"); 
    }


}
