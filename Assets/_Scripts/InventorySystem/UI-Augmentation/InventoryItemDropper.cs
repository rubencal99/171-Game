using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryItemDropper : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        DragDrop _dragdrop = eventData.pointerDrag.GetComponent<DragDrop>();
        Debug.Log("OnDrop called on background");
        if (_dragdrop)
        {
            //soundManager.PlayClickItemSound(_dragdrop.slotElement.slot.item.type);
            _dragdrop.slotElement.slot.DropItem(Vector3.zero);
        }
    }
}
