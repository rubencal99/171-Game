using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class ItemInventory : ScriptableObject
{
    public  List<InventorySlot> Container = new List<InventorySlot>();
    public  List<WeaponSlot> WContainer = new List<WeaponSlot>();
    public  List<AugSlot> AContainer = new List<AugSlot>();
    
    public void AddItemToSlot(ItemObject _item, int _amount, Slot _slot)
    {
        if (_slot.item == null)
        {
            _slot.item = _item;
            _slot.AddAmount(_amount);
        }
        else if (_slot.item == _item)
        {
            _slot.AddAmount(_amount);
        }
        else 
        {
            // swap items with cached slot
        }
        
    }
    public void AddItemToInventory(ItemObject _item, int _amount)
    {
        // check if item is already in inventory
        // assume we don't
        bool hasItem = false;
        foreach (InventorySlot slot in Container)
        {
            if(slot.item == _item)
            {
                if (slot.item.stackable)
                {
                    slot.AddAmount(_amount);
                    hasItem = true;
                    //Debug.Log("stacking items");
                    break;
                }
                else
                {
                    hasItem = true;
                    break;
                }
            }
        }
        // if after all that business still no item, put it in a new slot
        if (!hasItem)
        {
            foreach (InventorySlot slot in Container)
            {
                if(slot.item == null)
                {
                    slot.item = _item;
                    slot.AddAmount(_amount);
                    hasItem = true;
                    //Debug.Log("item is now in inventory");
                    //Print();
                    break;
                }
            }
        }
    }

    public void MoveItemToSlot(InventorySlot fromSlot, InventorySlot toSlot)
    {
        //
    }

    public void ClearInventory()
    {
        foreach (InventorySlot slot in Container)
        {
            slot.item = null;
            slot.amount = 0;
        }
        foreach (AugSlot slot in AContainer)
        {
            slot.item = null;
            slot.amount = 0;
        }
        foreach (WeaponSlot slot in WContainer)
        {
            slot.item = null;
            slot.amount = 0;
        }
        Debug.Log("Inventory Cleared");
    }

    public void Print()
    {
        for (int i = 0; i < Container.Count; i++)
        {
            Debug.Log(i + ": " + Container[i].item + ": " + Container[i].amount);
        }
    }
}

[System.Serializable]
public class InventorySlot : Slot
{
    //public ItemObject item;
    public InventorySlot(ItemObject _item, int _amount)
    {
        item = _item;
        amount = _amount;
    }
}
[System.Serializable]
public class WeaponSlot : Slot
{
    //public WeaponItemSO item;
    public WeaponType slotType;
    public WeaponSlot(WeaponItemSO _item, int _amount)
    {
        item = _item;
        amount = _amount;

    }
}
[System.Serializable]
public class AugSlot : Slot
{
    //public AugmentationItemSO item;
    public AugType slotType;
    public AugSlot(AugmentationItemSO _item, int _amount)
    {
        item = _item;
        amount = _amount;

    }
    
}

public class Slot
{
    public ItemObject item;
    public int amount;
    public void AddAmount(int value)
    {
        amount += value;
    }
}

