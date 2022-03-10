using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PrefabHolder : MonoBehaviour
{
    public ShopItemSO itemData;
    public UI_Shop shop;
    public PlayerWeapon weaponParent;

    public AugmentationSO augData;
    public AugmentationUI augmentationUI;
    public GameObject Player;

    public void Start()
    {
        Player = GameObject.FindWithTag("Player");
    }

    public void TryBuyAugmentation()
    {
        GameObject prefab = augData.Prefab;
        Player playerInfo = Player.GetComponent<Player>();
        if (playerInfo.CanPurchase(augData.Cost) && PlayerAugmentations.AugmentationList[augData.name] == false)
        {
            // Debug.Log(prefab.name + " = " + "true");
            playerInfo.Purchase(augData.Cost);
            PlayerAugmentations.AugmentationList[augData.name] = true;
            PlayerAugmentations.PrintDictionary();
        }
        else if (PlayerAugmentations.AugmentationList[augData.name] == true)
        {
            Debug.Log("Already purchased " + augData.name);
        }
        else if (!playerInfo.CanPurchase(augData.Cost))
        {
            Debug.Log("Cannot afford " + augData.name);
        }
    }

    public void TryBuyItem()
    {
        GameObject prefab = itemData.Prefab;
        Player playerInfo = Player.GetComponent<Player>();
         popup popup = FindObjectOfType<popup>();
        if (playerInfo.CanPurchase(itemData.Cost))
        {
            if (prefab.GetComponent<Gun>())
            {
                weaponParent = FindObjectOfType<PlayerWeapon>();
                var weapon = Instantiate(prefab, weaponParent.transform.position, Quaternion.identity);
                weapon.transform.parent = weaponParent.transform;
                
                playerInfo.Purchase(itemData.Cost);
                // weapon.transform.position = weaponParent.transform.position;
                 popup.SetText(itemData.Name);
                popup.ShowText();
                weapon.transform.localPosition = new Vector3(0.574f, 0, 0);
                weapon.SetActive(false);

                /* 
                Vector3 p = weapon.transform.position;
                p.x = 0.574f;
                weapon.transform.position = p;
                
                */
            }
        }
        else if (!playerInfo.CanPurchase(itemData.Cost))
        {
          
            popup.SetAltText("Can't afford!");
            popup.ShowText();
            Debug.Log("Cannot afford " + itemData.name);
        }
    }
}
