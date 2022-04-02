using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlotElement : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData) {
        //Debug.Log("put me in pUT ME THE FUCK IN");
        if (eventData.pointerDrag != null) {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        }

    }
}
