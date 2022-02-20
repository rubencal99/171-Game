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

    public void TryBuyAugmentation()
    {
        GameObject prefab = augData.Prefab;
        // Debug.Log(prefab.name + " = " + "true");
        PlayerAugmentations.AugmentationList[augData.name] = true;
        PlayerAugmentations.PrintDictionary();
    }

    public void TryBuyItem()
    {
        GameObject prefab = itemData.Prefab;
        if (prefab.GetComponent<Gun>())
        {
            weaponParent = FindObjectOfType<PlayerWeapon>();
            var weapon = Instantiate(prefab, weaponParent.transform.position, Quaternion.identity);
            weapon.transform.parent = weaponParent.transform;
            // weapon.transform.position = weaponParent.transform.position;

            weapon.transform.localPosition = new Vector3(0.574f, 0, 0);
            weapon.SetActive(false);

            /* 
            Vector3 p = weapon.transform.position;
            p.x = 0.574f;
            weapon.transform.position = p;
            
            */
        }
    }
}
