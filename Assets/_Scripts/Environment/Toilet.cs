using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Toilet : MonoBehaviour
{
    public GameObject ToiletUI;
    public GameObject player;
    public float toiletRange;
    public bool inRange = false;
    public bool inToilet = false;
    public GameObject Key;
    public Button LeftButton;
    public Button RightButton;
    public List<GameObject> weaponList;
    public List<GameObject> augmentList;
    public GameObject currencySpawn;
    public GameObject LeftSpawnItem;
    public GameObject RightSpawnItem;
    public bool used;
    public float displayTime = 0.3f;
    //public UnityEvent EngageLeftButton{get; set;}
    //public UnityEvent EngageRightButton{get; set;}

    // Start is called before the first frame update
    void Start()
    {
        used = false;
        player = Player.instance.gameObject;
        ToiletUI.transform.localScale = Vector3.one;
        ToiletUI.SetActive(false);
        ChooseItems();
    }

    // Update is called once per frame
    void Update()
    {
        CheckDistance();
        //Debug.Log("Shopkeeper color: " + ShopKeeper.color);
        if (inToilet == true){
            if (Input.GetKeyDown(KeyCode.Escape)){
                // Debug.Log("esc press");
                CloseToilet();   
            }
        }
        GetInteractInput();
    }

    void ChooseItems()
    {
        /*if(weaponList.Count == 0)
        {
            if(LeftSpawnItem != null)
            {
                LeftButton.interactable = false;
                LeftButton.transform.Find("Sprite").GetComponent<Image>().sprite = LeftSpawnItem.GetComponent<WeaponPickup>().FireArm.prefab.GetComponent<SpriteRenderer>().sprite;
            }
            if(RightSpawnItem != null)
            {
                RightButton.interactable = false;
                RightButton.transform.Find("Sprite").GetComponent<Image>().sprite = RightSpawnItem.GetComponent<WeaponPickup>().FireArm.prefab.GetComponent<SpriteRenderer>().sprite;
            }
            return;
        }*/
        var index = UnityEngine.Random.Range(0, weaponList.Count);
        LeftSpawnItem = LeftChoose();
        RightSpawnItem = RightChoose();
        while(LeftSpawnItem == RightSpawnItem)
        {
            RightSpawnItem = RightChoose();
        }

        LeftButton.interactable = false;
        RightButton.interactable = false;
    }

    GameObject LeftChoose()
    {
        int r = UnityEngine.Random.Range(0, 75);
        if(r <= 45)
        {
            var index = UnityEngine.Random.Range(0, weaponList.Count);
            GameObject weapon = weaponList[index];
            LeftButton.transform.Find("Sprite").GetComponent<Image>().sprite = weapon.GetComponent<WeaponPickup>().FireArm.prefab.GetComponent<SpriteRenderer>().sprite;
            return weapon;
        }
        else if(r <= 75)
        {
            var index = UnityEngine.Random.Range(0, augmentList.Count);
            GameObject augment = augmentList[index];
            LeftButton.transform.Find("Sprite").GetComponent<Image>().sprite = augment.GetComponent<SpriteRenderer>().sprite;
            return augment;
        }
        else
        {
            return currencySpawn;
        }
    }

    GameObject RightChoose()
    {
        int r = UnityEngine.Random.Range(0, 75);
        if(r <= 35)
        {
            var index = UnityEngine.Random.Range(0, weaponList.Count);
            GameObject weapon = weaponList[index];
            RightButton.transform.Find("Sprite").GetComponent<Image>().sprite = weapon.GetComponent<WeaponPickup>().FireArm.prefab.GetComponent<SpriteRenderer>().sprite;
            return weapon;
        }
        else if(r <= 75)
        {
            var index = UnityEngine.Random.Range(0, augmentList.Count);
            GameObject augment = augmentList[index];
            RightButton.transform.Find("Sprite").GetComponent<Image>().sprite = augment.GetComponent<SpriteRenderer>().sprite;
            return augment;
        }
        else
        {
            return currencySpawn;
        }
    }

    void GetInteractInput()
    {
        if(inRange && !used)// && Input.GetAxisRaw("Interact") > 0)
        {
            if(!inToilet)
            {
                DisplayToilet();
                //SpawnItem();
            }
        }
        else
        {
            if(inToilet)
            {
                CloseToilet();
            }
        }
    }

    public void EngageLeftButton()
    {
        Debug.Log("Interacted with Toilet");
        SpawnItem(LeftSpawnItem);
        used = true;
    }

    public void EngageRightButton()
    {
        Debug.Log("Interacted with Toilet");
        SpawnItem(RightSpawnItem);
        used = true;
    }

    void SpawnItem(GameObject item)
    {
        Debug.Log("Spawning reward");
        GameObject spawnedItem = Instantiate(item, transform.position, Quaternion.identity);
        spawnedItem.GetComponent<Rigidbody>().AddForce(new Vector3(0, 20, -5), ForceMode.Impulse);
    }

    // This function when invoked will display the Shop UI
    public void DisplayToilet()
    {
        ToiletUI.SetActive(true);
        Debug.Log("In display toilet");

        inToilet = true;
        if(ToiletUI.GetComponent<RectTransform>().localScale == Vector3.zero)
        {
            Debug.Log("Should lean scale in");
        }
        //ToiletUI.LeanScale(Vector3.one, displayTime).setEaseOutQuart();
        StartCoroutine(EnableButtons());
    }

    // This function when invoked disables the Shop UI
    public void CloseToilet()
    {
        ToiletUI.SetActive(false);
        StartCoroutine(closingToilet());
        if(ToiletUI.GetComponent<RectTransform>().localScale == Vector3.one)
        {
            Debug.Log("Should lean scale out");
        }
        //ToiletUI.LeanScale(Vector3.zero, displayTime).setEaseInBack();
        LeftButton.interactable = false;
        RightButton.interactable = false;
    }

    IEnumerator closingToilet()
    {
        yield return new WaitForSeconds(0.01f);
        // Debug.Log("After 0.01s");
        inToilet = false;
    }

    IEnumerator EnableButtons()
    {
        yield return new WaitForSeconds(displayTime - 0.2f);
        LeftButton.interactable = true;
        RightButton.interactable = true;
    }

    public void CheckDistance()
    {
        if(Vector3.Distance(player.transform.position, transform.position) <= toiletRange)
        {
            // Debug.Log("In Distance of Shopkeeper");
            if(inRange == false)
            {
                inRange = true;
                //HighlightShopKeeper();
                //var popUp = Key.GetComponent<SpriteRenderer>();
                //popUp.enabled = !popUp.enabled;
            }
        }
        else
        {
            // Debug.Log("Not in Distance of Shopkeeper");
            if(inRange == true)
            {
                inRange = false;
                //UnHighlightShopKeeper();
                //var popUp = Key.GetComponent<SpriteRenderer>();
                //popUp.enabled = !popUp.enabled;
            }
        }
    }

}
