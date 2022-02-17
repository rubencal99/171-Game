using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public List<ShopItemSO> ItemsForSale;
    public Transform ShopUI;
    public float ShopDistance;
    public bool inDistance = false;
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        ShopUI = transform.Find("UI_Shop");
        CloseShop();
    }

    // Update is called once per frame
    void Update()
    {
        CheckDistance();
    }

    // This function when invoked will display the Shop UI
    public void DisplayShop()
    {
        ShopUI.gameObject.SetActive(true);
    }

    // This function when invoked disables the Shop UI
    public void CloseShop()
    {
        ShopUI.gameObject.SetActive(false);
    }

    public void CheckDistance()
    {
        if(Vector2.Distance(Player.transform.position, transform.position) <= ShopDistance)
        {
            inDistance = true;
            DisplayShop();
        }
        else
        {
            inDistance = false;
            CloseShop();
        }
    }
}
