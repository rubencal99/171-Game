using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlotElement : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private Sprite defaultBG;
    [SerializeField]
    private Sprite hasItemBG;
    private Sprite current;
    public Image background;
    public Image itemDisplay;
    public ItemInventory inventory;
    [SerializeField]
    private int slotIndex;
    private Slot slot;
    
    public void Awake()
    {
        slot = inventory.Container[slotIndex];
        //background = gameObject.GetComponentInChildren<Image>();

        current = null;
        background.sprite = defaultBG;
        itemDisplay.sprite = current;
    }

    public void OnPointerDown()
    {
        // Moves item from this slot to MContainer[0]
    }

    public void OnPointerUp()
    {
        // Moves item from MContainer[0] to this slot
    }

    public void Update()
    {
        // if slot.item changes, update current to slot.item.icon
        if (slot.item != null && itemDisplay.sprite == null)
        {
            current = slot.item.icon;
            itemDisplay.sprite = current;
            itemDisplay.color = new Color (255, 255, 255, 1);

            background.sprite = hasItemBG;
        }
        else if (slot.item == null && itemDisplay.sprite != null)
        {
            current = null;
            itemDisplay.sprite = current;
            itemDisplay.color = new Color (255, 255, 255, 0);

            background.sprite = defaultBG;
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