using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Shop : MonoBehaviour
{
    private Transform container;
    private Transform shopItemTemplate;

    [SerializeField]
    public List<ShopItemSO> ShopInventory;

    private void Awake()
    {
        container = transform.Find("container");
        shopItemTemplate = container.Find("shopItemTemplate");
        shopItemTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        
        
        int i = 0;
        foreach(ShopItemSO itemData in ShopInventory)
        {
            GameObject prefab = itemData.Prefab;
            string name = itemData.Name;
            int price = itemData.Cost;
            Sprite sprite = prefab.GetComponent<SpriteRenderer>().sprite;
            CreateItemButton(itemData, sprite, name, price, i);
            i++;
        }
    }

    private void CreateItemButton(ShopItemSO itemData, Sprite itemSprite, string itemName, int itemPrice, int positionIndex)
    {
        Transform shopItemTransform = Instantiate(shopItemTemplate, container);
        RectTransform shopItemRectTransform = shopItemTransform.GetComponent<RectTransform>();
        
        Text shopItemText = shopItemTransform.GetComponent<Text>();         //for storge the name and price of item, it is set to be invisible in the scene
        Vector2 position = new Vector2(0,0);                                //Initialize the position of slots
        
        // Debug.Log("Screen size: " + Screen.width + " x " +Screen.height);
        //position for the slots
        if(positionIndex == 0 ){
            position = new Vector2(Screen.width / 7, Screen.height / 30);
        } else if(positionIndex == 1){
            position = new Vector2(- Screen.width / 5 , - Screen.height / 5);
        }else{
            position = new Vector2(Screen.width / 5 ,- Screen.height / 5);
        }
        //Debug.Log("item "+ positionIndex +" position" + position.x +" "+ position.y);
        
        // float shopItemHeight = 90f;
        //shopItemRectTransform.anchoredPosition = new Vector2 (0, shopItemHeight * positionIndex);
        shopItemRectTransform.anchoredPosition = position;

        shopItemTransform.Find("NameText").GetComponent<TextMeshProUGUI>().SetText(itemName);
        shopItemTransform.Find("PriceText").GetComponent<TextMeshProUGUI>().SetText(itemPrice.ToString());
        shopItemTransform.Find("ItemSprite").GetComponent<Image>().sprite = itemSprite;

        //Storge the name and price of item in button's text component, easier to access
        shopItemText.text = (itemName + "\n" + "$ " +itemPrice.ToString()); 

        shopItemTransform.GetComponent<PrefabHolder>().itemData = itemData;
        shopItemTransform.GetComponent<PrefabHolder>().shop = this;
        shopItemTransform.gameObject.SetActive(true);

        
    }

    /*public void TryBuyItem(ShopItemSO itemData)
    {
        
    }*/
}
