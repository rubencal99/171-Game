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
    public SpriteRenderer ShopKeeper;
    public bool inShop;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Player.GetComponent<PlayerInput>().ShopKeeper = this;
        ShopKeeper = transform.Find("avatar").GetComponent<SpriteRenderer>();
        ShopKeeper.color = new Color(175, 175, 175, 1);
        //Debug.Log("Shopkeeper color on Start: " + ShopKeeper.color);
        // ShopUI = transform.Find("UI_Shop");
        ShopUI = transform.Find("Canvas-ShopUI");
        CloseShop();
        inShop = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckDistance();
        //Debug.Log("Shopkeeper color: " + ShopKeeper.color);
        if (inShop == true){
            if (Input.GetKeyDown(KeyCode.Escape)){
                // Debug.Log("esc press");
                CloseShop();   
            }
        }
    }


    public void HighlightShopKeeper()
    {
        ShopKeeper.color = new Color(225/255f, 225/255f, 225/255f, 1);
    }
    public void UnHighlightShopKeeper()
    {
        ShopKeeper.color = new Color(175/255f, 175/255f, 175/255f, 1);
    }

    // This function when invoked will display the Shop UI
    public void DisplayShop()
    {
        ShopUI.gameObject.SetActive(true);
        inShop = true;
    }

    // This function when invoked disables the Shop UI
    public void CloseShop()
    {
        ShopUI.gameObject.SetActive(false);
        StartCoroutine(closingShop());
    }

    IEnumerator closingShop()
    {
        yield return new WaitForSeconds(0.01f);
        Debug.Log("After 0.01s");
        inShop = false;
    }

    public void CheckDistance()
    {
        if(Vector2.Distance(Player.transform.position, transform.position) <= ShopDistance)
        {
            // Debug.Log("In Distance of Shopkeeper");
            if(inDistance == false)
            {
                inDistance = true;
                HighlightShopKeeper();
            }
        }
        else
        {
            // Debug.Log("Not in Distance of Shopkeeper");
            if(inDistance == true)
            {
                inDistance = false;
                UnHighlightShopKeeper();
            }
        }
    }
}
