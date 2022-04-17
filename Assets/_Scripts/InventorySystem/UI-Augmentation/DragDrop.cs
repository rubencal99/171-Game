using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    [SerializeField] private Canvas canvas; 
    [SerializeField] public GameObject parent;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    private void Awake() {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public void OnPointerDown(PointerEventData eventData) {
        // Debug.Log("pointer do be down tho");

    }

    public void OnBeginDrag(PointerEventData eventData) {
        //Debug.Log("you have started to drag me"); 
        canvasGroup.alpha = .7f;
        canvasGroup.blocksRaycasts = false;
        transform.SetParent(canvas.transform);
    }
    
    public void OnDrag(PointerEventData eventData) {
        //Debug.Log("OnDrag called on " + parent.GetComponent<InventorySlotElement>().slotIndex);
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData) {
        //Debug.Log("if you love me let me goooooo");
        canvasGroup.alpha = 1.0f;
        canvasGroup.blocksRaycasts = true;
        rectTransform.SetParent(parent.transform);
        rectTransform.anchoredPosition = Vector2.zero;
    }
    public void OnDrop(PointerEventData eventData){
        // 
        Debug.Log("OnDrop called on item");
        if (eventData.pointerDrag.GetComponent<DragDrop>() != null && eventData.pointerDrag != this)
        {
            parent.GetComponent<InventorySlotElement>().inventory.MoveSwapCombine(eventData.pointerDrag.GetComponent<DragDrop>().parent.GetComponent<InventorySlotElement>().slot, parent.GetComponent<InventorySlotElement>().slot);
            parent.GetComponent<InventorySlotElement>().inventory.Print();
        }
    }

    
}
