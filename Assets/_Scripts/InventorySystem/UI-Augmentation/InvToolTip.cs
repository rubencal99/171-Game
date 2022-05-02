using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class InvToolTip : MonoBehaviour, IEventSystemHandler
{
    [SerializeField] private Canvas canvas; 
    public RectTransform rectTransform;
    public TMP_Text nameText;
    public TMP_Text typeText;
    public TMP_Text descriptionText;

    
    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        // we need to hide it here rather than in the hierarchy, otherwise DragDrop can't find it
        HideToolTip();
    }

    public void ShowToolTip(ItemObject _item)
    {
        nameText.text = _item.Name;
            if (_item.GetType() == typeof(WeaponItemSO)) 
            {
                typeText.text = "Weapon - " + ((WeaponType)_item.itemType).ToString(); 
            }
            else if (_item.GetType() == typeof(AugmentationItemSO)) 
            {
                typeText.text = "Augmentation - " + ((AugType)_item.itemType).ToString(); 
            }
            else
            {
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
        rectTransform.position = position;
    }
    
}
