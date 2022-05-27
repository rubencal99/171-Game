using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public static Shop instance;
    public List<ShopItemSO> ItemsForSale;
    public Transform ShopUI;
    public float ShopDistance;
    public bool inDistance = false;
    public GameObject player;
    public SpriteRenderer ShopKeeper;
    public bool inShop;
    public GameObject Key;

    public float shopTimer = 0.5f;
    public float shopTime = 0.5f;
    public bool canShop = true;

    void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        player = Player.instance.gameObject;
        player.GetComponent<PlayerInput>().ShopKeeper = this;
        ShopKeeper = transform.Find("avatar").GetComponent<SpriteRenderer>();
        ShopKeeper.color = new Color(175, 175, 175, 1);
        //Debug.Log("Shopkeeper color on Start: " + ShopKeeper.color);
        // ShopUI = transform.Find("UI_Shop");
        ShopUI = transform.Find("Canvas-ShopUI");
        CloseShop();
        inShop = false;

        RoomNode spawnRoom;
        if(transform.parent != null)
        {
            spawnRoom = transform.parent.GetComponent<RoomNode>();
            transform.position = new Vector3(spawnRoom.roomCenter.x, 0, spawnRoom.roomCenter.y);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        //CheckDistance();
        //Debug.Log("Shopkeeper color: " + ShopKeeper.color);
        if (inShop == true){
            if (Input.GetKeyDown(KeyCode.Escape)){
                // Debug.Log("esc press");
                CloseShop();   
            }
        }

        if(!canShop)
        {
            shopTimer -= Time.deltaTime;
            if(shopTimer <= 0)
            {
                canShop = true;
                shopTimer = shopTime;
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
        if(!canShop)
        {
            return;
        }
        ShopUI.gameObject.SetActive(true);
        inShop = true;
    }

    // This function when invoked disables the Shop UI
    public void CloseShop()
    {
        PlayerStateManager.instance.InteractKeyPressed = true;
        ShopUI.gameObject.SetActive(false);
        PlayerStateManager sm = player.GetComponent<PlayerStateManager>();
        sm.SwitchState(sm.RunGunState);
        StartCoroutine(closingShop());
        canShop = false;
    }

    IEnumerator closingShop()
    {
        yield return new WaitForSeconds(0.01f);
        // Debug.Log("After 0.01s");
        inShop = false;
    }

    public void CheckDistance()
    {
        if(Vector2.Distance(player.transform.position, transform.position) <= ShopDistance)
        {
            // Debug.Log("In Distance of Shopkeeper");
            if(inDistance == false)
            {
                inDistance = true;
                HighlightShopKeeper();
                var popUp = Key.GetComponent<SpriteRenderer>();
                popUp.enabled = !popUp.enabled;
            }
        }
        else
        {
            // Debug.Log("Not in Distance of Shopkeeper");
            if(inDistance == true)
            {
                inDistance = false;
                UnHighlightShopKeeper();
                var popUp = Key.GetComponent<SpriteRenderer>();
                popUp.enabled = !popUp.enabled;
            }
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        
        if(collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("In shop trigger enter");
            inDistance = true;
            Key.SetActive(true);
        }
    }

    void OnTriggerExit(Collider collider)
    {
        
        if(collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("In shop trigger exit");  
            inDistance = false;
            Key.SetActive(false);
        }
    }

    void OnCollisionEnter(Collision collider)
    {
        if(collider.gameObject.layer == LayerMask.NameToLayer("player"))
        {
            inDistance = true;
            Key.SetActive(true);
        }
    }

    void OnCollisionExit(Collision collider)
    {
        if(collider.gameObject.layer == LayerMask.NameToLayer("player"))
        {
            inDistance = false;
            Key.SetActive(false);
        }
    }
}
