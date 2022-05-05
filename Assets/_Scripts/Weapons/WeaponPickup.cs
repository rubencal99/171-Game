using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public WeaponItemSO FireArm;
    public string tag;
    private Gun weapon;

    public string name;


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
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
                Player.instance.inventory.AddItemToInventory(FireArm, 1);
                // popup popup = FindObjectOfType<popup>();
                //popup.SetText(name);
                //popup.ShowText();
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
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
                Player.instance.inventory.AddItemToInventory(FireArm, 1);
                // popup popup = FindObjectOfType<popup>();
                //popup.SetText(name);
                //popup.ShowText();
                Destroy(gameObject);
            }
        }
    }

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



