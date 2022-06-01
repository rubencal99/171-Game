using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems; 

public class InvToolTip : MonoBehaviour, IEventSystemHandler
{
    [SerializeField] private GameObject canvas;
    public RectTransform rectTransform;
    public TMP_Text nameText;
    public TMP_Text typeText;
    public TMP_Text descriptionText;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    void Start()
    {
        HideToolTip();
    }

    public void ShowToolTip(ItemObject _item)
    {
        // set name, type, and description fields to the item's corresponding name, type, and description
        nameText.text = _item.Name;
        if (_item.type == ItemType.Weapon){
            typeText.text = "Weapon - " + ((WeaponType)_item.itemType).ToString();
        }
        else if (_item.type == ItemType.Augmentation){
            typeText.text = "Augmentation - " + ((AugType)_item.itemType).ToString();
        }
        else{
            typeText.text = "Item";
        }
        descriptionText.text = _item.description;

        gameObject.SetActive(true);
    }
    public void HideToolTip()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        Vector2 position = Input.mousePosition;

        float pivotX = position.x / Screen.width;
        float pivotY = position.y / Screen.height;

        rectTransform.pivot = new Vector2(pivotX, pivotY);
        rectTransform.position = position;
    }
}
