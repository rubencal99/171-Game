using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PrefabHolder : MonoBehaviour
{
    public ShopItemSO itemData;
    public UI_Shop shop;
    public PlayerWeapon weaponParent;

    public AugmentationSO augData;
    public AugmentationUI augmentationUI;
    public GameObject Player;
    public GameObject MoneyReducePopUp;
    public Text moneyreduce;

    public void Start()
    {
        Player = GameObject.FindWithTag("Player");
        MoneyReducePopUp = GameObject.FindWithTag("MoneyReduceText");
    }

    public void TryBuyAugmentation()
    {
        GameObject prefab = augData.Prefab;
        Player playerInfo = Player.GetComponent<Player>();
        if (playerInfo.CanPurchase(augData.Cost) && PlayerAugmentations.AugmentationList[augData.name] == false)
        {
            // Debug.Log(prefab.name + " = " + "true");
            playerInfo.Purchase(augData.Cost);

            //money reduce display
            MoneyReducePopUp.SetActive(true);
            var reduce = itemData.Cost;
            Debug.Log("cost is "+ reduce.ToString());
            moneyreduce.text = "-"+ reduce.ToString();

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
            //if (prefab.GetComponent<Gun>())
            //{
            Debug.Log("Purchased item");
            //weaponParent = FindObjectOfType<PlayerWeapon>();

            //money reduce
            //MoneyReducePopUp.SetActive(true);
            var reduce = itemData.Cost;
            Debug.Log("cost is "+ reduce.ToString());
            moneyreduce.text = "-"+ reduce.ToString();

            //

            var pos = FindSpawnPosition();
            var weapon = Instantiate(prefab, pos, Quaternion.identity);
            weapon.transform.parent = transform.root;
            
            playerInfo.Purchase(itemData.Cost);

            //Money reduce pop up
            // moneyreduce = itemData.Cost;

            // weapon.transform.position = weaponParent.transform.position;

            //weapon.transform.localPosition = new Vector3(0, -0.25F, 0);
            //popup.SetText(itemData.Name);
            //popup.ShowText();
            //weapon.SetActive(false);

            /* 
            Vector3 p = weapon.transform.position;
            p.x = 0.574f;
            weapon.transform.position = p;
            
                */
            //}
            Destroy(gameObject);
        }
        else if (!playerInfo.CanPurchase(itemData.Cost))
        {
          
            popup.SetAltText("Can't afford!");
            popup.ShowText();
            Debug.Log("Cannot afford " + itemData.name);
        }
    }

    Vector3 FindSpawnPosition()
    {
        var pos = transform.parent.parent.parent.transform.position + Vector3.right * 5;
        var offset = new Vector3(UnityEngine.Random.Range(-1, 1), 1, UnityEngine.Random.Range(-1, 1));
        return pos + offset;
    }
}
