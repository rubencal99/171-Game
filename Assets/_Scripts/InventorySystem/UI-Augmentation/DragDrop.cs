using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    [SerializeField] private Canvas canvas; 
    [SerializeField] public GameObject parent;
    public InventorySoundManager soundManager;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private InventorySlotElement slotElement;

    private void Awake() {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        slotElement = parent.GetComponent<InventorySlotElement>();
        soundManager = canvas.GetComponent<InventorySoundManager>();
    }
    public void OnPointerDown(PointerEventData eventData) {
        // Debug.Log("pointer do be down tho");
        soundManager.PlayClickItemSound(slotElement.slot.item.type);

    }

    public void OnBeginDrag(PointerEventData eventData) {
        //Debug.Log("you have started to drag me"); 
        canvasGroup.alpha = .7f;
        canvasGroup.blocksRaycasts = false;
        transform.SetParent(canvas.transform);
    }
    
    public void OnDrag(PointerEventData eventData) {
        //Debug.Log("OnDrag called on " + slotElement.slotIndex);
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
            slotElement.inventory.MoveSwapCombine(eventData.pointerDrag.GetComponent<DragDrop>().slotElement.slot, slotElement.slot);
            soundManager.PlayClickItemSound(slotElement.slot.item.type);
            slotElement.inventory.Print();
        }
    }

    
}
