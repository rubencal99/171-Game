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


        float shopItemHeight = 90f;
        shopItemRectTransform.anchoredPosition = new Vector2(0, -shopItemHeight * positionIndex);

        shopItemTransform.Find("NameText").GetComponent<TextMeshProUGUI>().SetText(itemName);
        shopItemTransform.Find("PriceText").GetComponent<TextMeshProUGUI>().SetText(itemPrice.ToString());

        shopItemTransform.Find("ItemSprite").GetComponent<Image>().sprite = itemSprite;
        shopItemTransform.GetComponent<PrefabHolder>().itemData = itemData;
        shopItemTransform.GetComponent<PrefabHolder>().shop = this;
        shopItemTransform.gameObject.SetActive(true);

        
    }

    /*public void TryBuyItem(ShopItemSO itemData)
    {
        
    }*/
}
