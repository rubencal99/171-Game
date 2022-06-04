using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Canvas canvas; 
    [SerializeField] public GameObject parent;
    public InventoryUIParent UIParent;
    public InventorySoundManager soundManager;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    public InventorySlotElement slotElement;
    public InvToolTip toolTip;

    private void Awake() {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        slotElement = parent.GetComponent<InventorySlotElement>();
        soundManager = canvas.GetComponent<InventorySoundManager>();
        UIParent = canvas.GetComponent<InventoryUIParent>();
        toolTip = canvas.GetComponentInChildren<InvToolTip>();

    }
    public void OnPointerDown(PointerEventData eventData) {
        // Debug.Log("pointer do be down tho");
        

    }

    public void OnBeginDrag(PointerEventData eventData) {
        //Debug.Log("you have started to drag me"); 
        if (slotElement.slot.item != null)
        {
            soundManager.PlayClickItemSound((float)slotElement.slot.item.type, 1);
            canvasGroup.alpha = .7f;
            canvasGroup.blocksRaycasts = false;
            transform.SetParent(canvas.transform);
        }
        
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
        DragDrop droppedItem = eventData.pointerDrag.GetComponent<DragDrop>();
        if (droppedItem == null || eventData.pointerDrag == this)
        {
            return;
        }
        else
        {
            slotElement.inventory.MoveSwapCombine(droppedItem.slotElement.slot, slotElement.slot);
            //soundManager.PlayClickItemSound((float)slotElement.slot.item.type, 0);
            if (
                slotElement.inventory.MoveQuery(droppedItem.slotElement.slot, slotElement.slot) ||
                slotElement.inventory.CombineQuery(droppedItem.slotElement.slot, slotElement.slot) ||
                slotElement.inventory.SwapQuery(droppedItem.slotElement.slot, slotElement.slot)){
                    soundManager.PlayClickItemSound((float)slotElement.slot.item.type, 0);
                    Debug.Log("play clickitemsound for " + slotElement.slot.item.type);
                }
            else{
                soundManager.PlayClickItemSound(4.0f, 0);
                Debug.Log("play clickitemsound failure");
            }
            
            slotElement.inventory.Print();
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        soundManager.PlayHoverSound();
        // update InvToolTip
        toolTip.ShowToolTip(slotElement.slot.item);
        //Debug.Log("OnPointerEnter " + slotElement.slot.item); 
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        // Update InvToolTip to null
        toolTip.HideToolTip();
        //Debug.Log("OnPointerExit " + slotElement.slot.item); 
    }

    
}
