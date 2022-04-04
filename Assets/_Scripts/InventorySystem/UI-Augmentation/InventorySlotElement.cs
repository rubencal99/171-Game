using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlotElement : MonoBehaviour
{
    [SerializeField]
    private Sprite background;
    private Sprite current;
    public Image image;
    public ItemInventory inventory;
    [SerializeField]
    private int slotIndex;
    private InventorySlot slot;
    
    void Awake()
    {
        slot = inventory.Container[slotIndex];
        image = gameObject.GetComponent<Image>();

        current = background;
        image.sprite = current;
    }

    public void Update()
    {
        // if slot.item changes, update current to slot.item.icon
        if (slot.item != null && current == background)
        {
            current = slot.item.icon;
            image.sprite = current;
        }
        else if (slot.item == null && current != background)
        {
            current = background;
            image.sprite = current;
        }
    }
    
}

public class WeaponSlotElement : MonoBehaviour
{
    // 
}

public class AugSlotElement : MonoBehaviour
{
    // 
}