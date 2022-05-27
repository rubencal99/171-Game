using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryTrash : MonoBehaviour, IDropHandler
{
    public GameObject p;
    void Start()
    {
        // find player
        p = Player.instance.gameObject;

    }
    public void OnDrop(PointerEventData eventData)
    {
        // call DropItem
        DragDrop _dragdrop = eventData.pointerDrag.GetComponent<DragDrop>();
        CreateDrop(_dragdrop.slotElement.slot);
    }
    void CreateDrop(Slot _slot)
    {
        // instantiates _item.pickup
        Instantiate(_slot.item.pickup, p.transform.position + (Vector3.back*2), Quaternion.identity, null);
        _slot.Clear();
    }
}
