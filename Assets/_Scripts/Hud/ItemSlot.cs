using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject tooltip;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // tooltip.SetActive(true);
        tooltip.SetActive(true);
        // ToolTip.Show();
        // Debug.Log("Show Floating window");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.SetActive(false);
        // ToolTip.Hide();
        // Debug.Log("Hide Floating window");
    }



}
