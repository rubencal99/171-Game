using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryTrash : MonoBehaviour, IDropHandler
{
    public GameObject p;
    public InventoryUIParent UIParent;
    private InventorySoundManager soundManager;
    void Start()
    {
        // find player
        p = Player.instance.gameObject;
        soundManager = UIParent.GetComponent<InventorySoundManager>();

    }
    public void OnDrop(PointerEventData eventData)
    {
        DragDrop _dragdrop = eventData.pointerDrag.GetComponent<DragDrop>();
        soundManager.PlayClickItemSound((float)_dragdrop.slotElement.slot.item.type, 0);
        CreateDrop(_dragdrop.slotElement.slot);
        
    }
    void CreateDrop(Slot _slot)
    {
        // makes an item drop in front of the player and clears the relevant slot
        Instantiate(_slot.item.pickup, p.transform.position + (Vector3.back*2), Quaternion.identity, null);
        _slot.Clear();
    }
}
