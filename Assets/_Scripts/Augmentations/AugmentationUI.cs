using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AugmentationUI : MonoBehaviour
{
    public Transform augmentationUI;
    private Transform container;
    private Transform augmentationTemplate;

    [SerializeField]
    public List<AugmentationSO> AugmentationInventory;
    public GameObject Player;

    private void Awake()
    {
        augmentationUI = transform.Find("InventoryUI");
        container = augmentationUI.transform.Find("container");
        augmentationTemplate = container.Find("AugmentationTemplate");
        augmentationTemplate.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        int i = 0;
        foreach(AugmentationSO aug in AugmentationInventory)
        {
            GameObject prefab = aug.Prefab;
            string name = aug.Name;
            int price = aug.Cost;
            Sprite sprite = prefab.GetComponent<SpriteRenderer>().sprite;
            CreateItemButton(aug, sprite, name, price, i);
            i++;
        }
        CloseAugmentationMenu();
    }

    public void CloseAugmentationMenu()
    {
        augmentationUI.gameObject.SetActive(false);
    }

    public void OpenAugmentationMenu()
    {
        augmentationUI.gameObject.SetActive(true);
    }

    public void ToggleAugmentationMenu()
    {
        if (augmentationUI.gameObject.activeSelf)
        {
            CloseAugmentationMenu();
        }
        else
        {
            OpenAugmentationMenu();
        }
    }


    private void CreateItemButton(AugmentationSO augData, Sprite augSprite, string augName, int augPrice, int positionIndex)
    {
        Transform shopItemTransform = Instantiate(augmentationTemplate, container);
        RectTransform shopItemRectTransform = shopItemTransform.GetComponent<RectTransform>();


        float shopItemHeight = 90f;
        shopItemRectTransform.anchoredPosition = new Vector2(0, -shopItemHeight * positionIndex);

        shopItemTransform.Find("NameText").GetComponent<TextMeshProUGUI>().SetText(augName);
        shopItemTransform.Find("PriceText").GetComponent<TextMeshProUGUI>().SetText(augPrice.ToString());

        shopItemTransform.Find("ItemSprite").GetComponent<Image>().sprite = augSprite;
        shopItemTransform.GetComponent<PrefabHolder>().augData = augData;
        // shopItemTransform.GetComponent<PrefabHolder>().shop = this;
        shopItemTransform.gameObject.SetActive(true);

        
    }
}
