using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject tooltip;
    public GameObject Item;
    public Text ItemText;
    public Text tooltipText;
    public Transform tooltipContent; 
    public Text tooltipContentText;
    
    
    // Start is called before the first frame update
    void Start()
    {
        Item = this.transform.parent.gameObject;
        tooltipText = tooltip.GetComponent<Text>();
        ItemText = Item.GetComponent<Text>();
        tooltipContent = tooltip.transform.Find("Content");
        tooltipContentText = tooltipContent.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
        tooltip.SetActive(true);            //Display floating window

        ItemText = Item.GetComponent<Text>(); //Change the Text in Floating Window
        tooltipText.text = ItemText.text;
        tooltipContentText.text = ItemText.text;

        // ToolTip.Show();
        // Debug.Log("Show Floating window");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.SetActive(false);           //Hide the floating window
        // ToolTip.Hide();
        // Debug.Log("Hide Floating window");
    }



}
