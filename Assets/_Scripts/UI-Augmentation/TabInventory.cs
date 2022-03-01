using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabInventory
{
    private List<Item> itemList;

    public TabInventory() {
        itemList = new List<Item>(); 

        Debug.Log("Inventory"); 
    }


}
