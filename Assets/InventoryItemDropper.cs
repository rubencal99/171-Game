using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryItemDropper : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop called on background");
        if (eventData.pointerDrag.GetComponent<DragDrop>())
        {
            DragDrop _dragdrop = eventData.pointerDrag.GetComponent<DragDrop>();
            //soundManager.PlayClickItemSound(_dragdrop.slotElement.slot.item.type);
            _dragdrop.slotElement.slot.DropItem(Vector3.zero);
        }
    }
}
