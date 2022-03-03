using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabInventory
{
    private List<ItemObject> itemList;

    public TabInventory() {
        itemList = new List<ItemObject>(); 

        Debug.Log("Inventory"); 
    }


}
