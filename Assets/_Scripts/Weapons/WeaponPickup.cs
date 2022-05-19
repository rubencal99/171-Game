using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public WeaponItemSO FireArm;
    public string tag;
    private Gun weapon;
    public int pickupAmount = 1;

    public float pickupDistance = 3;

    public bool inDistance = false;

    public string name;

    public SpriteRenderer Key;

    private bool interacted;

    //public GameObject Player;

    void Start() {
        
        Key = transform.GetChild(0).GetComponent<SpriteRenderer>();

    }
    void Awake(){
        interacted = false;
    }
    //  public void CheckDistance()
    // {
    //     if(Vector2.Distance(Player.transform.position, transform.position) <= pickupDistance)
    //     {
    //         // Debug.Log("In Distance of Shopkeeper");
    //         if(inDistance == false)
    //         {
    //             inDistance = true;
    //             HighlightShopKeeper();
    //             var popUp = Key.GetComponent<SpriteRenderer>();
    //             popUp.enabled = !popUp.enabled;
    //         }
    //     }
    //     else
    //     {
    //         // Debug.Log("Not in Distance of Shopkeeper");
    //         if(inDistance == true)
    //         {
    //             inDistance = false;
    //             UnHighlightShopKeeper();
    //             var popUp = Key.GetComponent<SpriteRenderer>();
    //             popUp.enabled = !popUp.enabled;
    //         }
    //     }
    // }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("Player entered zone");
            Key.enabled = true;
           if (Input.GetAxisRaw("Interact") > 0 && !interacted) {
                 
                interacted = true;
                Debug.Log("Interacted with object");
                GameObject weaParent = GameObject.Find("WeaponParent");
                GameObject weap = FindDupe(weaParent, tag);

                if (weap != null)
                {
              
                    Debug.Log("GUN_Restocked");
                    weapon = weap.GetComponent<Gun>();
                    weapon.AmmoFill();
                    popup popup = FindObjectOfType<popup>();
                    popup.SetText("ammo refill");
                    popup.ShowText();
                    Destroy(gameObject);
                }
                else {
                    Debug.Log("GUN_Acquired");
                    GameObject thisFireArm = Instantiate(FireArm.prefab) as GameObject;
                    
                    
                    thisFireArm.transform.parent = GameObject.Find("InventoryParent").transform;
                    thisFireArm.transform.localPosition = new Vector3(0f, -0.25f, 0f);
                    thisFireArm.transform.position = Vector3.zero;
                    thisFireArm.transform.localRotation = Quaternion.identity;
                    thisFireArm.SetActive(false);
                    FireArm.prefabClone = thisFireArm;
                    Player.instance.inventory.AddItemToInventory(FireArm, pickupAmount);
                    // popup popup = FindObjectOfType<popup>();
                    //popup.SetText(name);
                    //popup.ShowText();
                    Destroy(gameObject);
                }
            }
           
        }
    }

    private void OnTriggerExit(Collider collision) {
        if (collision.tag == "Player")
        {
            Key.enabled = false;
            
        }
    }

    // private void OnCollisionEnter(Collision collision)
    // {
    //     if (collision.gameObject.tag == "Player")
    //     {
    //         GameObject weaParent = GameObject.Find("WeaponParent");
    //         GameObject weap = FindDupe(weaParent, tag);

    //         if (weap != null)
    //         {
              
    //             Debug.Log("GUN_Restocked");
    //             weapon = weap.GetComponent<Gun>();
    //             weapon.AmmoFill();
    //             popup popup = FindObjectOfType<popup>();
    //             popup.SetText("ammo refill");
    //             popup.ShowText();
    //             Destroy(gameObject);
    //         }
    //         else {
    //             Debug.Log("GUN_Acquired");
    //             GameObject thisFireArm = Instantiate(FireArm.prefab) as GameObject;
                
                
    //             thisFireArm.transform.parent = GameObject.Find("InventoryParent").transform;
    //             thisFireArm.transform.localPosition = new Vector3(0f, -0.25f, 0f);
    //             thisFireArm.transform.position = Vector3.zero;
    //             thisFireArm.transform.localRotation = Quaternion.identity;
    //             thisFireArm.SetActive(false);
    //             FireArm.prefabClone = thisFireArm;
    //             Player.instance.inventory.AddItemToInventory(FireArm, 1);
    //             // popup popup = FindObjectOfType<popup>();
    //             //popup.SetText(name);
    //             //popup.ShowText();
    //             Destroy(gameObject);
    //         }
    //     }
    // }

    public static GameObject FindDupe(GameObject parent, string tag)
    {
        Transform Pt = parent.transform;
        GameObject result;
        for (int i = 0; i < Pt.childCount; i++)
        {
            result = Pt.GetChild(i).gameObject;
            if (result?.tag == tag)
            {
                return result;
            }

        }
        return null;
    }

}



