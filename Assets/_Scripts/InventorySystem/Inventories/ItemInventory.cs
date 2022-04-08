using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class ItemInventory : ScriptableObject
{
    public  List<InventorySlot> Container = new List<InventorySlot>();
    public  List<WeaponSlot> WContainer = new List<WeaponSlot>();
    public  List<AugSlot> AContainer = new List<AugSlot>();
    public  List<InventorySlot> MContainer = new List<InventorySlot>();

    public void MoveSwapCombine(Slot _fromslot, Slot _toslot)
    {
        // facilitates the movement of items between slots
        // if items are the same or _toslot is empty, just add item and amount to _toslot
        // if not, swap item and amount data
        if (_fromslot.item == _toslot.item || _toslot.item == null)
        {
            _toslot.item = _fromslot.item;
            _toslot.AddAmount(_fromslot.amount);
            _fromslot.Clear();
        }
        else
        {
            ItemObject toitem = _toslot.item;
            int toamount = _toslot.amount;
            ItemObject fromitem = _fromslot.item;
            int fromamount = _fromslot.amount;
            
            _toslot.Clear();
            _fromslot.Clear();

            _fromslot.AddItemToSlot(toitem, toamount);
            _toslot.AddItemToSlot(fromitem, fromamount);
            
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
                    slot.AddItemToSlot(_item, _amount);
                    hasItem = true;
                    Debug.Log("item is now in inventory");
                    Print();
                    break;
                }
            }
        }
    }

    public void ClearInventory()
    {
        foreach (InventorySlot slot in Container)
        {
            slot.Clear();
        }
        foreach (AugSlot slot in AContainer)
        {
            slot.Clear();
        }
        foreach (WeaponSlot slot in WContainer)
        {
            slot.Clear();
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

// HERE BE SLOTS, LOTS AND LOTS

[System.Serializable]
public class InventorySlot : Slot
{
    //public ItemObject item;
    public InventorySlot(ItemObject _item, int _amount)
    {
        item = _item;
        amount = _amount;
    }
    public void Awake()
    {
        allows = null;
    }
}
[System.Serializable]
public class WeaponSlot : Slot
{
    //public WeaponItemSO item;
    public WeaponType slotType;
    public WeaponSlot(ItemObject _item, int _amount)
    {
        item = _item;
        amount = _amount;

    }
    public void Awake()
    {
        allows = typeof(WeaponItemSO);
    }
}
[System.Serializable]
public class AugSlot : Slot
{
    //public AugmentationItemSO item;
    public AugType slotType;
    public AugSlot(ItemObject _item, int _amount)
    {
        item = _item;
        amount = _amount;

    }
    public void Awake()
    {
        allows = typeof(AugmentationItemSO);
    }
    
}

public abstract class Slot
{
    public ItemObject item;
    public int amount;
    public System.Type allows;
    public void AddAmount(int value)
    {
        amount += value;
    }

    public void AddItemToSlot(ItemObject _item, int _amount)
    {
        if (_item.GetType() == allows || allows == null)
        {
            // does the thing
            if (item == null)
            {
                item = _item;
                AddAmount(_amount);
            }
            else if (item == _item && item.stackable)
            {
                AddAmount(_amount);
            }
            else 
            {
                Debug.Log("There's already an item in this slot");
            }
        }
        else 
        { 
            // does not do the thing
            Debug.Log("Item Type " + _item.GetType() + " not accepted");
        }
        
        
    }

    public void Clear()
    {
        // clears the slot
        amount = 0;
        item = null;
    }
}

